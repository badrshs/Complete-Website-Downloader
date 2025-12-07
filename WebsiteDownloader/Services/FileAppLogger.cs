using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace WebsiteDownloader.Services
{
    /// <summary>
    /// File-based logger implementation that writes to both Debug output and a log file.
    /// Thread-safe for concurrent logging operations.
    /// </summary>
    public class FileAppLogger : IAppLogger
    {
        private readonly string _logFilePath;
        private readonly object _lockObject = new object();
        private readonly LogLevel _minimumLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAppLogger"/> class.
        /// </summary>
        /// <param name="logFilePath">Path to the log file. If null, only Debug output is used.</param>
        /// <param name="minimumLevel">Minimum log level to record.</param>
        public FileAppLogger(string logFilePath = null, LogLevel minimumLevel = LogLevel.Info)
        {
            _logFilePath = logFilePath ?? Path.Combine(AppConstants.AppDataFolder, "app.log");
            _minimumLevel = minimumLevel;

            // Ensure log directory exists
            EnsureLogDirectoryExists();

            // Rotate log if too large (> 5MB)
            RotateLogIfNeeded();
        }

        /// <inheritdoc/>
        public void Log(LogLevel level, string message, Exception exception = null)
        {
            if (level < _minimumLevel)
                return;

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var levelStr = level.ToString().ToUpperInvariant().PadRight(7);
            
            var logMessage = new StringBuilder();
            logMessage.Append($"[{timestamp}] [{levelStr}] {message}");

            if (exception != null)
            {
                logMessage.AppendLine();
                logMessage.Append($"  Exception: {exception.GetType().Name}: {exception.Message}");
                if (exception.StackTrace != null)
                {
                    logMessage.AppendLine();
                    logMessage.Append($"  StackTrace: {exception.StackTrace}");
                }
            }

            var finalMessage = logMessage.ToString();

            // Always write to Debug output
            System.Diagnostics.Debug.WriteLine(finalMessage);

            // Write to file
            WriteToFile(finalMessage);
        }

        /// <inheritdoc/>
        public void Debug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        /// <inheritdoc/>
        public void Info(string message)
        {
            Log(LogLevel.Info, message);
        }

        /// <inheritdoc/>
        public void Warning(string message, Exception exception = null)
        {
            Log(LogLevel.Warning, message, exception);
        }

        /// <inheritdoc/>
        public void Error(string message, Exception exception = null)
        {
            Log(LogLevel.Error, message, exception);
        }

        private void WriteToFile(string message)
        {
            if (string.IsNullOrEmpty(_logFilePath))
                return;

            lock (_lockObject)
            {
                try
                {
                    File.AppendAllText(_logFilePath, message + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    // Fallback to Debug output only
                    System.Diagnostics.Debug.WriteLine($"Failed to write to log file: {ex.Message}");
                }
            }
        }

        private void EnsureLogDirectoryExists()
        {
            try
            {
                var directory = Path.GetDirectoryName(_logFilePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to create log directory: {ex.Message}");
            }
        }

        private void RotateLogIfNeeded()
        {
            try
            {
                if (!File.Exists(_logFilePath))
                    return;

                var fileInfo = new FileInfo(_logFilePath);
                const long maxSizeBytes = 5 * 1024 * 1024; // 5MB

                if (fileInfo.Length > maxSizeBytes)
                {
                    var archivePath = Path.Combine(
                        Path.GetDirectoryName(_logFilePath),
                        $"{Path.GetFileNameWithoutExtension(_logFilePath)}_{DateTime.Now:yyyyMMdd_HHmmss}.log");

                    File.Move(_logFilePath, archivePath);

                    // Clean up old archives (keep last 3)
                    CleanupOldArchives();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to rotate log: {ex.Message}");
            }
        }

        private void CleanupOldArchives()
        {
            try
            {
                var directory = Path.GetDirectoryName(_logFilePath);
                var pattern = $"{Path.GetFileNameWithoutExtension(_logFilePath)}_*.log";
                var archives = Directory.GetFiles(directory, pattern);

                if (archives.Length > 3)
                {
                    Array.Sort(archives);
                    for (int i = 0; i < archives.Length - 3; i++)
                    {
                        File.Delete(archives[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to cleanup old log archives: {ex.Message}");
            }
        }
    }
}
