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
        public bool NoClobber { get; set; } = false;

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
                    var settings = JsonConvert.DeserializeObject<AppSettings>(json);
                    if (settings != null)
                    {
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
        private void ValidateAndFix()
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
