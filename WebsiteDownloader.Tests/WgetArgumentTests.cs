using System;
using WebsiteDownloader.Services;
using Xunit;

namespace WebsiteDownloader.Tests
{
    /// <summary>
    /// Tests for <see cref="WgetDownloader.BuildArguments"/> — the construction of the wget
    /// command line from <see cref="DownloadOptions"/>. Pure string building, no process launch.
    /// </summary>
    public class WgetArgumentTests : IDisposable
    {
        private readonly TempDir _temp;
        private readonly WgetDownloader _sut;

        public WgetArgumentTests()
        {
            // The constructor validates that wget.exe exists, so point it at a dummy file.
            _temp = new TempDir();
            string fakeWget = _temp.CreateFile("wget.exe");
            _sut = new WgetDownloader(fakeWget);
        }

        public void Dispose()
        {
            _sut.Dispose();
            _temp.Dispose();
        }

        private static DownloadOptions Options(
            string url = "https://example.com/",
            bool convertLinks = true,
            bool adjustExtensions = true,
            int maxDepth = 0,
            int waitBetweenRequests = 0,
            string rateLimit = null,
            bool ignoreSslErrors = false,
            int connectionTimeout = 30,
            int readTimeout = 60,
            int retryCount = 3,
            ResumeMode? resumeMode = ResumeMode.Off)
        {
            return new DownloadOptions(
                url: new Uri(url),
                outputFolder: @"C:\out",
                userAgent: "TestAgent",
                convertLinks: convertLinks,
                adjustExtensions: adjustExtensions,
                maxDepth: maxDepth,
                waitBetweenRequests: waitBetweenRequests,
                rateLimit: rateLimit,
                ignoreSslErrors: ignoreSslErrors,
                connectionTimeout: connectionTimeout,
                readTimeout: readTimeout,
                retryCount: retryCount,
                resumeMode: resumeMode);
        }

        [Fact]
        public void CoreFlags_AreAlwaysPresent()
        {
            string args = _sut.BuildArguments(Options());
            Assert.Contains("-r ", args);                 // recursive
            Assert.Contains("-p ", args);                 // page requisites
            Assert.Contains("-e robots=off ", args);      // ignore robots.txt
            Assert.Contains("-U \"TestAgent\" ", args);   // user agent (quoted, sanitized)
        }

        [Fact]
        public void Url_AndOutputDirectory_AreQuotedAndHostBased()
        {
            string args = _sut.BuildArguments(Options("https://example.com/blog"));
            Assert.Contains("\"https://example.com/blog\" ", args);
            // Output goes under a host-named folder — the basis for skip/resume on restart.
            Assert.Contains("-P \"./example.com\"", args);
        }

        [Fact]
        public void ConvertLinks_AndAdjustExtensions_AreOptional()
        {
            string with = _sut.BuildArguments(Options(convertLinks: true, adjustExtensions: true));
            Assert.Contains("-k ", with);
            Assert.Contains("-E ", with);

            string without = _sut.BuildArguments(Options(convertLinks: false, adjustExtensions: false));
            Assert.DoesNotContain("-k ", without);
            Assert.DoesNotContain("-E ", without);
        }

        [Fact]
        public void MaxDepth_ZeroMeansInfinite_PositiveIsPassedThrough()
        {
            Assert.Contains("-l 0 ", _sut.BuildArguments(Options(maxDepth: 0)));
            Assert.Contains("-l 5 ", _sut.BuildArguments(Options(maxDepth: 5)));
        }

        [Fact]
        public void Wait_AndRateLimit_AreEmittedOnlyWhenSet()
        {
            string none = _sut.BuildArguments(Options(waitBetweenRequests: 0, rateLimit: null));
            Assert.DoesNotContain("-w ", none);
            Assert.DoesNotContain("--limit-rate=", none);

            string set = _sut.BuildArguments(Options(waitBetweenRequests: 2, rateLimit: "200k"));
            Assert.Contains("-w 2 ", set);
            Assert.Contains("--limit-rate=200k ", set);
        }

        [Fact]
        public void Timeouts_AndRetries_AreFormatted()
        {
            string args = _sut.BuildArguments(Options(connectionTimeout: 15, readTimeout: 45, retryCount: 7));
            Assert.Contains("--connect-timeout=15 ", args);
            Assert.Contains("--read-timeout=45 ", args);
            Assert.Contains("--tries=7 ", args);
        }

        [Fact]
        public void IgnoreSsl_TogglesCertificateCheck()
        {
            Assert.Contains("--no-check-certificate ", _sut.BuildArguments(Options(ignoreSslErrors: true)));
            Assert.DoesNotContain("--no-check-certificate ", _sut.BuildArguments(Options(ignoreSslErrors: false)));
        }

        [Theory]
        [InlineData(ResumeMode.Off, null)]
        [InlineData(ResumeMode.Continue, "-c ")]
        [InlineData(ResumeMode.Timestamping, "-N ")]
        [InlineData(ResumeMode.NoClobber, "-nc ")]
        public void ResumeMode_EmitsExactlyOneFlag(ResumeMode mode, string expectedFlag)
        {
            string args = _sut.BuildArguments(Options(resumeMode: mode));

            // Whatever the mode, never more than one resume flag is present.
            int count = 0;
            if (System.Text.RegularExpressions.Regex.IsMatch(args, @"(^|\s)-c(\s|$)")) count++;
            if (System.Text.RegularExpressions.Regex.IsMatch(args, @"(^|\s)-N(\s|$)")) count++;
            if (System.Text.RegularExpressions.Regex.IsMatch(args, @"(^|\s)-nc(\s|$)")) count++;
            Assert.True(count <= 1, $"More than one resume flag emitted for {mode}: {args}");

            if (expectedFlag != null)
                Assert.Contains(expectedFlag, args);
        }

        [Fact]
        public void Sanitization_IsAppliedToUserAgentAndRateLimitAndHost()
        {
            // Inject shell metacharacters into the values that get interpolated.
            var options = new DownloadOptions(
                url: new Uri("https://example.com/"),
                outputFolder: @"C:\out",
                userAgent: "Agent$(evil)",
                rateLimit: "200k",
                resumeMode: ResumeMode.Off);

            string args = _sut.BuildArguments(options);
            // The '$', '(' and ')' must have been stripped out of the user agent.
            Assert.DoesNotContain("$(evil)", args);
            Assert.Contains("-U \"Agentevil\" ", args);
        }
    }
}
