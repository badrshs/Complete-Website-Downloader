using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace WebsiteDownloader.Helpers
{
    /// <summary>
    /// Event arguments for ZIP progress updates.
    /// </summary>
    public class ZipProgressEventArgs : EventArgs
    {
        public int FilesProcessed { get; set; }
        public int TotalFiles { get; set; }
        public string CurrentFile { get; set; }
        public int PercentComplete => TotalFiles > 0 ? (FilesProcessed * 100 / TotalFiles) : 0;
    }

    /// <summary>
    /// Result of a ZIP operation.
    /// </summary>
    public class ZipResult
    {
        public bool Success { get; set; }
        public string ZipFilePath { get; set; }
        public long ZipFileSize { get; set; }
        public int FilesZipped { get; set; }
        public TimeSpan Duration { get; set; }
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets a human-readable size string.
        /// </summary>
        public string FormattedSize
        {
            get
            {
                if (ZipFileSize < 1024)
                    return $"{ZipFileSize} B";
                if (ZipFileSize < 1024 * 1024)
                    return $"{ZipFileSize / 1024.0:F1} KB";
                if (ZipFileSize < 1024 * 1024 * 1024)
                    return $"{ZipFileSize / (1024.0 * 1024):F1} MB";
                return $"{ZipFileSize / (1024.0 * 1024 * 1024):F2} GB";
            }
        }
    }

    /// <summary>
    /// Helper class for creating ZIP archives from downloaded websites.
    /// </summary>
    public static class ZipHelper
    {
        /// <summary>
        /// Event raised during ZIP creation to report progress.
        /// </summary>
        public static event EventHandler<ZipProgressEventArgs> ProgressChanged;

        /// <summary>
        /// Creates a ZIP archive from a folder.
        /// </summary>
        /// <param name="sourceFolder">The folder to compress.</param>
        /// <param name="zipFilePath">The destination ZIP file path. If null, creates next to source folder.</param>
        /// <param name="deleteSourceAfterZip">Whether to delete the source folder after successful ZIP.</param>
        /// <param name="compressionLevel">Compression level to use.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Result of the ZIP operation.</returns>
        public static async Task<ZipResult> CreateZipAsync(
            string sourceFolder,
            string zipFilePath = null,
            bool deleteSourceAfterZip = false,
            CompressionLevel compressionLevel = CompressionLevel.Optimal,
            CancellationToken cancellationToken = default)
        {
            var startTime = DateTime.Now;
            int filesZipped = 0;

            try
            {
                if (!Directory.Exists(sourceFolder))
                {
                    return new ZipResult
                    {
                        Success = false,
                        ErrorMessage = $"Source folder does not exist: {sourceFolder}"
                    };
                }

                // Default ZIP path is next to the source folder with same name
                if (string.IsNullOrEmpty(zipFilePath))
                {
                    zipFilePath = sourceFolder.TrimEnd(Path.DirectorySeparatorChar) + ".zip";
                }

                // Ensure unique filename if ZIP already exists
                zipFilePath = GetUniqueFilePath(zipFilePath);

                // Get all files for progress reporting
                var files = Directory.GetFiles(sourceFolder, "*", SearchOption.AllDirectories);
                int totalFiles = files.Length;

                // Create the ZIP file
                await Task.Run(() =>
                {
                    using (var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                    {
                        foreach (var file in files)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            // Calculate relative path for entry
                            string relativePath = file.Substring(sourceFolder.Length).TrimStart(Path.DirectorySeparatorChar);
                            
                            zipArchive.CreateEntryFromFile(file, relativePath, compressionLevel);
                            filesZipped++;

                            ProgressChanged?.Invoke(null, new ZipProgressEventArgs
                            {
                                FilesProcessed = filesZipped,
                                TotalFiles = totalFiles,
                                CurrentFile = relativePath
                            });
                        }
                    }
                }, cancellationToken).ConfigureAwait(false);

                var zipInfo = new FileInfo(zipFilePath);

                // Delete source folder if requested
                if (deleteSourceAfterZip && zipInfo.Exists)
                {
                    await Task.Run(() =>
                    {
                        Directory.Delete(sourceFolder, recursive: true);
                    }, cancellationToken).ConfigureAwait(false);
                }

                return new ZipResult
                {
                    Success = true,
                    ZipFilePath = zipFilePath,
                    ZipFileSize = zipInfo.Length,
                    FilesZipped = filesZipped,
                    Duration = DateTime.Now - startTime
                };
            }
            catch (OperationCanceledException)
            {
                // Clean up partial ZIP file
                if (!string.IsNullOrEmpty(zipFilePath) && File.Exists(zipFilePath))
                {
                    try { File.Delete(zipFilePath); } catch { }
                }

                return new ZipResult
                {
                    Success = false,
                    FilesZipped = filesZipped,
                    Duration = DateTime.Now - startTime,
                    ErrorMessage = "Operation was cancelled"
                };
            }
            catch (Exception ex)
            {
                // Clean up partial ZIP file
                if (!string.IsNullOrEmpty(zipFilePath) && File.Exists(zipFilePath))
                {
                    try { File.Delete(zipFilePath); } catch { }
                }

                return new ZipResult
                {
                    Success = false,
                    FilesZipped = filesZipped,
                    Duration = DateTime.Now - startTime,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Gets a unique file path by appending a number if the file already exists.
        /// </summary>
        private static string GetUniqueFilePath(string filePath)
        {
            if (!File.Exists(filePath))
                return filePath;

            string directory = Path.GetDirectoryName(filePath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);

            int counter = 1;
            string newPath;
            do
            {
                newPath = Path.Combine(directory, $"{fileNameWithoutExt} ({counter}){extension}");
                counter++;
            } while (File.Exists(newPath));

            return newPath;
        }
    }
}
