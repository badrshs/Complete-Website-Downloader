using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace WebsiteDownloader.Models
{
    /// <summary>
    /// Application settings that persist between sessions.
    /// Provides robust loading/saving with fallback to defaults on errors.
    /// </summary>
    public class AppSettings
    {
        // Download settings
        public string DefaultOutputFolder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public string UserAgent { get; set; } = AppConstants.DefaultUserAgent;
        public bool ConvertLinksForOffline { get; set; } = true;
        public bool AdjustExtensions { get; set; } = true;
        public int MaxDepth { get; set; } = 0;  // 0 = unlimited
        public int WaitBetweenRequests { get; set; } = 0;  // seconds
        public string RateLimit { get; set; } = "";  // e.g., "200k"
        // Legacy resume flags. Kept only for backward-compatible deserialization of older
        // settings files (migrated into ResumeMode on load); new code reads ResumeMode below.
        public bool NoClobber { get; set; } = false;
        public bool ContinueDownload { get; set; } = true;  // Resume interrupted downloads (-c)

        // Resume/skip behaviour for the wget-based engines. Replaces the two booleans above.
        // Timestamping (-N) is the safest default for continuing an interrupted mirror:
        // it skips files already up-to-date and only re-fetches missing/changed ones.
        // New installs default to Timestamping (-N). Older settings files that predate this
        // field are migrated from the legacy booleans in Load() before this default applies.
        public Services.ResumeMode ResumeMode { get; set; } = Services.ResumeMode.Timestamping;

        public bool IgnoreSslErrors { get; set; } = false;  // Skip certificate validation
        public int ConnectionTimeout { get; set; } = 30;    // Connection timeout in seconds
        public int ReadTimeout { get; set; } = 60;          // Read timeout in seconds
        public int RetryCount { get; set; } = 3;            // Number of retries on failure

        // Recursion scope and filters
        public bool NoParent { get; set; } = false;            // -np: never ascend above the start URL
        public bool SpanHosts { get; set; } = false;            // -H: allow crossing to other hosts
        public string DomainList { get; set; } = "";            // --domains: comma-separated allowed domains
        public string AcceptFileTypes { get; set; } = "";       // -A: comma-separated accepted suffixes
        public string RejectFileTypes { get; set; } = "";       // -R: comma-separated rejected suffixes
        public string IncludeDirectories { get; set; } = "";    // -I: comma-separated directories to follow
        public string ExcludeDirectories { get; set; } = "";    // -X: comma-separated directories to skip
        public bool IgnoreFilterCase { get; set; } = false;     // --ignore-case for the filters above
        public string DownloadQuota { get; set; } = "";         // --quota: total size cap, e.g. "500m"
        public int MaxRedirect { get; set; } = 20;              // --max-redirect (20 = wget default)

        // Authentication / request headers
        public string HttpUser { get; set; } = "";              // --user
        public string HttpPassword { get; set; } = "";          // --password (stored in plain text)
        public string CookiesFilePath { get; set; } = "";       // --load-cookies: browser-exported cookies file
        public bool KeepSessionCookies { get; set; } = false;   // --keep-session-cookies
        public string CustomHeaders { get; set; } = "";         // --header: one "Name: value" per line
        public string Referer { get; set; } = "";               // --referer

        // Download behaviour
        public bool RandomWait { get; set; } = false;            // --random-wait: vary the wait time
        public bool ContentDisposition { get; set; } = false;    // --content-disposition: server file names
        public Services.DirectoryStructure DirectoryStructure { get; set; } = Services.DirectoryStructure.Default;
        
        // Post-download options
        public bool ExportToZip { get; set; } = false;      // Zip the downloaded folder
        public bool DeleteAfterZip { get; set; } = false;   // Delete original folder after zipping
        
        // Advanced options
        public bool EnableMultiThreaded { get; set; } = false;  // Use multiple wget instances
        public int ThreadCount { get; set; } = 4;               // Number of parallel downloads
        
        // Download engine
        public Services.DownloadEngine Engine { get; set; } = Services.DownloadEngine.Wget;
        
        // Playwright-specific settings
        public bool StripAnalyticsScripts { get; set; } = true;  // Remove tracking/analytics scripts for offline viewing
        
        // Bandwidth scheduler
        public bool EnableBandwidthScheduler { get; set; } = false;
        public string PeakHoursRateLimit { get; set; } = "100k";   // Rate during peak hours
        public string OffPeakRateLimit { get; set; } = "";         // Rate during off-peak (empty = unlimited)
        public int PeakHoursStart { get; set; } = 9;               // Peak starts at 9 AM
        public int PeakHoursEnd { get; set; } = 17;                // Peak ends at 5 PM
        
        // Auto-update
        public bool CheckForUpdates { get; set; } = true;
        public DateTime LastUpdateCheck { get; set; } = DateTime.MinValue;

        // UI settings
        public bool OpenFolderAfterDownload { get; set; } = true;
        public bool MinimizeToTray { get; set; } = false;
        public bool ShowNotifications { get; set; } = true;

        // Window state
        public int WindowX { get; set; } = -1;
        public int WindowY { get; set; } = -1;
        public int WindowWidth { get; set; } = 600;
        public int WindowHeight { get; set; } = 400;

        /// <summary>
        /// Event raised when settings fail to save, allowing UI to notify user.
        /// </summary>
        public static event EventHandler<SettingsSaveErrorEventArgs> SaveFailed;

        /// <summary>
        /// Loads settings from disk or returns default settings.
        /// </summary>
        /// <returns>Loaded settings or defaults if loading fails.</returns>
        public static AppSettings Load()
        {
            try
            {
                if (File.Exists(AppConstants.SettingsFilePath))
                {
                    string json = File.ReadAllText(AppConstants.SettingsFilePath);
                    // Detect whether this file predates the ResumeMode field, so we can
                    // migrate it from the legacy NoClobber/ContinueDownload booleans.
                    bool hasResumeMode = json.IndexOf("\"ResumeMode\"", StringComparison.OrdinalIgnoreCase) >= 0;
                    var settings = JsonConvert.DeserializeObject<AppSettings>(json);
                    if (settings != null)
                    {
                        if (!hasResumeMode)
                            settings.ResumeMode = Services.ResumeModeExtensions.FromLegacyFlags(
                                settings.NoClobber, settings.ContinueDownload);

                        // Validate loaded settings
                        settings.ValidateAndFix();
                        return settings;
                    }
                }
            }
            catch (JsonException ex)
            {
                Debug.WriteLine($"Settings file corrupted, using defaults: {ex.Message}");
                // Could optionally backup the corrupted file here
            }
            catch (IOException ex)
            {
                Debug.WriteLine($"Failed to read settings file: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unexpected error loading settings: {ex.Message}");
            }

            return new AppSettings();
        }

        /// <summary>
        /// Saves current settings to disk.
        /// </summary>
        /// <returns>True if save succeeded, false otherwise.</returns>
        public bool Save()
        {
            try
            {
                // Ensure directory exists
                if (!Directory.Exists(AppConstants.AppDataFolder))
                    Directory.CreateDirectory(AppConstants.AppDataFolder);

                // Use atomic write pattern
                string tempPath = AppConstants.SettingsFilePath + ".tmp";
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                
                File.WriteAllText(tempPath, json);
                
                // Atomic replace
                if (File.Exists(AppConstants.SettingsFilePath))
                {
                    File.Delete(AppConstants.SettingsFilePath);
                }
                File.Move(tempPath, AppConstants.SettingsFilePath);
                
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to save settings: {ex.Message}");
                SaveFailed?.Invoke(this, new SettingsSaveErrorEventArgs(ex));
                return false;
            }
        }

        /// <summary>
        /// Validates settings and fixes any invalid values with defaults.
        /// </summary>
        internal void ValidateAndFix()
        {
            // Ensure UserAgent is not empty
            if (string.IsNullOrWhiteSpace(UserAgent))
                UserAgent = AppConstants.DefaultUserAgent;

            // Ensure output folder exists or reset to desktop
            if (string.IsNullOrWhiteSpace(DefaultOutputFolder) || !Directory.Exists(DefaultOutputFolder))
                DefaultOutputFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Clamp numeric values
            if (MaxDepth < 0) MaxDepth = 0;
            if (MaxDepth > 100) MaxDepth = 100;
            if (WaitBetweenRequests < 0) WaitBetweenRequests = 0;
            if (WaitBetweenRequests > 300) WaitBetweenRequests = 300;
            
            // Validate new settings
            if (ConnectionTimeout < 5) ConnectionTimeout = 5;
            if (ConnectionTimeout > 300) ConnectionTimeout = 300;
            if (ReadTimeout < 10) ReadTimeout = 10;
            if (ReadTimeout > 600) ReadTimeout = 600;
            if (RetryCount < 0) RetryCount = 0;
            if (RetryCount > 20) RetryCount = 20;
            if (ThreadCount < 1) ThreadCount = 1;
            if (ThreadCount > 16) ThreadCount = 16;
            if (PeakHoursStart < 0 || PeakHoursStart > 23) PeakHoursStart = 9;
            if (PeakHoursEnd < 0 || PeakHoursEnd > 23) PeakHoursEnd = 17;
            if (MaxRedirect < 0) MaxRedirect = 20;
            if (MaxRedirect > 100) MaxRedirect = 100;

            // Normalize null strings (hand-edited or partial settings files) to empty
            DomainList = DomainList ?? "";
            AcceptFileTypes = AcceptFileTypes ?? "";
            RejectFileTypes = RejectFileTypes ?? "";
            IncludeDirectories = IncludeDirectories ?? "";
            ExcludeDirectories = ExcludeDirectories ?? "";
            HttpUser = HttpUser ?? "";
            HttpPassword = HttpPassword ?? "";
            CookiesFilePath = CookiesFilePath ?? "";
            CustomHeaders = CustomHeaders ?? "";
            Referer = Referer ?? "";

            // Quota uses the same size format as rate limit; reset if unparseable
            DownloadQuota = DownloadQuota ?? "";
            if (!Helpers.ValidationPatterns.RateLimit.IsMatch(DownloadQuota))
                DownloadQuota = "";

            // Hand-edited files may contain out-of-range enum values
            if (!System.Enum.IsDefined(typeof(Services.DirectoryStructure), DirectoryStructure))
                DirectoryStructure = Services.DirectoryStructure.Default;

            // Ensure window dimensions are reasonable
            if (WindowWidth < 400) WindowWidth = 600;
            if (WindowHeight < 300) WindowHeight = 400;
        }
    }

    /// <summary>
    /// Event arguments for settings save failures.
    /// </summary>
    public class SettingsSaveErrorEventArgs : EventArgs
    {
        public Exception Exception { get; }
        public string Message => Exception?.Message ?? "Unknown error";

        public SettingsSaveErrorEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
