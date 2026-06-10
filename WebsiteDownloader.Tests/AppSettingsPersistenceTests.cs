using System.IO;
using WebsiteDownloader.Models;
using WebsiteDownloader.Services;
using Xunit;

namespace WebsiteDownloader.Tests
{
    /// <summary>
    /// Round-trip and migration tests for <see cref="AppSettings.Save"/> / <see cref="AppSettings.Load"/>,
    /// using a redirected AppData folder so nothing touches the real user profile. The headline case
    /// is the ResumeMode migration: an older settings file (no ResumeMode field) must be derived from
    /// the legacy NoClobber/ContinueDownload booleans.
    /// </summary>
    [Collection(PersistenceCollection.Name)]
    public class AppSettingsPersistenceTests
    {
        private static T WithRedirectedAppData<T>(System.Func<string, T> body)
        {
            using (var temp = new TempDir())
            {
                AppConstants.AppDataFolderOverride = temp.Path;
                try
                {
                    return body(temp.Path);
                }
                finally
                {
                    AppConstants.AppDataFolderOverride = null;
                }
            }
        }

        [Fact]
        public void Load_WhenNoFileExists_ReturnsDefaults()
        {
            var loaded = WithRedirectedAppData(_ => AppSettings.Load());
            Assert.NotNull(loaded);
            Assert.Equal(ResumeMode.Timestamping, loaded.ResumeMode);   // new-install default
            Assert.Equal(AppConstants.DefaultUserAgent, loaded.UserAgent);
        }

        [Fact]
        public void SaveThenLoad_RoundTripsValues()
        {
            var loaded = WithRedirectedAppData(_ =>
            {
                var s = new AppSettings
                {
                    UserAgent = "RoundTripAgent",
                    MaxDepth = 3,
                    ResumeMode = ResumeMode.NoClobber,
                    RateLimit = "250k",
                };
                Assert.True(s.Save());
                return AppSettings.Load();
            });

            Assert.Equal("RoundTripAgent", loaded.UserAgent);
            Assert.Equal(3, loaded.MaxDepth);
            Assert.Equal(ResumeMode.NoClobber, loaded.ResumeMode);
            Assert.Equal("250k", loaded.RateLimit);
        }

        [Fact]
        public void Save_WritesSettingsFileToAppDataFolder()
        {
            WithRedirectedAppData(path =>
            {
                Assert.True(new AppSettings().Save());
                Assert.True(File.Exists(Path.Combine(path, AppConstants.SettingsFileName)));
                return true;
            });
        }

        [Theory]
        // Legacy file with NoClobber=true should migrate to NoClobber (it wins).
        [InlineData("\"NoClobber\": true, \"ContinueDownload\": true", ResumeMode.NoClobber)]
        // Legacy file with only ContinueDownload=true -> Continue (the historic default).
        [InlineData("\"NoClobber\": false, \"ContinueDownload\": true", ResumeMode.Continue)]
        // Both off -> Off.
        [InlineData("\"NoClobber\": false, \"ContinueDownload\": false", ResumeMode.Off)]
        public void Load_LegacyFileWithoutResumeMode_MigratesFromBooleans(string flagsJson, ResumeMode expected)
        {
            var loaded = WithRedirectedAppData(path =>
            {
                // Simulate a pre-ResumeMode settings file: it has the old booleans but no ResumeMode key.
                string json = "{ " + flagsJson + ", \"UserAgent\": \"Legacy\" }";
                File.WriteAllText(Path.Combine(path, AppConstants.SettingsFileName), json);
                return AppSettings.Load();
            });

            Assert.Equal(expected, loaded.ResumeMode);
            Assert.Equal("Legacy", loaded.UserAgent);   // confirms the legacy file was actually read
        }

        [Fact]
        public void Load_NewFileWithResumeMode_DoesNotOverrideWithLegacyMigration()
        {
            var loaded = WithRedirectedAppData(path =>
            {
                // A modern file: ResumeMode present (Timestamping=2) but legacy booleans would imply NoClobber.
                string json = "{ \"ResumeMode\": 2, \"NoClobber\": true, \"ContinueDownload\": true }";
                File.WriteAllText(Path.Combine(path, AppConstants.SettingsFileName), json);
                return AppSettings.Load();
            });

            Assert.Equal(ResumeMode.Timestamping, loaded.ResumeMode);
        }

        [Fact]
        public void Load_CorruptedJson_ReturnsDefaults()
        {
            var loaded = WithRedirectedAppData(path =>
            {
                File.WriteAllText(Path.Combine(path, AppConstants.SettingsFileName), "{ this is not valid json ");
                return AppSettings.Load();
            });

            Assert.NotNull(loaded);
            Assert.Equal(AppConstants.DefaultUserAgent, loaded.UserAgent);
        }

        [Fact]
        public void Load_AppliesValidateAndFix_ClampingOutOfRangeValues()
        {
            var loaded = WithRedirectedAppData(path =>
            {
                // MaxDepth above the allowed maximum must be clamped on load.
                string json = "{ \"MaxDepth\": 9999, \"ResumeMode\": 1 }";
                File.WriteAllText(Path.Combine(path, AppConstants.SettingsFileName), json);
                return AppSettings.Load();
            });

            Assert.Equal(100, loaded.MaxDepth);
        }
    }
}
