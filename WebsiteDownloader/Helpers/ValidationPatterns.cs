using System.Text.RegularExpressions;

namespace WebsiteDownloader.Helpers
{
    /// <summary>
    /// Centralized regex patterns for input validation.
    /// All patterns are compiled for performance.
    /// </summary>
    public static class ValidationPatterns
    {
        /// <summary>
        /// Validates rate limit format (e.g., "200k", "1m", "500", or empty).
        /// Accepts: digits optionally followed by k, m, or g suffix.
        /// </summary>
        public static readonly Regex RateLimit = new Regex(
            @"^$|^\d+[kmg]?$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Validates rate limit format (strict - non-empty only).
        /// Use this when empty is not acceptable.
        /// </summary>
        public static readonly Regex RateLimitStrict = new Regex(
            @"^\d+[kmg]?$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Validates URL format (HTTP or HTTPS).
        /// </summary>
        public static readonly Regex HttpUrl = new Regex(
            @"^https?://",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }
}
