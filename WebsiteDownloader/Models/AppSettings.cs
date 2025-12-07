using System;
using System.IO;
using Newtonsoft.Json;

namespace WebsiteDownloader.Models
{
    /// <summary>
    /// Application settings that persist between sessions
    /// </summary>
    public class AppSettings
    {
        private static readonly string SettingsFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "WebsiteDownloader",
            "settings.json");

        // Download settings
        public string DefaultOutputFolder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public string UserAgent { get; set; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36";
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
                if (File.Exists(SettingsFilePath))
                {
                    string json = File.ReadAllText(SettingsFilePath);
                    return JsonConvert.DeserializeObject<AppSettings>(json) ?? new AppSettings();
                }
            }
            catch
            {
                // Return defaults if file is corrupted
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
                string directory = Path.GetDirectoryName(SettingsFilePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(SettingsFilePath, json);
            }
            catch
            {
                // Settings save is best-effort
            }
        }
    }
}
