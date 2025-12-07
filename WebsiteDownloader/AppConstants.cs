using System;
using System.IO;

namespace WebsiteDownloader
{
    /// <summary>
    /// Centralized application constants to avoid magic strings.
    /// </summary>
    public static class AppConstants
    {
        /// <summary>
        /// The application name.
        /// </summary>
        public const string AppName = "WebsiteDownloader";

        /// <summary>
        /// The settings file name.
        /// </summary>
        public const string SettingsFileName = "settings.json";

        /// <summary>
        /// The history file name.
        /// </summary>
        public const string HistoryFileName = "history.json";

        /// <summary>
        /// The default user agent string for wget requests.
        /// </summary>
        public const string DefaultUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36";

        /// <summary>
        /// The prefix for extracted wget executable files.
        /// </summary>
        public const string WgetFilePrefix = "WebsiteDownloader_wget_";

        /// <summary>
        /// Maximum number of history items to retain.
        /// </summary>
        public const int MaxHistoryItems = 50;

        /// <summary>
        /// Gets the application data folder path.
        /// </summary>
        public static string AppDataFolder => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            AppName);

        /// <summary>
        /// Gets the full path to the settings file.
        /// </summary>
        public static string SettingsFilePath => Path.Combine(AppDataFolder, SettingsFileName);

        /// <summary>
        /// Gets the full path to the history file.
        /// </summary>
        public static string HistoryFilePath => Path.Combine(AppDataFolder, HistoryFileName);
    }
}
