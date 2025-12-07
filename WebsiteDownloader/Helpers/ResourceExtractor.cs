using System;
using System.IO;
using System.Reflection;

namespace WebsiteDownloader.Helpers
{
    /// <summary>
    /// Handles extraction of embedded resources from the assembly
    /// </summary>
    public static class ResourceExtractor
    {
        private const string WgetResourceName = "WebsiteDownloader.wget.exe";

        /// <summary>
        /// Extracts wget.exe from embedded resources to a temporary location
        /// </summary>
        /// <returns>Path to the extracted wget.exe</returns>
        public static string ExtractWget()
        {
            // Use a unique name to avoid conflicts with multiple instances
            string tempPath = Path.Combine(Path.GetTempPath(), $"WebsiteDownloader_wget_{GetAssemblyVersion()}.exe");

            if (File.Exists(tempPath))
            {
                return tempPath;
            }

            ExtractResource(WgetResourceName, tempPath);
            return tempPath;
        }

        /// <summary>
        /// Extracts an embedded resource to a file
        /// </summary>
        private static void ExtractResource(string resourceName, string outputPath)
        {
            using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null)
                {
                    throw new InvalidOperationException(
                        $"Cannot find embedded resource '{resourceName}'. " +
                        "Ensure wget.exe is included as an EmbeddedResource in the project.");
                }

                // Ensure directory exists
                string directory = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    resourceStream.CopyTo(fileStream);
                }
            }
        }

        /// <summary>
        /// Cleans up extracted resources from temp folder
        /// </summary>
        public static void Cleanup()
        {
            try
            {
                string pattern = "WebsiteDownloader_wget_*.exe";
                string tempPath = Path.GetTempPath();

                foreach (string file in Directory.GetFiles(tempPath, pattern))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (IOException)
                    {
                        // File in use, ignore
                    }
                }
            }
            catch
            {
                // Cleanup is best-effort
            }
        }

        private static string GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString().Replace(".", "_");
        }
    }
}
