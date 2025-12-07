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
        
        // History form
        public const string ConfirmClearHistoryTitle = "Clear History";
        public const string ConfirmClearHistoryMessage = "Are you sure you want to clear all download history?";
        
        // Success/failure indicators
        public const string StatusSuccess = "✓ Success";
        public const string StatusFailed = "✗ Failed";
        
        // Settings - New options
        public const string SettingsTabAdvanced = "Advanced";
        public const string SettingsTabScheduler = "Scheduler";
        
        // Continue/Resume
        public const string SettingsContinueDownload = "Resume interrupted downloads (-c)";
        public const string SettingsContinueDownloadHint = "Continue downloading partially retrieved files";
        
        // SSL
        public const string SettingsIgnoreSsl = "Ignore SSL certificate errors";
        public const string SettingsIgnoreSslHint = "Use for self-signed or expired certificates";
        
        // Timeout
        public const string SettingsConnectionTimeout = "Connection timeout (sec):";
        public const string SettingsReadTimeout = "Read timeout (sec):";
        public const string SettingsRetryCount = "Retry attempts:";
        
        // ZIP Export
        public const string SettingsExportZip = "Create ZIP archive after download";
        public const string SettingsDeleteAfterZip = "Delete original folder after ZIP";
        
        // Multi-threaded
        public const string SettingsMultiThreaded = "Enable multi-threaded download";
        public const string SettingsThreadCount = "Number of threads:";
        public const string SettingsMultiThreadedHint = "Parallel downloads for faster completion";
        
        // Bandwidth Scheduler
        public const string SettingsEnableScheduler = "Enable bandwidth scheduler";
        public const string SettingsPeakHoursLabel = "Peak hours:";
        public const string SettingsPeakRateLimit = "Peak rate limit:";
        public const string SettingsOffPeakRateLimit = "Off-peak rate limit:";
        public const string SettingsSchedulerHint = "Automatically adjust download speed based on time of day";
        
        // Auto-update
        public const string SettingsCheckUpdates = "Check for updates automatically";
        public const string UpdateAvailableTitle = "Update Available";
        public const string UpdateAvailableMessage = "A new version ({0}) is available!\n\nCurrent version: {1}\n\nWould you like to open the download page?";
        public const string UpdateCheckingMessage = "Checking for updates...";
        public const string UpdateLatestVersion = "You have the latest version.";
        public const string UpdateCheckFailed = "Failed to check for updates: {0}";
        
        // Size estimation
        public const string EstimateSizeButton = "Estimate Size";
        public const string EstimatingSizeMessage = "Estimating download size...";
        public const string EstimatedSizeResult = "Estimated size: {0} ({1} files)";
        public const string EstimationCancelled = "Size estimation cancelled";
        public const string EstimationFailed = "Size estimation failed: {0}";
        
        // Download Queue
        public const string QueueTabHeader = "Queue ({0})";
        public const string QueueAddButton = "Add to Queue";
        public const string QueueStartButton = "Start Queue";
        public const string QueuePauseButton = "Pause Queue";
        public const string QueueClearButton = "Clear Completed";
        public const string QueueEmptyMessage = "Queue is empty";
        public const string QueueProcessingMessage = "Processing queue: {0} of {1}";
        
        // ZIP operations
        public const string ZipCreatingMessage = "Creating ZIP archive...";
        public const string ZipCompleteMessage = "ZIP created: {0} ({1})";
        public const string ZipFailedMessage = "Failed to create ZIP: {0}";
        
        // Validation - new
        public const string ValidationTimeoutInvalid = "Timeout must be between {0} and {1} seconds.";
        public const string ValidationRetryInvalid = "Retry count must be between 0 and 20.";
        public const string ValidationThreadCountInvalid = "Thread count must be between 1 and 16.";
    }
}
