using System;

namespace WebsiteDownloader.Services
{
    /// <summary>
    /// A null implementation of IAppLogger that does nothing.
    /// Used as a default when no logger is provided (Null Object Pattern).
    /// </summary>
    public class NullLogger : IAppLogger
    {
        /// <summary>
        /// Singleton instance of the null logger.
        /// </summary>
        public static readonly NullLogger Instance = new NullLogger();

        /// <inheritdoc/>
        public void Log(LogLevel level, string message, Exception exception = null)
        {
            // Intentionally empty - null object pattern
        }

        /// <inheritdoc/>
        public void Debug(string message)
        {
            // Intentionally empty
        }

        /// <inheritdoc/>
        public void Info(string message)
        {
            // Intentionally empty
        }

        /// <inheritdoc/>
        public void Warning(string message, Exception exception = null)
        {
            // Intentionally empty
        }

        /// <inheritdoc/>
        public void Error(string message, Exception exception = null)
        {
            // Intentionally empty
        }
    }
}
