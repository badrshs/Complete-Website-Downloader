using System.IO;
using WebsiteDownloader.Models;
using Xunit;

namespace WebsiteDownloader.Tests
{
    /// <summary>
    /// Tests for <see cref="AppSettings.ValidateAndFix"/>, the normalization pass that clamps
    /// out-of-range numeric settings and resets invalid string settings to safe defaults.
    /// </summary>
    public class AppSettingsValidateTests
    {
        /// <summary>
        /// A settings object whose output folder points at a real, existing directory so the
        /// "reset folder to Desktop" branch doesn't fire while we test numeric clamping.
        /// </summary>
        private static AppSettings WithValidFolder(TempDir temp) => new AppSettings
        {
            DefaultOutputFolder = temp.Path,
        };

        [Fact]
        public void EmptyUserAgent_IsResetToDefault()
        {
            using (var temp = new TempDir())
            {
                var s = WithValidFolder(temp);
                s.UserAgent = "   ";
                s.ValidateAndFix();
                Assert.Equal(AppConstants.DefaultUserAgent, s.UserAgent);
            }
        }

        [Fact]
        public void NonExistentOutputFolder_IsResetToDesktop()
        {
            var s = new AppSettings { DefaultOutputFolder = @"Z:\does\not\exist\anywhere" };
            s.ValidateAndFix();
            string desktop = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            Assert.Equal(desktop, s.DefaultOutputFolder);
        }

        [Theory]
        [InlineData(-5, 0)]      // below min clamps to 0
        [InlineData(0, 0)]
        [InlineData(50, 50)]     // in range, unchanged
        [InlineData(101, 100)]   // above max clamps to 100
        public void MaxDepth_IsClampedTo0To100(int input, int expected)
        {
            using (var temp = new TempDir())
            {
                var s = WithValidFolder(temp);
                s.MaxDepth = input;
                s.ValidateAndFix();
                Assert.Equal(expected, s.MaxDepth);
            }
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(400, 300)]   // above max clamps to 300
        public void WaitBetweenRequests_IsClampedTo0To300(int input, int expected)
        {
            using (var temp = new TempDir())
            {
                var s = WithValidFolder(temp);
                s.WaitBetweenRequests = input;
                s.ValidateAndFix();
                Assert.Equal(expected, s.WaitBetweenRequests);
            }
        }

        [Theory]
        [InlineData(1, 5)]       // below min clamps to 5
        [InlineData(500, 300)]   // above max clamps to 300
        public void ConnectionTimeout_IsClampedTo5To300(int input, int expected)
        {
            using (var temp = new TempDir())
            {
                var s = WithValidFolder(temp);
                s.ConnectionTimeout = input;
                s.ValidateAndFix();
                Assert.Equal(expected, s.ConnectionTimeout);
            }
        }

        [Theory]
        [InlineData(0, 1)]       // below min clamps to 1
        [InlineData(99, 16)]     // above max clamps to 16
        public void ThreadCount_IsClampedTo1To16(int input, int expected)
        {
            using (var temp = new TempDir())
            {
                var s = WithValidFolder(temp);
                s.ThreadCount = input;
                s.ValidateAndFix();
                Assert.Equal(expected, s.ThreadCount);
            }
        }

        [Theory]
        [InlineData(-1, 9)]      // out of [0,23] resets to default 9
        [InlineData(25, 9)]
        [InlineData(6, 6)]       // valid, unchanged
        public void PeakHoursStart_OutOfRangeResetsToDefault(int input, int expected)
        {
            using (var temp = new TempDir())
            {
                var s = WithValidFolder(temp);
                s.PeakHoursStart = input;
                s.ValidateAndFix();
                Assert.Equal(expected, s.PeakHoursStart);
            }
        }

        [Theory]
        [InlineData(-1, 20)]     // negative resets to wget default
        [InlineData(0, 0)]       // 0 (= use wget default) is allowed
        [InlineData(50, 50)]     // in range, unchanged
        [InlineData(999, 100)]   // above max clamps to 100
        public void MaxRedirect_IsNormalized(int input, int expected)
        {
            using (var temp = new TempDir())
            {
                var s = WithValidFolder(temp);
                s.MaxRedirect = input;
                s.ValidateAndFix();
                Assert.Equal(expected, s.MaxRedirect);
            }
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, "")]
        [InlineData("500m", "500m")]    // valid size, kept
        [InlineData("2g", "2g")]
        [InlineData("lots", "")]        // unparseable, reset to unlimited
        [InlineData("10x", "")]
        public void DownloadQuota_InvalidFormat_IsReset(string input, string expected)
        {
            using (var temp = new TempDir())
            {
                var s = WithValidFolder(temp);
                s.DownloadQuota = input;
                s.ValidateAndFix();
                Assert.Equal(expected, s.DownloadQuota);
            }
        }

        [Fact]
        public void NullFilterAndAuthStrings_AreNormalizedToEmpty()
        {
            using (var temp = new TempDir())
            {
                var s = WithValidFolder(temp);
                s.DomainList = null;
                s.AcceptFileTypes = null;
                s.RejectFileTypes = null;
                s.IncludeDirectories = null;
                s.ExcludeDirectories = null;
                s.HttpUser = null;
                s.HttpPassword = null;
                s.CookiesFilePath = null;
                s.CustomHeaders = null;
                s.Referer = null;

                s.ValidateAndFix();

                Assert.Equal("", s.DomainList);
                Assert.Equal("", s.AcceptFileTypes);
                Assert.Equal("", s.RejectFileTypes);
                Assert.Equal("", s.IncludeDirectories);
                Assert.Equal("", s.ExcludeDirectories);
                Assert.Equal("", s.HttpUser);
                Assert.Equal("", s.HttpPassword);
                Assert.Equal("", s.CookiesFilePath);
                Assert.Equal("", s.CustomHeaders);
                Assert.Equal("", s.Referer);
            }
        }

        [Fact]
        public void UndefinedDirectoryStructure_IsResetToDefault()
        {
            using (var temp = new TempDir())
            {
                var s = WithValidFolder(temp);
                s.DirectoryStructure = (WebsiteDownloader.Services.DirectoryStructure)99;
                s.ValidateAndFix();
                Assert.Equal(WebsiteDownloader.Services.DirectoryStructure.Default, s.DirectoryStructure);
            }
        }

        [Fact]
        public void TinyWindowDimensions_AreResetToDefaults()
        {
            using (var temp = new TempDir())
            {
                var s = WithValidFolder(temp);
                s.WindowWidth = 100;
                s.WindowHeight = 50;
                s.ValidateAndFix();
                Assert.Equal(600, s.WindowWidth);
                Assert.Equal(400, s.WindowHeight);
            }
        }
    }
}
