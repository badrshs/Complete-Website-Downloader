using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebsiteDownloader.Helpers;

namespace WebsiteDownloader.Services
{
    /// <summary>
    /// Handles website downloading using wget.exe.
    /// Implements <see cref="IWebsiteDownloader"/> for dependency injection and testability.
    /// </summary>
    public class WgetDownloader : IWebsiteDownloader
    {
        private readonly string _wgetPath;
        private readonly IAppLogger _logger;
        private readonly object _processLock = new object();
        private Process _currentProcess;
        private volatile bool _isDownloading;
        private bool _disposed;

        /// <inheritdoc/>
        public event EventHandler<DownloadProgressEventArgs> ProgressChanged;

        /// <inheritdoc/>
        public event EventHandler<DownloadCompletedEventArgs> DownloadCompleted;

        /// <inheritdoc/>
        public bool IsDownloading => _isDownloading;

        /// <summary>
        /// Initializes a new instance of the <see cref="WgetDownloader"/> class.
        /// </summary>
        /// <param name="wgetPath">The path to the wget executable.</param>
        /// <param name="logger">Optional logger for diagnostic output.</param>
        /// <exception cref="ArgumentNullException">Thrown when wgetPath is null.</exception>
        /// <exception cref="FileNotFoundException">Thrown when wget.exe is not found at the specified path.</exception>
        public WgetDownloader(string wgetPath, IAppLogger logger = null)
        {
            _wgetPath = wgetPath ?? throw new ArgumentNullException(nameof(wgetPath));
            _logger = logger ?? NullLogger.Instance;

            if (!File.Exists(_wgetPath))
                throw new FileNotFoundException("wget.exe not found", _wgetPath);

            _logger.Debug($"WgetDownloader initialized with path: {_wgetPath}");
        }

        /// <summary>
        /// Downloads a website recursively to the specified output folder.
        /// </summary>
        /// <param name="options">The download configuration options.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <exception cref="ArgumentNullException">Thrown when options is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when a download is already in progress.</exception>
        public async Task DownloadAsync(DownloadOptions options, CancellationToken cancellationToken = default)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (_isDownloading) throw new InvalidOperationException("A download is already in progress.");

            ValidateOptions(options);

            _isDownloading = true;
            var startTime = DateTime.Now;

            _logger.Info($"Starting download: {options.Url}");

            try
            {
                var arguments = BuildArguments(options);
                var outputFolder = Path.Combine(options.OutputFolder, options.Url.Host);

                _logger.Debug($"wget arguments: {arguments}");

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
                bool wasCancelled = false;
                using (cancellationToken.Register(() => 
                {
                    wasCancelled = true;
                    CancelDownload();
                }))
                {
                    try
                    {
                        await Task.Run(() => _currentProcess.WaitForExit()).ConfigureAwait(false);
                    }
                    catch (InvalidOperationException)
                    {
                        // Process was killed during cancellation
                        wasCancelled = true;
                    }
                }

                // Safely get exit code - process may have been killed
                int exitCode = 0;
                try
                {
                    if (_currentProcess != null && _currentProcess.HasExited)
                    {
                        exitCode = _currentProcess.ExitCode;
                    }
                }
                catch (InvalidOperationException)
                {
                    // Process was killed, use default exit code
                }

                // wget returns various exit codes - only truly "failed" if folder doesn't exist
                // Exit code 8 = server error (some resources unavailable) - still consider success if folder exists
                // Exit code 0 = perfect, but rare for complex sites
                var success = !wasCancelled && Directory.Exists(outputFolder);

                var duration = DateTime.Now - startTime;
                if (success)
                {
                    _logger.Info($"Download completed successfully: {options.Url} (Duration: {duration:mm\\:ss})");
                }
                else if (wasCancelled)
                {
                    _logger.Warning($"Download cancelled: {options.Url}");
                }
                else
                {
                    _logger.Error($"Download failed: {options.Url} (Exit code: {exitCode})");
                }

                OnDownloadCompleted(new DownloadCompletedEventArgs
                {
                    Success = success,
                    OutputFolder = outputFolder,
                    Url = options.Url,
                    Duration = duration,
                    Cancelled = wasCancelled || cancellationToken.IsCancellationRequested,
                    ExitCode = exitCode
                });
            }
            finally
            {
                CleanupProcess();
                _isDownloading = false;
            }
        }

        /// <summary>
        /// Timeout in milliseconds for graceful process termination.
        /// </summary>
        private const int ProcessTerminationTimeoutMs = 5000;

        /// <inheritdoc/>
        public void CancelDownload()
        {
            lock (_processLock)
            {
                try
                {
                    if (_currentProcess == null)
                        return;

                    if (_currentProcess.HasExited)
                        return;

                    // Try graceful shutdown first (give it a short time)
                    bool closedGracefully = false;
                    try
                    {
                        closedGracefully = _currentProcess.CloseMainWindow();
                        if (closedGracefully)
                        {
                            // Wait briefly for graceful exit
                            if (!_currentProcess.WaitForExit(1000))
                            {
                                closedGracefully = false;
                            }
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        // Process already exited during graceful shutdown attempt
                        return;
                    }

                    // Force kill if graceful shutdown failed
                    if (!closedGracefully && !_currentProcess.HasExited)
                    {
                        _currentProcess.Kill();
                        _currentProcess.WaitForExit(ProcessTerminationTimeoutMs);
                    }
                }
                catch (InvalidOperationException)
                {
                    // Process already exited
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    // Access denied or other Win32 error - process may be terminating
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

            // Validate rate limit format if provided
            if (!string.IsNullOrWhiteSpace(options.RateLimit) && !ValidationPatterns.RateLimitStrict.IsMatch(options.RateLimit))
                throw new ArgumentException(
                    $"Invalid rate limit format: '{options.RateLimit}'. Expected format: number followed by optional k, m, or g (e.g., '200k', '1m')", 
                    nameof(options));
        }

        private string BuildArguments(DownloadOptions options)
        {
            var args = new StringBuilder();

            // Core recursive download flags
            args.Append("-r ");                    // Recursive
            args.Append("-p ");                    // Page requisites (CSS, JS, images)
            args.Append("-e robots=off ");         // Ignore robots.txt
            args.Append($"-U \"{SanitizeArgument(options.UserAgent)}\" "); // User agent

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
                args.Append($"--limit-rate={SanitizeArgument(options.RateLimit)} ");

            if (options.NoClobber)
                args.Append("-nc ");               // Don't overwrite existing files

            // New options: Continue/Resume
            if (options.ContinueDownload)
                args.Append("-c ");                // Continue getting a partially-downloaded file

            // SSL/TLS options
            if (options.IgnoreSslErrors)
                args.Append("--no-check-certificate ");  // Don't validate server certificate

            // Timeout settings
            if (options.ConnectionTimeout > 0)
                args.Append($"--connect-timeout={options.ConnectionTimeout} ");

            if (options.ReadTimeout > 0)
                args.Append($"--read-timeout={options.ReadTimeout} ");

            // Retry settings
            if (options.RetryCount >= 0)
                args.Append($"--tries={options.RetryCount} ");

            // URL and output directory
            args.Append($"\"{options.Url}\" ");
            args.Append($"-P \"./{SanitizeArgument(options.Url.Host)}\"");

            return args.ToString();
        }

        /// <summary>
        /// Sanitizes a string for safe use in command-line arguments.
        /// Escapes quotes and removes potentially dangerous characters.
        /// </summary>
        /// <param name="value">The value to sanitize.</param>
        /// <returns>A sanitized string safe for command-line use.</returns>
        private static string SanitizeArgument(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            // Escape embedded quotes
            var sanitized = value.Replace("\"", "\\\"");
            
            // Remove shell metacharacters that could cause command injection
            sanitized = sanitized.Replace("|", "")
                                 .Replace("&", "")
                                 .Replace(";", "")
                                 .Replace("`", "")
                                 .Replace("$", "")
                                 .Replace("(", "")
                                 .Replace(")", "")
                                 .Replace("<", "")
                                 .Replace(">", "");

            return sanitized;
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
            lock (_processLock)
            {
                if (_currentProcess != null)
                {
                    _currentProcess.Dispose();
                    _currentProcess = null;
                }
            }
        }

        /// <summary>
        /// Releases all resources used by the <see cref="WgetDownloader"/>.
        /// </summary>
        /// <param name="disposing">True if called from Dispose(), false if called from finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    CancelDownload();
                    CleanupProcess();
                }
                _disposed = true;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Immutable options for configuring a website download.
    /// </summary>
    public class DownloadOptions
    {
        /// <summary>
        /// Gets the URL of the website to download.
        /// </summary>
        public Uri Url { get; }

        /// <summary>
        /// Gets the output folder path where the website will be saved.
        /// </summary>
        public string OutputFolder { get; }

        /// <summary>
        /// Gets the user agent string for HTTP requests.
        /// </summary>
        public string UserAgent { get; }

        /// <summary>
        /// Gets a value indicating whether to convert links for offline viewing.
        /// </summary>
        public bool ConvertLinks { get; }

        /// <summary>
        /// Gets a value indicating whether to adjust file extensions (add .html).
        /// </summary>
        public bool AdjustExtensions { get; }

        /// <summary>
        /// Gets the maximum recursion depth. 0 means unlimited.
        /// </summary>
        public int MaxDepth { get; }

        /// <summary>
        /// Gets the wait time in seconds between requests.
        /// </summary>
        public int WaitBetweenRequests { get; }

        /// <summary>
        /// Gets the bandwidth rate limit (e.g., "200k", "1m").
        /// </summary>
        public string RateLimit { get; }

        /// <summary>
        /// Gets a value indicating whether to skip existing files.
        /// </summary>
        public bool NoClobber { get; }

        /// <summary>
        /// Gets a value indicating whether to continue/resume interrupted downloads.
        /// </summary>
        public bool ContinueDownload { get; }

        /// <summary>
        /// Gets a value indicating whether to ignore SSL certificate errors.
        /// </summary>
        public bool IgnoreSslErrors { get; }

        /// <summary>
        /// Gets the connection timeout in seconds.
        /// </summary>
        public int ConnectionTimeout { get; }

        /// <summary>
        /// Gets the read timeout in seconds.
        /// </summary>
        public int ReadTimeout { get; }

        /// <summary>
        /// Gets the number of retry attempts on failure.
        /// </summary>
        public int RetryCount { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadOptions"/> class.
        /// </summary>
        public DownloadOptions(
            Uri url,
            string outputFolder,
            string userAgent = null,
            bool convertLinks = true,
            bool adjustExtensions = true,
            int maxDepth = 0,
            int waitBetweenRequests = 0,
            string rateLimit = null,
            bool noClobber = false,
            bool continueDownload = true,
            bool ignoreSslErrors = false,
            int connectionTimeout = 30,
            int readTimeout = 60,
            int retryCount = 3)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
            OutputFolder = outputFolder ?? throw new ArgumentNullException(nameof(outputFolder));
            UserAgent = userAgent ?? AppConstants.DefaultUserAgent;
            ConvertLinks = convertLinks;
            AdjustExtensions = adjustExtensions;
            MaxDepth = maxDepth;
            WaitBetweenRequests = waitBetweenRequests;
            RateLimit = rateLimit;
            NoClobber = noClobber;
            ContinueDownload = continueDownload;
            IgnoreSslErrors = ignoreSslErrors;
            ConnectionTimeout = connectionTimeout;
            ReadTimeout = readTimeout;
            RetryCount = retryCount;
        }
    }

    /// <summary>
    /// Event arguments for download progress updates.
    /// </summary>
    public class DownloadProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the progress message.
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// Event arguments for download completion.
    /// </summary>
    public class DownloadCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether the download was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the download was cancelled.
        /// </summary>
        public bool Cancelled { get; set; }

        /// <summary>
        /// Gets or sets the output folder path.
        /// </summary>
        public string OutputFolder { get; set; }

        /// <summary>
        /// Gets or sets the downloaded URL.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// Gets or sets the download duration.
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets the wget exit code.
        /// </summary>
        public int ExitCode { get; set; }

        /// <summary>
        /// Gets or sets the error message if the download failed.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
