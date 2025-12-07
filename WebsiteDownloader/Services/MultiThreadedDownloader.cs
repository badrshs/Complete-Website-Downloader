using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WebsiteDownloader.Helpers;

namespace WebsiteDownloader.Services
{
    /// <summary>
    /// Progress event for multi-threaded downloads.
    /// </summary>
    public class MultiThreadProgressEventArgs : EventArgs
    {
        public int ActiveThreads { get; set; }
        public int CompletedUrls { get; set; }
        public int TotalUrls { get; set; }
        public string CurrentMessage { get; set; }
    }

    /// <summary>
    /// Result of multi-threaded download.
    /// </summary>
    public class MultiThreadDownloadResult
    {
        public bool Success { get; set; }
        public int TotalUrls { get; set; }
        public int SuccessfulUrls { get; set; }
        public int FailedUrls { get; set; }
        public TimeSpan Duration { get; set; }
        public string OutputFolder { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    /// <summary>
    /// Handles multi-threaded website downloading using parallel wget instances.
    /// First discovers URLs via spider mode, then downloads them in parallel.
    /// </summary>
    public class MultiThreadedDownloader
    {
        private readonly string _wgetPath;
        private readonly IAppLogger _logger;
        private readonly ConcurrentDictionary<int, Process> _activeProcesses;
        private volatile bool _isCancelled;

        // Regex to extract URLs from wget spider output
        private static readonly Regex UrlPattern = new Regex(
            @"--\s*[\d:]+\s+(https?://\S+)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public event EventHandler<MultiThreadProgressEventArgs> ProgressChanged;

        public MultiThreadedDownloader(string wgetPath, IAppLogger logger = null)
        {
            _wgetPath = wgetPath ?? throw new ArgumentNullException(nameof(wgetPath));
            _logger = logger ?? NullLogger.Instance;
            _activeProcesses = new ConcurrentDictionary<int, Process>();
        }

        /// <summary>
        /// Downloads a website using multiple parallel wget instances.
        /// </summary>
        /// <param name="options">Download options.</param>
        /// <param name="threadCount">Number of parallel downloads.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Download result.</returns>
        public async Task<MultiThreadDownloadResult> DownloadAsync(
            DownloadOptions options,
            int threadCount = 4,
            CancellationToken cancellationToken = default)
        {
            var startTime = DateTime.Now;
            var result = new MultiThreadDownloadResult
            {
                OutputFolder = Path.Combine(options.OutputFolder, options.Url.Host)
            };

            _isCancelled = false;

            try
            {
                _logger.Info($"Starting multi-threaded download: {options.Url} with {threadCount} threads");

                // Phase 1: Discover all URLs using spider mode
                OnProgress(0, 0, 0, "Discovering URLs...");
                var urls = await DiscoverUrlsAsync(options.Url, options.MaxDepth, cancellationToken);
                
                if (cancellationToken.IsCancellationRequested || _isCancelled)
                {
                    result.Success = false;
                    return result;
                }

                result.TotalUrls = urls.Count;
                _logger.Info($"Discovered {urls.Count} URLs to download");

                if (urls.Count == 0)
                {
                    result.Success = false;
                    result.Errors.Add("No URLs discovered");
                    return result;
                }

                // Phase 2: Download URLs in parallel
                var urlQueue = new ConcurrentQueue<string>(urls);
                int completedCount = 0;
                int successCount = 0;
                var errors = new ConcurrentBag<string>();

                var downloadTasks = new List<Task>();
                var semaphore = new SemaphoreSlim(threadCount);

                foreach (var url in urls)
                {
                    if (cancellationToken.IsCancellationRequested || _isCancelled)
                        break;

                    await semaphore.WaitAsync(cancellationToken);

                    var task = Task.Run(async () =>
                    {
                        try
                        {
                            if (!_isCancelled)
                            {
                                var success = await DownloadSingleUrlAsync(url, options, cancellationToken);
                                if (success)
                                    Interlocked.Increment(ref successCount);
                                else
                                    errors.Add($"Failed to download: {url}");
                            }
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Error downloading {url}: {ex.Message}");
                        }
                        finally
                        {
                            var completed = Interlocked.Increment(ref completedCount);
                            OnProgress(_activeProcesses.Count, completed, urls.Count, $"Downloaded {completed}/{urls.Count}");
                            semaphore.Release();
                        }
                    }, cancellationToken);

                    downloadTasks.Add(task);
                }

                await Task.WhenAll(downloadTasks);

                result.Success = successCount > 0;
                result.SuccessfulUrls = successCount;
                result.FailedUrls = urls.Count - successCount;
                result.Duration = DateTime.Now - startTime;
                result.Errors = errors.ToList();

                _logger.Info($"Multi-threaded download completed: {successCount}/{urls.Count} successful");

                return result;
            }
            catch (OperationCanceledException)
            {
                result.Success = false;
                result.Duration = DateTime.Now - startTime;
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"Multi-threaded download failed: {ex.Message}", ex);
                result.Success = false;
                result.Duration = DateTime.Now - startTime;
                result.Errors.Add(ex.Message);
                return result;
            }
            finally
            {
                // Cleanup any remaining processes
                foreach (var kvp in _activeProcesses)
                {
                    try { kvp.Value?.Kill(); } catch { }
                    try { kvp.Value?.Dispose(); } catch { }
                }
                _activeProcesses.Clear();
            }
        }

        /// <summary>
        /// Cancels all active downloads.
        /// </summary>
        public void Cancel()
        {
            _isCancelled = true;
            foreach (var kvp in _activeProcesses)
            {
                try
                {
                    if (!kvp.Value.HasExited)
                        kvp.Value.Kill();
                }
                catch { }
            }
        }

        /// <summary>
        /// Discovers all URLs from a website using spider mode.
        /// </summary>
        private async Task<List<string>> DiscoverUrlsAsync(Uri url, int maxDepth, CancellationToken ct)
        {
            var urls = new HashSet<string>();
            urls.Add(url.ToString());

            var args = new StringBuilder();
            args.Append("--spider ");
            args.Append("-r ");
            args.Append("-e robots=off ");
            if (maxDepth > 0)
                args.Append($"-l {maxDepth} ");
            else
                args.Append("-l 5 ");  // Limit depth for URL discovery
            args.Append($"\"{url}\"");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _wgetPath,
                    Arguments = args.ToString(),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    StandardErrorEncoding = Encoding.UTF8
                }
            };

            process.ErrorDataReceived += (s, e) =>
            {
                if (string.IsNullOrEmpty(e.Data)) return;

                var match = UrlPattern.Match(e.Data);
                if (match.Success)
                {
                    var discoveredUrl = match.Groups[1].Value;
                    // Only add URLs from the same domain
                    if (Uri.TryCreate(discoveredUrl, UriKind.Absolute, out Uri parsed))
                    {
                        if (parsed.Host.Equals(url.Host, StringComparison.OrdinalIgnoreCase))
                        {
                            lock (urls)
                            {
                                urls.Add(discoveredUrl);
                            }
                        }
                    }
                }
            };

            process.Start();
            process.BeginErrorReadLine();

            using (ct.Register(() => { try { process.Kill(); } catch { } }))
            {
                await Task.Run(() => process.WaitForExit());
            }

            process.Dispose();

            return urls.ToList();
        }

        /// <summary>
        /// Downloads a single URL.
        /// </summary>
        private async Task<bool> DownloadSingleUrlAsync(string urlString, DownloadOptions options, CancellationToken ct)
        {
            var args = new StringBuilder();
            args.Append("-p ");  // Page requisites
            args.Append("-e robots=off ");
            args.Append($"-U \"{options.UserAgent}\" ");
            
            if (options.ConvertLinks)
                args.Append("-k ");
            if (options.AdjustExtensions)
                args.Append("-E ");
            if (options.ContinueDownload)
                args.Append("-c ");
            if (options.IgnoreSslErrors)
                args.Append("--no-check-certificate ");
            if (!string.IsNullOrEmpty(options.RateLimit))
                args.Append($"--limit-rate={options.RateLimit} ");
            if (options.ConnectionTimeout > 0)
                args.Append($"--connect-timeout={options.ConnectionTimeout} ");
            if (options.ReadTimeout > 0)
                args.Append($"--read-timeout={options.ReadTimeout} ");

            args.Append($"--tries={options.RetryCount} ");
            args.Append($"\"{urlString}\" ");
            
            if (Uri.TryCreate(urlString, UriKind.Absolute, out Uri parsed))
            {
                args.Append($"-P \"./{parsed.Host}\"");
            }

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _wgetPath,
                    Arguments = args.ToString(),
                    WorkingDirectory = options.OutputFolder,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            var processId = process.GetHashCode();
            _activeProcesses[processId] = process;

            try
            {
                process.Start();

                using (ct.Register(() => { try { process.Kill(); } catch { } }))
                {
                    await Task.Run(() => process.WaitForExit());
                }

                return process.ExitCode == 0 || process.ExitCode == 8; // 8 = some files not downloaded
            }
            finally
            {
                _activeProcesses.TryRemove(processId, out _);
                process.Dispose();
            }
        }

        private void OnProgress(int activeThreads, int completed, int total, string message)
        {
            ProgressChanged?.Invoke(this, new MultiThreadProgressEventArgs
            {
                ActiveThreads = activeThreads,
                CompletedUrls = completed,
                TotalUrls = total,
                CurrentMessage = message
            });
        }
    }
}
