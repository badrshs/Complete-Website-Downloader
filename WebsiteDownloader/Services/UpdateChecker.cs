using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WebsiteDownloader.Services
{
    /// <summary>
    /// Result of an update check.
    /// </summary>
    public class UpdateCheckResult
    {
        public bool UpdateAvailable { get; set; }
        public string CurrentVersion { get; set; }
        public string LatestVersion { get; set; }
        public string ReleaseUrl { get; set; }
        public string ReleaseNotes { get; set; }
        public DateTime PublishedDate { get; set; }
        public string DownloadUrl { get; set; }
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Checks for application updates from GitHub releases.
    /// </summary>
    public class UpdateChecker
    {
        private readonly IAppLogger _logger;
        private readonly HttpClient _httpClient;

        public UpdateChecker(IAppLogger logger = null)
        {
            _logger = logger ?? NullLogger.Instance;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", AppConstants.AppName);
        }

        /// <summary>
        /// Gets the current application version.
        /// </summary>
        public static Version CurrentVersion
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                var version = assembly.GetName().Version;
                return version ?? new Version(1, 0, 0);
            }
        }

        /// <summary>
        /// Checks GitHub for the latest release.
        /// </summary>
        /// <returns>Update check result.</returns>
        public async Task<UpdateCheckResult> CheckForUpdatesAsync()
        {
            var result = new UpdateCheckResult
            {
                CurrentVersion = CurrentVersion.ToString(3)
            };

            try
            {
                string apiUrl = string.Format(
                    AppConstants.GitHubReleasesApi,
                    AppConstants.GitHubOwner,
                    AppConstants.GitHubRepo);

                _logger.Debug($"Checking for updates at: {apiUrl}");

                var response = await _httpClient.GetStringAsync(apiUrl);
                var release = JObject.Parse(response);

                string tagName = release["tag_name"]?.ToString() ?? "";
                string releaseName = release["name"]?.ToString() ?? "";
                string releaseNotes = release["body"]?.ToString() ?? "";
                string htmlUrl = release["html_url"]?.ToString() ?? "";
                string publishedAt = release["published_at"]?.ToString() ?? "";

                // Parse version from tag (remove 'v' prefix if present)
                string versionString = tagName.TrimStart('v', 'V');
                
                // Handle version formats like "2.0.0" or "2.0"
                if (!Version.TryParse(versionString, out Version latestVersion))
                {
                    // Try appending .0 for two-part versions
                    if (Version.TryParse(versionString + ".0", out latestVersion))
                    {
                        // Success with appended .0
                    }
                    else
                    {
                        _logger.Warning($"Could not parse version from tag: {tagName}");
                        result.ErrorMessage = "Could not parse version information";
                        return result;
                    }
                }

                result.LatestVersion = latestVersion.ToString(3);
                result.ReleaseUrl = htmlUrl;
                result.ReleaseNotes = releaseNotes;
                result.UpdateAvailable = latestVersion > CurrentVersion;

                if (DateTime.TryParse(publishedAt, out DateTime publishedDate))
                {
                    result.PublishedDate = publishedDate;
                }

                // Try to find download URL for the executable
                var assets = release["assets"] as JArray;
                if (assets != null)
                {
                    foreach (var asset in assets)
                    {
                        string name = asset["name"]?.ToString() ?? "";
                        if (name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) ||
                            name.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                        {
                            result.DownloadUrl = asset["browser_download_url"]?.ToString();
                            break;
                        }
                    }
                }

                _logger.Info($"Update check complete. Current: {result.CurrentVersion}, Latest: {result.LatestVersion}, Update available: {result.UpdateAvailable}");

                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.Warning($"Failed to check for updates (network error): {ex.Message}");
                result.ErrorMessage = "Network error. Please check your internet connection.";
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to check for updates: {ex.Message}", ex);
                result.ErrorMessage = ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Opens the release page in the default browser.
        /// </summary>
        public static void OpenReleasePage(string url)
        {
            if (string.IsNullOrEmpty(url)) return;

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch
            {
                // Ignore errors opening browser
            }
        }

        /// <summary>
        /// Checks if enough time has passed since the last update check.
        /// </summary>
        public static bool ShouldCheckForUpdates(DateTime lastCheckDate)
        {
            if (lastCheckDate == DateTime.MinValue)
                return true;

            return (DateTime.Now - lastCheckDate).TotalDays >= AppConstants.UpdateCheckIntervalDays;
        }
    }
}
