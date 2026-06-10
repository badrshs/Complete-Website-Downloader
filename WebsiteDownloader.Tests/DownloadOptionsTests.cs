using System;
using WebsiteDownloader.Services;
using Xunit;

namespace WebsiteDownloader.Tests
{
    /// <summary>
    /// Tests for <see cref="DownloadOptions"/> construction — defaults and the derivation of
    /// <see cref="DownloadOptions.ResumeMode"/> from the legacy boolean flags when not specified.
    /// </summary>
    public class DownloadOptionsTests
    {
        private static DownloadOptions Make(
            bool noClobber = false,
            bool continueDownload = true,
            ResumeMode? resumeMode = null)
        {
            return new DownloadOptions(
                url: new Uri("https://example.com/"),
                outputFolder: @"C:\out",
                noClobber: noClobber,
                continueDownload: continueDownload,
                resumeMode: resumeMode);
        }

        [Fact]
        public void NullUrl_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new DownloadOptions(url: null, outputFolder: @"C:\out"));
        }

        [Fact]
        public void NullOutputFolder_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new DownloadOptions(url: new Uri("https://example.com/"), outputFolder: null));
        }

        [Fact]
        public void UserAgent_DefaultsToAppConstant_WhenNotProvided()
        {
            var options = Make();
            Assert.Equal(AppConstants.DefaultUserAgent, options.UserAgent);
        }

        [Fact]
        public void ExplicitResumeMode_TakesPrecedenceOverLegacyFlags()
        {
            // Legacy flags would imply NoClobber, but the explicit mode must win.
            var options = Make(noClobber: true, continueDownload: true, resumeMode: ResumeMode.Timestamping);
            Assert.Equal(ResumeMode.Timestamping, options.ResumeMode);
        }

        [Theory]
        [InlineData(false, true, ResumeMode.Continue)]   // historic default
        [InlineData(true, false, ResumeMode.NoClobber)]
        [InlineData(false, false, ResumeMode.Off)]
        [InlineData(true, true, ResumeMode.NoClobber)]
        public void ResumeMode_IsDerivedFromLegacyFlags_WhenNotProvided(
            bool noClobber, bool continueDownload, ResumeMode expected)
        {
            var options = Make(noClobber: noClobber, continueDownload: continueDownload, resumeMode: null);
            Assert.Equal(expected, options.ResumeMode);
        }
    }
}
