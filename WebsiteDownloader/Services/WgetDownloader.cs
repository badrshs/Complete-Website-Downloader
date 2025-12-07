using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebsiteDownloader.Services
{
    /// <summary>
    /// Handles website downloading using wget.exe
    /// </summary>
    public class WgetDownloader : IDisposable
    {
        private readonly string _wgetPath;
        private Process _currentProcess;
        private bool _disposed;

        public event EventHandler<DownloadProgressEventArgs> ProgressChanged;
        public event EventHandler<DownloadCompletedEventArgs> DownloadCompleted;

        public bool IsDownloading { get; private set; }

        public WgetDownloader(string wgetPath)
        {
            _wgetPath = wgetPath ?? throw new ArgumentNullException(nameof(wgetPath));

            if (!File.Exists(_wgetPath))
                throw new FileNotFoundException("wget.exe not found", _wgetPath);
        }

        /// <summary>
        /// Downloads a website recursively to the specified output folder
        /// </summary>
        public async Task DownloadAsync(DownloadOptions options, CancellationToken cancellationToken = default)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (IsDownloading) throw new InvalidOperationException("A download is already in progress.");

            ValidateOptions(options);

            IsDownloading = true;
            var startTime = DateTime.Now;

            try
            {
                var arguments = BuildArguments(options);
                var outputFolder = Path.Combine(options.OutputFolder, options.Url.Host);

                _currentProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = _wgetPath,
                        Arguments = arguments,
                        WorkingDirectory = options.OutputFolder,
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
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        OnProgressChanged(e.Data);
                    }
                };

                _currentProcess.Start();
                _currentProcess.BeginErrorReadLine();

                // Register cancellation
                using (cancellationToken.Register(() => CancelDownload()))
                {
                    await Task.Run(() => _currentProcess.WaitForExit(), cancellationToken);
                }

                var exitCode = _currentProcess.ExitCode;
                // wget returns various exit codes - only truly "failed" if folder doesn't exist
                // Exit code 8 = server error (some resources unavailable) - still consider success if folder exists
                // Exit code 0 = perfect, but rare for complex sites
                var success = Directory.Exists(outputFolder);

                OnDownloadCompleted(new DownloadCompletedEventArgs
                {
                    Success = success,
                    OutputFolder = outputFolder,
                    Url = options.Url,
                    Duration = DateTime.Now - startTime,
                    Cancelled = cancellationToken.IsCancellationRequested,
                    ExitCode = exitCode
                });
            }
            finally
            {
                CleanupProcess();
                IsDownloading = false;
            }
        }

        /// <summary>
        /// Cancels the current download operation
        /// </summary>
        public void CancelDownload()
        {
            if (_currentProcess != null && !_currentProcess.HasExited)
            {
                try
                {
                    _currentProcess.Kill();
                    _currentProcess.WaitForExit(1000);
                }
                catch (InvalidOperationException)
                {
                    // Process already exited
                }
            }
        }

        private void ValidateOptions(DownloadOptions options)
        {
            if (options.Url == null)
                throw new ArgumentException("URL is required", nameof(options));

            if (options.Url.Scheme != Uri.UriSchemeHttp && options.Url.Scheme != Uri.UriSchemeHttps)
                throw new ArgumentException("URL must be HTTP or HTTPS", nameof(options));

            if (string.IsNullOrWhiteSpace(options.OutputFolder))
                throw new ArgumentException("Output folder is required", nameof(options));

            if (!Directory.Exists(options.OutputFolder))
                throw new DirectoryNotFoundException($"Output folder does not exist: {options.OutputFolder}");
        }

        private string BuildArguments(DownloadOptions options)
        {
            var args = new StringBuilder();

            // Core recursive download flags
            args.Append("-r ");                    // Recursive
            args.Append("-p ");                    // Page requisites (CSS, JS, images)
            args.Append("-e robots=off ");         // Ignore robots.txt
            args.Append($"-U \"{options.UserAgent}\" "); // User agent

            // Optional flags based on settings
            if (options.ConvertLinks)
                args.Append("-k ");                // Convert links for offline viewing

            if (options.AdjustExtensions)
                args.Append("-E ");                // Add .html extensions

            if (options.MaxDepth > 0)
                args.Append($"-l {options.MaxDepth} ");

            if (options.WaitBetweenRequests > 0)
                args.Append($"-w {options.WaitBetweenRequests} ");

            if (!string.IsNullOrEmpty(options.RateLimit))
                args.Append($"--limit-rate={options.RateLimit} ");

            if (options.NoClobber)
                args.Append("-nc ");               // Don't overwrite existing files

            // URL and output directory
            args.Append($"\"{options.Url}\" ");
            args.Append($"-P \"./{options.Url.Host}\"");

            return args.ToString();
        }

        private void OnProgressChanged(string message)
        {
            ProgressChanged?.Invoke(this, new DownloadProgressEventArgs { Message = message });
        }

        private void OnDownloadCompleted(DownloadCompletedEventArgs args)
        {
            DownloadCompleted?.Invoke(this, args);
        }

        private void CleanupProcess()
        {
            if (_currentProcess != null)
            {
                _currentProcess.Dispose();
                _currentProcess = null;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                CancelDownload();
                CleanupProcess();
                _disposed = true;
            }
        }
    }

    /// <summary>
    /// Options for configuring a website download
    /// </summary>
    public class DownloadOptions
    {
        public Uri Url { get; set; }
        public string OutputFolder { get; set; }
        public string UserAgent { get; set; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36";
        public bool ConvertLinks { get; set; } = true;
        public bool AdjustExtensions { get; set; } = true;
        public int MaxDepth { get; set; } = 0;  // 0 = unlimited
        public int WaitBetweenRequests { get; set; } = 0;  // seconds
        public string RateLimit { get; set; }  // e.g., "200k"
        public bool NoClobber { get; set; } = false;
    }

    public class DownloadProgressEventArgs : EventArgs
    {
        public string Message { get; set; }
    }

    public class DownloadCompletedEventArgs : EventArgs
    {
        public bool Success { get; set; }
        public bool Cancelled { get; set; }
        public string OutputFolder { get; set; }
        public Uri Url { get; set; }
        public TimeSpan Duration { get; set; }
        public int ExitCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
