using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebsiteDownloader.Services
{
    /// <summary>
    /// Event arguments for size estimation progress.
    /// </summary>
    public class SizeEstimationProgressEventArgs : EventArgs
    {
        public int FilesFound { get; set; }
        public long EstimatedBytes { get; set; }
        public string CurrentUrl { get; set; }
    }

    /// <summary>
    /// Result of size estimation.
    /// </summary>
    public class SizeEstimationResult
    {
        public bool Success { get; set; }
        public int TotalFiles { get; set; }
        public long TotalBytes { get; set; }
        public TimeSpan Duration { get; set; }
        public string ErrorMessage { get; set; }
        public bool WasCancelled { get; set; }

        /// <summary>
        /// Gets a human-readable size string.
        /// </summary>
        public string FormattedSize
        {
            get
            {
                if (TotalBytes < 1024)
                    return $"{TotalBytes} B";
                if (TotalBytes < 1024 * 1024)
                    return $"{TotalBytes / 1024.0:F1} KB";
                if (TotalBytes < 1024 * 1024 * 1024)
                    return $"{TotalBytes / (1024.0 * 1024):F1} MB";
                return $"{TotalBytes / (1024.0 * 1024 * 1024):F2} GB";
            }
        }
    }

    /// <summary>
    /// Estimates download size using wget's spider mode.
    /// </summary>
    public class SizeEstimator
    {
        private readonly string _wgetPath;
        private Process _currentProcess;
        private volatile bool _isRunning;

        // Regex to extract file sizes from wget spider output
        // Matches: "Length: 12345 (12K)" or "Length: 12345"
        private static readonly Regex LengthPattern = new Regex(
            @"Length:\s*(\d+)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        // Matches URLs being processed
        private static readonly Regex UrlPattern = new Regex(
            @"--\s*[\d:]+\s+(.+)$",
            RegexOptions.Compiled);

        /// <summary>
        /// Event raised during estimation to report progress.
        /// </summary>
        public event EventHandler<SizeEstimationProgressEventArgs> ProgressChanged;

        public bool IsRunning => _isRunning;

        public SizeEstimator(string wgetPath)
        {
            _wgetPath = wgetPath ?? throw new ArgumentNullException(nameof(wgetPath));
        }

        /// <summary>
        /// Estimates the total download size for a URL using spider mode.
        /// </summary>
        /// <param name="url">The URL to estimate.</param>
        /// <param name="maxDepth">Maximum recursion depth (0 = unlimited).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Estimation result.</returns>
        public async Task<SizeEstimationResult> EstimateSizeAsync(
            Uri url, 
            int maxDepth = 5, 
            CancellationToken cancellationToken = default)
        {
            if (_isRunning)
                throw new InvalidOperationException("An estimation is already in progress.");

            _isRunning = true;
            var startTime = DateTime.Now;
            int filesFound = 0;
            long totalBytes = 0;

            try
            {
                var args = new StringBuilder();
                args.Append("--spider ");          // Don't download, just check
                args.Append("-r ");                // Recursive
                args.Append("-e robots=off ");     // Ignore robots.txt
                args.Append("-nd ");               // Don't create directories
                
                if (maxDepth > 0)
                    args.Append($"-l {maxDepth} ");
                else
                    args.Append("-l 3 ");          // Default to 3 levels for estimation

                args.Append($"\"{url}\"");

                _currentProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = _wgetPath,
                        Arguments = args.ToString(),
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        StandardOutputEncoding = Encoding.UTF8,
                        StandardErrorEncoding = Encoding.UTF8
                    },
                    EnableRaisingEvents = true
                };

                _currentProcess.ErrorDataReceived += (s, e) =>
                {
                    if (string.IsNullOrEmpty(e.Data)) return;

                    // Extract file size
                    var lengthMatch = LengthPattern.Match(e.Data);
                    if (lengthMatch.Success && long.TryParse(lengthMatch.Groups[1].Value, out long size))
                    {
                        filesFound++;
                        totalBytes += size;

                        // Extract current URL
                        string currentUrl = null;
                        var urlMatch = UrlPattern.Match(e.Data);
                        if (urlMatch.Success)
                            currentUrl = urlMatch.Groups[1].Value;

                        ProgressChanged?.Invoke(this, new SizeEstimationProgressEventArgs
                        {
                            FilesFound = filesFound,
                            EstimatedBytes = totalBytes,
                            CurrentUrl = currentUrl
                        });
                    }
                };

                _currentProcess.Start();
                _currentProcess.BeginErrorReadLine();

                bool wasCancelled = false;
                using (cancellationToken.Register(() =>
                {
                    wasCancelled = true;
                    Cancel();
                }))
                {
                    await Task.Run(() => _currentProcess.WaitForExit()).ConfigureAwait(false);
                }

                return new SizeEstimationResult
                {
                    Success = !wasCancelled && filesFound > 0,
                    TotalFiles = filesFound,
                    TotalBytes = totalBytes,
                    Duration = DateTime.Now - startTime,
                    WasCancelled = wasCancelled
                };
            }
            catch (Exception ex)
            {
                return new SizeEstimationResult
                {
                    Success = false,
                    TotalFiles = filesFound,
                    TotalBytes = totalBytes,
                    Duration = DateTime.Now - startTime,
                    ErrorMessage = ex.Message
                };
            }
            finally
            {
                _currentProcess?.Dispose();
                _currentProcess = null;
                _isRunning = false;
            }
        }

        /// <summary>
        /// Cancels the current estimation.
        /// </summary>
        public void Cancel()
        {
            try
            {
                if (_currentProcess != null && !_currentProcess.HasExited)
                {
                    _currentProcess.Kill();
                }
            }
            catch (InvalidOperationException)
            {
                // Process already exited
            }
        }
    }
}
