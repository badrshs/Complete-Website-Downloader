using System;

namespace WebsiteDownloader.Services
{
    /// <summary>
    /// Log severity levels.
    /// </summary>
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error
    }

    /// <summary>
    /// Abstraction for application logging.
    /// Enables consistent logging throughout the application with support for different outputs.
    /// </summary>
    public interface IAppLogger
    {
        /// <summary>
        /// Logs a message at the specified level.
        /// </summary>
        /// <param name="level">The severity level.</param>
        /// <param name="message">The log message.</param>
        /// <param name="exception">Optional exception to log.</param>
        void Log(LogLevel level, string message, Exception exception = null);

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        void Debug(string message);

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        void Info(string message);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        void Warning(string message, Exception exception = null);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        void Error(string message, Exception exception = null);
    }
}
