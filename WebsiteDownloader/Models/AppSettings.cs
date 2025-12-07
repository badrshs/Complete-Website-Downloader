using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace WebsiteDownloader.Models
{
    /// <summary>
    /// Application settings that persist between sessions
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
        /// Loads settings from disk or returns default settings
        /// </summary>
        public static AppSettings Load()
        {
            try
            {
                if (File.Exists(AppConstants.SettingsFilePath))
                {
                    string json = File.ReadAllText(AppConstants.SettingsFilePath);
                    return JsonConvert.DeserializeObject<AppSettings>(json) ?? new AppSettings();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load settings: {ex.Message}");
            }

            return new AppSettings();
        }

        /// <summary>
        /// Saves current settings to disk
        /// </summary>
        public void Save()
        {
            try
            {
                if (!Directory.Exists(AppConstants.AppDataFolder))
                    Directory.CreateDirectory(AppConstants.AppDataFolder);

                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(AppConstants.SettingsFilePath, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to save settings: {ex.Message}");
            }
        }
    }
}
