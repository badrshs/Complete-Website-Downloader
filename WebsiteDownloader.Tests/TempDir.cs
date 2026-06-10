using System;
using System.IO;

namespace WebsiteDownloader.Tests
{
    /// <summary>
    /// A disposable, unique temporary directory for tests that touch the filesystem.
    /// Created on construction and recursively deleted on dispose.
    /// </summary>
    public sealed class TempDir : IDisposable
    {
        public string Path { get; }

        public TempDir()
        {
            Path = System.IO.Path.Combine(
                System.IO.Path.GetTempPath(),
                "WDLTest_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(Path);
        }

        /// <summary>Full path of a file inside this directory (not created).</summary>
        public string File(string name) => System.IO.Path.Combine(Path, name);

        /// <summary>Creates an empty file inside this directory and returns its full path.</summary>
        public string CreateFile(string name)
        {
            string full = File(name);
            System.IO.File.WriteAllText(full, string.Empty);
            return full;
        }

        public void Dispose()
        {
            try
            {
                if (Directory.Exists(Path))
                    Directory.Delete(Path, recursive: true);
            }
            catch
            {
                // Best-effort cleanup; a leaked temp dir must never fail a test.
            }
        }
    }
}
