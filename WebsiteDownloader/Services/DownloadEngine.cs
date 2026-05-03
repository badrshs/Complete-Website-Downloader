namespace WebsiteDownloader.Services
{
    /// <summary>
    /// Available download engine implementations.
    /// </summary>
    public enum DownloadEngine
    {
        /// <summary>
        /// GNU wget - fast, lightweight, no JS support.
        /// </summary>
        Wget = 0,

        /// <summary>
        /// Playwright/Crawlee - full browser engine, handles JavaScript-rendered sites.
        /// Requires Node.js installed.
        /// </summary>
        Playwright = 1
    }
}
