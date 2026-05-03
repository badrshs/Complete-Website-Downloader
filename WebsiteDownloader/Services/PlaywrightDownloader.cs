using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebsiteDownloader.Services
{
    /// <summary>
    /// Downloads websites using Playwright/Crawlee (Node.js) for full JavaScript rendering.
    /// Falls back gracefully with clear error messages when Node.js is not available.
    /// </summary>
    public class PlaywrightDownloader : IWebsiteDownloader
    {
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
        /// Whether to strip analytics/tracking scripts from saved HTML for cleaner offline viewing.
        /// </summary>
        public bool StripAnalyticsScripts { get; set; } = true;

        /// <summary>
        /// Path to the crawler runtime directory in AppData.
        /// </summary>
        private static string CrawlerDir => Path.Combine(AppConstants.AppDataFolder, "crawler");

        /// <summary>
        /// Path to the crawler script.
        /// </summary>
        private static string CrawlerScriptPath => Path.Combine(CrawlerDir, "crawler.mjs");

        public PlaywrightDownloader(IAppLogger logger = null)
        {
            _logger = logger ?? NullLogger.Instance;
            _logger.Debug("PlaywrightDownloader initialized");
        }

        /// <summary>
        /// Checks whether the Playwright engine requirements are met.
        /// </summary>
        public static EngineRequirements CheckRequirements()
        {
            var result = new EngineRequirements();

            // Check Node.js
            result.NodeInstalled = IsCommandAvailable("node", "--version");
            if (result.NodeInstalled)
            {
                result.NodeVersion = GetCommandOutput("node", "--version")?.Trim();
            }

            // Check npm (on Windows it's npm.cmd, use cmd /c)
            result.NpmInstalled = IsCommandAvailable("cmd.exe", "/c npm --version");

            // Check if crawler is set up
            result.CrawlerInstalled = File.Exists(CrawlerScriptPath)
                && Directory.Exists(Path.Combine(CrawlerDir, "node_modules"));

            return result;
        }

        /// <summary>
        /// Sets up the crawler environment (installs npm packages + creates script).
        /// </summary>
        public async Task<bool> SetupAsync(Action<string> progressCallback, CancellationToken cancellationToken)
        {
            try
            {
                // Ensure directory exists
                if (!Directory.Exists(CrawlerDir))
                    Directory.CreateDirectory(CrawlerDir);

                progressCallback?.Invoke("Creating crawler project...");

                // Write package.json
                var packageJson = GetPackageJson();
                File.WriteAllText(Path.Combine(CrawlerDir, "package.json"), packageJson);

                // Write crawler script
                var script = GetCrawlerScript();
                File.WriteAllText(CrawlerScriptPath, script);

                progressCallback?.Invoke("Installing dependencies (crawlee + playwright)...");

                // Run npm install (use cmd /c on Windows since npm is a .cmd file)
                var npmResult = await RunProcessAsync("cmd.exe", "/c npm install", CrawlerDir, cancellationToken, progressCallback);
                if (!npmResult.Success)
                {
                    progressCallback?.Invoke($"npm install failed: {npmResult.Error}");
                    return false;
                }

                progressCallback?.Invoke("Installing Playwright browsers (downloading Chromium)...");

                // Install playwright browsers
                var pwResult = await RunProcessAsync("cmd.exe", "/c npx playwright install chromium", CrawlerDir, cancellationToken, progressCallback);
                if (!pwResult.Success)
                {
                    progressCallback?.Invoke($"Playwright browser install failed: {pwResult.Error}");
                    return false;
                }

                progressCallback?.Invoke("Setup complete!");
                return true;
            }
            catch (Exception ex)
            {
                progressCallback?.Invoke($"Setup failed: {ex.Message}");
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task DownloadAsync(DownloadOptions options, CancellationToken cancellationToken = default)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (_isDownloading) throw new InvalidOperationException("A download is already in progress.");

            _isDownloading = true;
            var startTime = DateTime.Now;

            _logger.Info($"Starting Playwright download: {options.Url}");

            try
            {
                var reqs = CheckRequirements();
                if (!reqs.NodeInstalled)
                    throw new InvalidOperationException("Node.js is not installed. Please install Node.js from https://nodejs.org/");

                if (!reqs.CrawlerInstalled)
                    throw new InvalidOperationException("Crawler not set up. Go to Settings → Advanced → Setup Playwright Engine.");

                // Always update the crawler script to latest version
                EnsureLatestScript();

                var outputFolder = Path.Combine(options.OutputFolder, options.Url.Host);

                // Build arguments for the crawler script
                var args = BuildScriptArgs(options);

                _logger.Debug($"Crawler args: {args}");

                _currentProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "node",
                        Arguments = args,
                        WorkingDirectory = CrawlerDir,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        StandardOutputEncoding = Encoding.UTF8,
                        StandardErrorEncoding = Encoding.UTF8
                    },
                    EnableRaisingEvents = true
                };

                _currentProcess.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        ParseCrawlerOutput(e.Data);
                    }
                };

                _currentProcess.ErrorDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data) && !e.Data.Contains("ExperimentalWarning"))
                    {
                        OnProgressChanged(e.Data);
                    }
                };

                _currentProcess.Start();
                _currentProcess.BeginOutputReadLine();
                _currentProcess.BeginErrorReadLine();

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
                        wasCancelled = true;
                    }
                }

                int exitCode = 0;
                try
                {
                    if (_currentProcess != null && _currentProcess.HasExited)
                        exitCode = _currentProcess.ExitCode;
                }
                catch (InvalidOperationException) { }

                var success = !wasCancelled && Directory.Exists(outputFolder);
                var duration = DateTime.Now - startTime;

                if (success)
                    _logger.Info($"Playwright download completed: {options.Url} (Duration: {duration:mm\\:ss})");
                else if (wasCancelled)
                    _logger.Warning($"Playwright download cancelled: {options.Url}");
                else
                    _logger.Error($"Playwright download failed: {options.Url} (Exit code: {exitCode})");

                OnDownloadCompleted(new DownloadCompletedEventArgs
                {
                    Success = success,
                    OutputFolder = outputFolder,
                    Url = options.Url,
                    Duration = duration,
                    Cancelled = wasCancelled,
                    ExitCode = exitCode
                });
            }
            finally
            {
                CleanupProcess();
                _isDownloading = false;
            }
        }

        /// <inheritdoc/>
        public void CancelDownload()
        {
            lock (_processLock)
            {
                try
                {
                    if (_currentProcess == null || _currentProcess.HasExited)
                        return;

                    _currentProcess.Kill();
                    _currentProcess.WaitForExit(5000);
                }
                catch (InvalidOperationException) { }
                catch (System.ComponentModel.Win32Exception) { }
            }
        }

        private string BuildScriptArgs(DownloadOptions options)
        {
            var sb = new StringBuilder();
            sb.Append($"\"{CrawlerScriptPath}\" ");
            sb.Append($"--url \"{options.Url}\" ");
            sb.Append($"--output \"{options.OutputFolder}\" ");
            sb.Append($"--depth {(options.MaxDepth > 0 ? options.MaxDepth : 50)} ");

            if (options.WaitBetweenRequests > 0)
                sb.Append($"--wait {options.WaitBetweenRequests * 1000} ");

            if (options.ConvertLinks)
                sb.Append("--convert-links ");

            // Try sitemap-based discovery for better coverage
            sb.Append("--use-sitemap ");

            if (StripAnalyticsScripts)
                sb.Append("--strip-analytics ");

            return sb.ToString();
        }

        private void ParseCrawlerOutput(string line)
        {
            // Our crawler script outputs structured lines: [TYPE] message
            if (line.StartsWith("[SAVED]"))
            {
                var msg = line.Substring(7).Trim();
                OnProgressChanged($"Saving to: {msg}");
            }
            else if (line.StartsWith("[QUEUE]"))
            {
                var msg = line.Substring(7).Trim();
                OnProgressChanged($"Discovered: {msg}");
            }
            else if (line.StartsWith("[STATUS]"))
            {
                var msg = line.Substring(8).Trim();
                OnProgressChanged(msg);
            }
            else if (line.StartsWith("[ERROR]"))
            {
                var msg = line.Substring(7).Trim();
                OnProgressChanged($"ERROR: {msg}");
            }
            else if (line.StartsWith("[DONE]"))
            {
                var msg = line.Substring(6).Trim();
                OnProgressChanged($"FINISHED -- {msg}");
            }
            else
            {
                OnProgressChanged(line);
            }
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
        /// Ensures the crawler script on disk is the latest version.
        /// </summary>
        private static void EnsureLatestScript()
        {
            var script = GetCrawlerScript();
            if (!Directory.Exists(CrawlerDir))
                Directory.CreateDirectory(CrawlerDir);
            File.WriteAllText(CrawlerScriptPath, script);
        }

        private static bool IsCommandAvailable(string command, string args)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var proc = Process.Start(psi))
                {
                    proc.WaitForExit(5000);
                    return proc.ExitCode == 0;
                }
            }
            catch
            {
                return false;
            }
        }

        private static string GetCommandOutput(string command, string args)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                using (var proc = Process.Start(psi))
                {
                    var output = proc.StandardOutput.ReadToEnd();
                    proc.WaitForExit(5000);
                    return output;
                }
            }
            catch
            {
                return null;
            }
        }

        private static async Task<ProcessResult> RunProcessAsync(string fileName, string args, string workDir, CancellationToken ct, Action<string> outputCallback = null)
        {
            var psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = args,
                WorkingDirectory = workDir,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            var outputBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();

            using (var proc = new Process { StartInfo = psi, EnableRaisingEvents = true })
            {
                proc.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        outputBuilder.AppendLine(e.Data);
                        outputCallback?.Invoke(e.Data);
                    }
                };

                proc.ErrorDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        errorBuilder.AppendLine(e.Data);
                        outputCallback?.Invoke(e.Data);
                    }
                };

                proc.Start();
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();

                using (ct.Register(() => { try { proc.Kill(); } catch { } }))
                {
                    await Task.Run(() => proc.WaitForExit()).ConfigureAwait(false);
                }

                return new ProcessResult
                {
                    Success = proc.ExitCode == 0,
                    Output = outputBuilder.ToString(),
                    Error = errorBuilder.ToString()
                };
            }
        }

        private static string GetPackageJson()
        {
            return @"{
  ""name"": ""website-downloader-crawler"",
  ""version"": ""1.0.0"",
  ""type"": ""module"",
  ""private"": true,
  ""dependencies"": {
    ""crawlee"": ""^3.8.0"",
    ""playwright"": ""^1.42.0""
  }
}";
        }

        private static string GetCrawlerScript()
        {
            return @"import { PlaywrightCrawler } from 'crawlee';
import { writeFile, mkdir, readFile } from 'fs/promises';
import { dirname, join } from 'path';
import { URL } from 'url';
import https from 'https';
import http from 'http';

// Parse CLI arguments
const args = process.argv.slice(2);
function getArg(name) {
    const idx = args.indexOf(name);
    return idx >= 0 && idx + 1 < args.length ? args[idx + 1] : null;
}
function hasFlag(name) {
    return args.includes(name);
}

const startUrl = getArg('--url');
const outputBase = getArg('--output');
const maxDepth = parseInt(getArg('--depth') || '50', 10);
const waitMs = parseInt(getArg('--wait') || '0', 10);
const convertLinks = hasFlag('--convert-links');
const useSitemap = hasFlag('--use-sitemap');
const stripAnalytics = hasFlag('--strip-analytics');

if (!startUrl || !outputBase) {
    console.error('Usage: node crawler.mjs --url <url> --output <dir> [--depth N] [--wait ms] [--convert-links] [--use-sitemap]');
    process.exit(1);
}

const startUrlObj = new URL(startUrl);
const hostDir = join(outputBase, startUrlObj.hostname);

// Use directory path for filtering
let basePath = startUrlObj.pathname;
if (!basePath.endsWith('/')) {
    const lastSlash = basePath.lastIndexOf('/');
    basePath = basePath.substring(0, lastSlash + 1);
}

let savedCount = 0;
let assetCount = 0;
const savedAssets = new Set();

// Download a URL as text
function fetchText(url) {
    return new Promise((resolve, reject) => {
        const lib = url.startsWith('https') ? https : http;
        lib.get(url, { headers: { 'User-Agent': 'Mozilla/5.0' } }, (res) => {
            if (res.statusCode !== 200) { reject(new Error(`HTTP ${res.statusCode}`)); return; }
            let data = '';
            res.on('data', chunk => data += chunk);
            res.on('end', () => resolve(data));
        }).on('error', reject);
    });
}

// Parse sitemap XML and return all <loc> URLs
function parseSitemapUrls(xml) {
    const urls = [];
    const locRegex = /<loc>([^<]+)<\/loc>/g;
    let match;
    while ((match = locRegex.exec(xml)) !== null) {
        urls.push(match[1].trim());
    }
    return urls;
}

// Discover URLs from sitemap
async function discoverFromSitemap() {
    const sitemapPaths = ['/sitemap.xml', '/sitemap-index.xml', '/sitemap_index.xml'];
    const baseUrl = `${startUrlObj.protocol}//${startUrlObj.host}`;
    
    for (const path of sitemapPaths) {
        try {
            console.log(`[STATUS] Checking for sitemap at ${baseUrl}${path}`);
            const xml = await fetchText(`${baseUrl}${path}`);
            
            // Check if it's a sitemap index
            if (xml.includes('<sitemapindex')) {
                console.log(`[STATUS] Found sitemap index, parsing sub-sitemaps...`);
                const subUrls = parseSitemapUrls(xml);
                let allUrls = [];
                for (const subUrl of subUrls) {
                    try {
                        console.log(`[STATUS] Parsing ${subUrl}...`);
                        const subXml = await fetchText(subUrl);
                        allUrls = allUrls.concat(parseSitemapUrls(subXml));
                    } catch (e) {
                        console.log(`[ERROR] Failed to parse ${subUrl}: ${e.message}`);
                    }
                }
                return allUrls;
            }
            
            // Regular sitemap
            return parseSitemapUrls(xml);
        } catch {
            continue;
        }
    }
    return [];
}

// Save a network response to disk
async function saveAsset(url, body) {
    try {
        const u = new URL(url);
        if (u.hostname !== startUrlObj.hostname) return;
        
        let assetPath = u.pathname;
        if (savedAssets.has(assetPath)) return;
        savedAssets.add(assetPath);
        
        const fullPath = join(hostDir, assetPath);
        await mkdir(dirname(fullPath), { recursive: true });
        await writeFile(fullPath, body);
        assetCount++;
    } catch {}
}

// Calculate relative path from one file's directory to another path
function relPath(fromDir, toPath) {
    const fromParts = fromDir.split('/').filter(Boolean);
    const toParts = toPath.split('/').filter(Boolean);
    
    // Find common prefix length
    let common = 0;
    while (common < fromParts.length && common < toParts.length && fromParts[common] === toParts[common]) {
        common++;
    }
    
    // Build relative path: go up from fromDir, then down to toPath
    const ups = fromParts.length - common;
    const rel = (ups > 0 ? '../'.repeat(ups) : './') + toParts.slice(common).join('/');
    return rel;
}

// Convert absolute URLs in HTML to relative paths for offline viewing
function convertLinksToRelative(html, pageDir) {
    const origin = startUrlObj.protocol + '//' + startUrlObj.host;
    const escapedOrigin = origin.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
    
    // Replace full URLs (https://host/path) with relative paths
    const fullUrlRe = new RegExp(escapedOrigin + '(/[^\\s' + String.fromCharCode(34) + String.fromCharCode(39) + '>]*)', 'g');
    html = html.replace(fullUrlRe, function(m, p) {
        return relPath(pageDir, p);
    });
    
    // Replace ALL remaining absolute paths starting with / in quoted contexts
    // This catches href, src, import(), url(), and any other references
    const dq = String.fromCharCode(34);
    const sq = String.fromCharCode(39);
    
    html = html.replace(new RegExp(dq + '(\/(?!\/)[^' + dq + ']*)' + dq, 'g'), function(m, p) {
        // Skip data URIs and protocol-relative
        if (p.startsWith('/data:') || p.startsWith('//')) return m;
        return dq + relPath(pageDir, p) + dq;
    });
    
    // Single-quoted: '/<path>'
    html = html.replace(new RegExp(sq + '(\/(?!\/)[^' + sq + ']*)' + sq, 'g'), function(m, p) {
        if (p.startsWith('/data:') || p.startsWith('//')) return m;
        return sq + relPath(pageDir, p) + sq;
    });
    
    // Remove external tracking/analytics scripts that won't work offline
    if (stripAnalytics) {
        html = html.replace(/<script[^>]*src=[^>]*cdn-cgi[^>]*><\/script>/gi, '');
        html = html.replace(/<script[^>]*src=[^>]*cloudflareinsights[^>]*><\/script>/gi, '');
        html = html.replace(/<script[^>]*>[^<]*zaraz[^<]*<\/script>/gi, '');
        html = html.replace(/<script[^>]*>[^<]*faro-web-sdk[^<]*<\/script>/gi, '');
        html = html.replace(/<script[^>]*>[^<]*grafana[^<]*<\/script>/gi, '');
        html = html.replace(/<script[^>]*src=[^>]*faro[^>]*><\/script>/gi, '');
        html = html.replace(/<script[^>]*src=[^>]*google-analytics[^>]*><\/script>/gi, '');
        html = html.replace(/<script[^>]*src=[^>]*googletagmanager[^>]*><\/script>/gi, '');
        html = html.replace(/<script[^>]*>[^<]*gtag\([^<]*<\/script>/gi, '');
        html = html.replace(/<script[^>]*>[^<]*google-analytics[^<]*<\/script>/gi, '');
        html = html.replace(/<script[^>]*src=[^>]*hotjar[^>]*><\/script>/gi, '');
        html = html.replace(/<script[^>]*src=[^>]*facebook[^>]*fbevents[^>]*><\/script>/gi, '');
        html = html.replace(/<noscript[^>]*>[^<]*facebook[^<]*<\/noscript>/gi, '');
    }
    
    return html;
}

// Collect starting URLs
let startUrls = [startUrl];

if (useSitemap) {
    console.log(`[STATUS] Discovering pages via sitemap...`);
    const sitemapUrls = await discoverFromSitemap();
    
    if (sitemapUrls.length > 0) {
        // Filter by base path
        const filtered = basePath === '/' 
            ? sitemapUrls 
            : sitemapUrls.filter(u => {
                try { return new URL(u).pathname.startsWith(basePath); } 
                catch { return false; }
            });
        console.log(`[STATUS] Found ${sitemapUrls.length} total URLs in sitemap, ${filtered.length} match path ${basePath}`);
        if (filtered.length > 0) startUrls = filtered;
    } else {
        console.log(`[STATUS] No sitemap found, using link discovery instead`);
    }
}

console.log(`[STATUS] Crawling ${startUrls.length} URLs (max depth: ${maxDepth}, base path: ${basePath})`);

const crawler = new PlaywrightCrawler({
    maxRequestsPerCrawl: 50000,
    maxConcurrency: 3,
    requestHandlerTimeoutSecs: 60,
    navigationTimeoutSecs: 30,
    
    preNavigationHooks: [
        async ({ page }) => {
            // Intercept ALL same-host responses to save assets
            page.on('response', async (response) => {
                try {
                    const url = response.url();
                    const status = response.status();
                    if (status < 200 || status >= 300) return;
                    
                    const u = new URL(url);
                    if (u.hostname !== startUrlObj.hostname) return;
                    
                    // Skip HTML pages (those are saved by the requestHandler)
                    const contentType = response.headers()['content-type'] || '';
                    if (contentType.match(/^text\/html/i)) return;
                    
                    const body = await response.body().catch(() => null);
                    if (body) await saveAsset(url, body);
                } catch {}
            });
        }
    ],

    async requestHandler({ page, request, enqueueLinks }) {
        const url = new URL(request.url);
        
        // Only process same-host pages under the base path
        if (url.hostname !== startUrlObj.hostname) return;
        if (basePath !== '/' && !url.pathname.startsWith(basePath)) return;

        // Wait for content to render
        await page.waitForLoadState('networkidle').catch(() => {});

        if (waitMs > 0) {
            await new Promise(r => setTimeout(r, waitMs));
        }

        // Get rendered HTML
        let html = await page.content();
        
        // Determine save path
        let savePath = url.pathname;
        if (savePath.endsWith('/')) savePath += 'index.html';
        else if (!savePath.match(/\.[a-z0-9]+$/i)) savePath += '/index.html';
        
        // Convert absolute paths to relative for offline viewing
        const pageDir = dirname(savePath);
        html = convertLinksToRelative(html, pageDir);
        
        const fullPath = join(hostDir, savePath);
        
        // Save HTML file
        await mkdir(dirname(fullPath), { recursive: true });
        await writeFile(fullPath, html, 'utf-8');
        savedCount++;
        console.log(`[SAVED] ${savePath} (${savedCount} pages, ${assetCount} assets)`);

        // Also follow links for discovery (in case sitemap is incomplete)
        await enqueueLinks({
            strategy: 'same-domain',
            transformRequestFunction: (req) => {
                try {
                    const u = new URL(req.url);
                    if (basePath !== '/' && !u.pathname.startsWith(basePath)) return false;
                    if (u.pathname.match(/\.(css|js|png|jpg|jpeg|gif|svg|ico|woff2?|ttf|eot|webp|avif|pdf|zip)$/i)) return false;
                } catch { return false; }
                return req;
            }
        });
    },

    failedRequestHandler({ request, error }) {
        console.log(`[ERROR] ${request.url}: ${error.message}`);
    },
});

await crawler.run(startUrls);
console.log(`[DONE] Downloaded ${savedCount} pages + ${assetCount} assets from ${startUrlObj.hostname}`);
process.exit(0);
";
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                CancelDownload();
                CleanupProcess();
            }
        }
    }

    /// <summary>
    /// Requirements check result for the Playwright engine.
    /// </summary>
    public class EngineRequirements
    {
        public bool NodeInstalled { get; set; }
        public string NodeVersion { get; set; }
        public bool NpmInstalled { get; set; }
        public bool CrawlerInstalled { get; set; }

        public bool IsReady => NodeInstalled && NpmInstalled && CrawlerInstalled;

        public string GetStatusMessage()
        {
            if (IsReady)
                return $"Ready (Node.js {NodeVersion})";
            if (!NodeInstalled)
                return "Node.js not found. Install from https://nodejs.org/";
            if (!NpmInstalled)
                return "npm not found. Reinstall Node.js.";
            return "Crawler not set up. Click 'Setup' to install.";
        }
    }

    /// <summary>
    /// Simple process execution result.
    /// </summary>
    internal class ProcessResult
    {
        public bool Success { get; set; }
        public string Output { get; set; }
        public string Error { get; set; }
    }
}
