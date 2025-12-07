namespace WebsiteDownloader.Resources
{
    /// <summary>
    /// Centralized string resources for localization support.
    /// Replace with .resx file-based resources for full localization.
    /// </summary>
    public static class Strings
    {
        // Application
        public const string AppTitle = "Website Downloader";
        
        // Status messages
        public const string StatusReady = "Ready";
        public const string StatusDownloading = "Downloading...";
        
        // Download messages
        public const string DownloadStarting = "Starting download: {0}";
        public const string DownloadOutputFolder = "Output folder: {0}";
        public const string DownloadCancelled = "Download cancelled by user.";
        public const string DownloadCancelledShort = "Download cancelled.";
        public const string DownloadCancelling = "Cancelling download...";
        public const string DownloadCompleteSuccess = "✓ Download completed successfully! Duration: {0}";
        public const string DownloadCompleteWithIssues = "  ⚠ {0} error(s), {1} warning(s) detected - check the Issues tab for details";
        public const string DownloadCompleteExitCode = "  (wget exit code {0} - some resources may have been unavailable)";
        public const string DownloadFailed = "✗ Download failed. wget exit code: {0}";
        public const string DownloadFailedCheckIssues = "  Check the Issues tab for {0} error(s), {1} warning(s)";
        
        // Notifications
        public const string NotifyDownloadSuccess = "Website downloaded successfully!\n{0}";
        public const string NotifyDownloadFailed = "Download failed for {0}";
        public const string NotifyDownloadComplete = "Download Complete";
        public const string NotifyDownloadFailedTitle = "Download Failed";
        
        // Validation errors
        public const string ValidationEnterUrl = "Please enter a website URL.";
        public const string ValidationInvalidUrl = "Please enter a valid HTTP or HTTPS URL.\n\nExample: https://example.com";
        public const string ValidationSelectFolder = "Please select an output folder.";
        public const string ValidationUserAgentEmpty = "User Agent cannot be empty.";
        public const string ValidationRateLimitInvalid = "Invalid rate limit format.\n\nExpected format: number followed by optional k, m, or g\nExamples: 200k, 1m, 500";
        public const string ValidationMaxDepthInvalid = "Max depth must be between 0 and 100 (0 = unlimited).";
        public const string ValidationWaitInvalid = "Wait between requests must be between 0 and 300 seconds.";
        public const string ValidationError = "Validation Error";
        
        // Dialogs
        public const string ConfirmExitTitle = "Download in Progress";
        public const string ConfirmExitMessage = "A download is in progress. Are you sure you want to exit?";
        public const string ConfirmResetSettingsTitle = "Reset Settings";
        public const string ConfirmResetSettingsMessage = "Are you sure you want to reset all settings to their default values?";
        
        // Errors
        public const string ErrorTitle = "Error";
        public const string ErrorInitializationTitle = "Initialization Error";
        public const string ErrorInitializationMessage = "Failed to initialize wget: {0}\n\nThe application cannot function without wget.exe.";
        public const string ErrorDownloadFailed = "Download failed: {0}";
        
        // Folder browser
        public const string FolderBrowserDescription = "Select output folder for downloaded website";
        
        // Issues tab
        public const string IssuesTabHeader = "⚠ Issues ({0})";
        public const string IssuesStatusFormat = "  |  ⚠ {0} error(s), {1} warning(s)";
        
        // Placeholder text
        public const string UrlPlaceholder = "https://example.com";
    }
}
