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

            // Quota uses the same size format as rate limit (e.g., "500m")
            if (!string.IsNullOrWhiteSpace(options.DownloadQuota) && !ValidationPatterns.RateLimitStrict.IsMatch(options.DownloadQuota))
                throw new ArgumentException(
                    $"Invalid quota format: '{options.DownloadQuota}'. Expected format: number followed by optional k, m, or g (e.g., '500m', '2g')",
                    nameof(options));

            // A missing cookies file would make wget fail with an obscure error; fail fast instead.
            if (!string.IsNullOrWhiteSpace(options.CookiesFilePath) && !File.Exists(options.CookiesFilePath))
                throw new FileNotFoundException(
                    $"Cookies file not found: {options.CookiesFilePath}", options.CookiesFilePath);
        }

        internal string BuildArguments(DownloadOptions options)
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
            else
                args.Append("-l 0 ");              // Explicitly set infinite depth (wget defaults to 5)

            if (options.WaitBetweenRequests > 0)
                args.Append($"-w {options.WaitBetweenRequests} ");

            if (!string.IsNullOrEmpty(options.RateLimit))
                args.Append($"--limit-rate={SanitizeArgument(options.RateLimit)} ");

            // Resume/skip behaviour: emit exactly one of -c / -N / -nc (or nothing).
            // These flags are mutually exclusive in wget, so this must never combine them.
            args.Append(options.ResumeMode.ToWgetFlag());

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

            // Recursion scope: keep the crawl from wandering off-site / out of the subtree
            if (options.NoParent)
                args.Append("-np ");                   // Never ascend to parent directories

            if (options.SpanHosts)
                args.Append("-H ");                    // Allow crossing to other hosts

            if (!string.IsNullOrWhiteSpace(options.DomainList))
                args.Append($"--domains=\"{SanitizeArgument(options.DomainList)}\" ");

            // File and directory filters
            if (!string.IsNullOrWhiteSpace(options.AcceptFileTypes))
                args.Append($"-A \"{SanitizeArgument(options.AcceptFileTypes)}\" ");

            if (!string.IsNullOrWhiteSpace(options.RejectFileTypes))
                args.Append($"-R \"{SanitizeArgument(options.RejectFileTypes)}\" ");

            if (!string.IsNullOrWhiteSpace(options.IncludeDirectories))
                args.Append($"-I \"{SanitizeArgument(options.IncludeDirectories)}\" ");

            if (!string.IsNullOrWhiteSpace(options.ExcludeDirectories))
                args.Append($"-X \"{SanitizeArgument(options.ExcludeDirectories)}\" ");

            if (options.IgnoreFilterCase)
                args.Append("--ignore-case ");

            // Size and redirect limits
            if (!string.IsNullOrWhiteSpace(options.DownloadQuota))
                args.Append($"--quota={SanitizeArgument(options.DownloadQuota)} ");

            if (options.MaxRedirect > 0)
                args.Append($"--max-redirect={options.MaxRedirect} ");

            // Authentication and request headers
            AppendAuthArguments(args, options);

            // Politeness / behaviour tweaks
            if (options.RandomWait)
                args.Append("--random-wait ");         // Vary -w by 0.5-1.5x to look less bot-like

            if (options.ContentDisposition)
                args.Append("--content-disposition "); // Honor server-suggested file names

            // Directory layout: emit exactly one of -nd / -x (or nothing).
            args.Append(options.DirectoryStructure.ToWgetFlag());

            // Always sanitize file names for Windows; URLs containing ?, *, : etc.
            // would otherwise produce invalid paths and silently fail to save.
            args.Append("--restrict-file-names=windows ");

            // URL and output directory
            args.Append($"\"{options.Url}\" ");
            args.Append($"-P \"./{SanitizeArgument(options.Url.Host)}\"");

            return args.ToString();
        }

        /// <summary>
        /// Appends authentication-related arguments (credentials, cookies, custom headers,
        /// referer) shared by every wget invocation that may hit protected resources.
        /// </summary>
        internal static void AppendAuthArguments(StringBuilder args, DownloadOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.HttpUser))
                args.Append($"--user=\"{SanitizeArgument(options.HttpUser)}\" ");

            if (!string.IsNullOrWhiteSpace(options.HttpPassword))
                args.Append($"--password=\"{SanitizeArgument(options.HttpPassword)}\" ");

            if (!string.IsNullOrWhiteSpace(options.CookiesFilePath))
            {
                args.Append($"--load-cookies \"{SanitizeArgument(options.CookiesFilePath)}\" ");
                if (options.KeepSessionCookies)
                    args.Append("--keep-session-cookies ");
            }

            if (!string.IsNullOrWhiteSpace(options.CustomHeaders))
            {
                foreach (var rawLine in options.CustomHeaders.Split('\n'))
                {
                    var header = rawLine.Trim().TrimEnd('\r');
                    if (header.Length > 0 && header.Contains(":"))
                        args.Append($"--header \"{SanitizeArgument(header)}\" ");
                }
            }

            if (!string.IsNullOrWhiteSpace(options.Referer))
                args.Append($"--referer=\"{SanitizeArgument(options.Referer)}\" ");
        }

        /// <summary>
        /// Sanitizes a string for safe use in command-line arguments.
        /// Escapes quotes and removes potentially dangerous characters.
        /// </summary>
        /// <param name="value">The value to sanitize.</param>
        /// <returns>A sanitized string safe for command-line use.</returns>
        internal static string SanitizeArgument(string value)
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
        /// Gets how a restart/re-run treats files already present on disk.
        /// Maps to a single, mutually-exclusive wget flag (-c / -N / -nc).
        /// </summary>
        public ResumeMode ResumeMode { get; }

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
        /// Gets a value indicating whether recursion is restricted to the start URL's
        /// directory subtree, never ascending to parent directories (wget <c>-np</c>).
        /// </summary>
        public bool NoParent { get; }

        /// <summary>
        /// Gets a value indicating whether recursion may span to hosts other than the
        /// start host (wget <c>-H</c>). Usually combined with <see cref="DomainList"/>.
        /// </summary>
        public bool SpanHosts { get; }

        /// <summary>
        /// Gets the comma-separated list of domains recursion is allowed to follow
        /// (wget <c>--domains</c>). Empty means no restriction beyond the start host.
        /// </summary>
        public string DomainList { get; }

        /// <summary>
        /// Gets the comma-separated list of accepted file suffixes/patterns (wget <c>-A</c>).
        /// </summary>
        public string AcceptFileTypes { get; }

        /// <summary>
        /// Gets the comma-separated list of rejected file suffixes/patterns (wget <c>-R</c>).
        /// </summary>
        public string RejectFileTypes { get; }

        /// <summary>
        /// Gets the comma-separated list of directories to follow (wget <c>-I</c>).
        /// </summary>
        public string IncludeDirectories { get; }

        /// <summary>
        /// Gets the comma-separated list of directories to skip (wget <c>-X</c>).
        /// </summary>
        public string ExcludeDirectories { get; }

        /// <summary>
        /// Gets a value indicating whether accept/reject/directory matching is
        /// case-insensitive (wget <c>--ignore-case</c>).
        /// </summary>
        public bool IgnoreFilterCase { get; }

        /// <summary>
        /// Gets the total download size cap (wget <c>--quota</c>, e.g. "500m").
        /// Empty means unlimited.
        /// </summary>
        public string DownloadQuota { get; }

        /// <summary>
        /// Gets the maximum number of redirects to follow (wget <c>--max-redirect</c>).
        /// 0 or negative means use wget's default.
        /// </summary>
        public int MaxRedirect { get; }

        /// <summary>
        /// Gets the username for HTTP authentication (wget <c>--user</c>).
        /// </summary>
        public string HttpUser { get; }

        /// <summary>
        /// Gets the password for HTTP authentication (wget <c>--password</c>).
        /// </summary>
        public string HttpPassword { get; }

        /// <summary>
        /// Gets the path to a Netscape-format cookies file (wget <c>--load-cookies</c>).
        /// Lets users download login-protected sites using cookies exported from a browser.
        /// </summary>
        public string CookiesFilePath { get; }

        /// <summary>
        /// Gets a value indicating whether session (non-persistent) cookies are honored
        /// (wget <c>--keep-session-cookies</c>). Only meaningful with <see cref="CookiesFilePath"/>.
        /// </summary>
        public bool KeepSessionCookies { get; }

        /// <summary>
        /// Gets custom HTTP headers, one per line in "Name: value" form (wget <c>--header</c>).
        /// </summary>
        public string CustomHeaders { get; }

        /// <summary>
        /// Gets the Referer header value to send (wget <c>--referer</c>).
        /// </summary>
        public string Referer { get; }

        /// <summary>
        /// Gets a value indicating whether the wait between requests is randomized
        /// (wget <c>--random-wait</c>, 0.5–1.5× of <see cref="WaitBetweenRequests"/>).
        /// </summary>
        public bool RandomWait { get; }

        /// <summary>
        /// Gets a value indicating whether server-suggested file names are honored
        /// (wget <c>--content-disposition</c>).
        /// </summary>
        public bool ContentDisposition { get; }

        /// <summary>
        /// Gets how downloaded files are laid out on disk.
        /// Maps to a single, mutually-exclusive wget flag (-nd / -x).
        /// </summary>
        public DirectoryStructure DirectoryStructure { get; }

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
            int retryCount = 3,
            ResumeMode? resumeMode = null,
            bool noParent = false,
            bool spanHosts = false,
            string domainList = null,
            string acceptFileTypes = null,
            string rejectFileTypes = null,
            string includeDirectories = null,
            string excludeDirectories = null,
            bool ignoreFilterCase = false,
            string downloadQuota = null,
            int maxRedirect = 20,
            string httpUser = null,
            string httpPassword = null,
            string cookiesFilePath = null,
            bool keepSessionCookies = false,
            string customHeaders = null,
            string referer = null,
            bool randomWait = false,
            bool contentDisposition = false,
            DirectoryStructure directoryStructure = DirectoryStructure.Default)
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
            // When the caller doesn't specify a mode explicitly, derive it from the legacy
            // booleans so existing call sites keep their previous behaviour.
            ResumeMode = resumeMode ?? ResumeModeExtensions.FromLegacyFlags(noClobber, continueDownload);
            IgnoreSslErrors = ignoreSslErrors;
            ConnectionTimeout = connectionTimeout;
            ReadTimeout = readTimeout;
            RetryCount = retryCount;
            NoParent = noParent;
            SpanHosts = spanHosts;
            DomainList = domainList;
            AcceptFileTypes = acceptFileTypes;
            RejectFileTypes = rejectFileTypes;
            IncludeDirectories = includeDirectories;
            ExcludeDirectories = excludeDirectories;
            IgnoreFilterCase = ignoreFilterCase;
            DownloadQuota = downloadQuota;
            MaxRedirect = maxRedirect;
            HttpUser = httpUser;
            HttpPassword = httpPassword;
            CookiesFilePath = cookiesFilePath;
            KeepSessionCookies = keepSessionCookies;
            CustomHeaders = customHeaders;
            Referer = referer;
            RandomWait = randomWait;
            ContentDisposition = contentDisposition;
            DirectoryStructure = directoryStructure;
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
