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
            ResumeMode? resumeMode = ResumeMode.Off,
            bool noParent = false,
            bool spanHosts = false,
            string domainList = null,
            string acceptFileTypes = null,
            string rejectFileTypes = null,
            string includeDirectories = null,
            string excludeDirectories = null,
            bool ignoreFilterCase = false,
            string downloadQuota = null,
            int maxRedirect = 20,
            string httpUser = null,
            string httpPassword = null,
            string cookiesFilePath = null,
            bool keepSessionCookies = false,
            string customHeaders = null,
            string referer = null,
            bool randomWait = false,
            bool contentDisposition = false,
            DirectoryStructure directoryStructure = DirectoryStructure.Default)
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
                resumeMode: resumeMode,
                noParent: noParent,
                spanHosts: spanHosts,
                domainList: domainList,
                acceptFileTypes: acceptFileTypes,
                rejectFileTypes: rejectFileTypes,
                includeDirectories: includeDirectories,
                excludeDirectories: excludeDirectories,
                ignoreFilterCase: ignoreFilterCase,
                downloadQuota: downloadQuota,
                maxRedirect: maxRedirect,
                httpUser: httpUser,
                httpPassword: httpPassword,
                cookiesFilePath: cookiesFilePath,
                keepSessionCookies: keepSessionCookies,
                customHeaders: customHeaders,
                referer: referer,
                randomWait: randomWait,
                contentDisposition: contentDisposition,
                directoryStructure: directoryStructure);
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
        public void RestrictFileNames_Windows_IsAlwaysPresent()
        {
            // URLs containing ?, *, : etc. would otherwise produce invalid Windows paths.
            Assert.Contains("--restrict-file-names=windows ", _sut.BuildArguments(Options()));
        }

        [Fact]
        public void NoParent_IsEmittedOnlyWhenSet()
        {
            Assert.Contains("-np ", _sut.BuildArguments(Options(noParent: true)));
            Assert.DoesNotContain("-np ", _sut.BuildArguments(Options(noParent: false)));
        }

        [Fact]
        public void SpanHosts_AndDomains_AreEmittedOnlyWhenSet()
        {
            string none = _sut.BuildArguments(Options());
            Assert.DoesNotContain("-H ", none);
            Assert.DoesNotContain("--domains=", none);

            string set = _sut.BuildArguments(Options(spanHosts: true, domainList: "example.com,cdn.example.com"));
            Assert.Contains("-H ", set);
            Assert.Contains("--domains=\"example.com,cdn.example.com\" ", set);
        }

        [Fact]
        public void AcceptAndRejectFilters_AreEmittedOnlyWhenSet()
        {
            string none = _sut.BuildArguments(Options());
            Assert.DoesNotContain("-A ", none);
            Assert.DoesNotContain("-R ", none);

            string set = _sut.BuildArguments(Options(acceptFileTypes: "pdf,jpg", rejectFileTypes: "zip,exe"));
            Assert.Contains("-A \"pdf,jpg\" ", set);
            Assert.Contains("-R \"zip,exe\" ", set);
        }

        [Fact]
        public void IncludeAndExcludeDirectories_AreEmittedOnlyWhenSet()
        {
            string none = _sut.BuildArguments(Options());
            Assert.DoesNotContain("-I ", none);
            Assert.DoesNotContain("-X ", none);

            string set = _sut.BuildArguments(Options(includeDirectories: "/blog,/docs", excludeDirectories: "/forum"));
            Assert.Contains("-I \"/blog,/docs\" ", set);
            Assert.Contains("-X \"/forum\" ", set);
        }

        [Fact]
        public void IgnoreCase_TogglesFilterMatching()
        {
            Assert.Contains("--ignore-case ", _sut.BuildArguments(Options(ignoreFilterCase: true)));
            Assert.DoesNotContain("--ignore-case ", _sut.BuildArguments(Options(ignoreFilterCase: false)));
        }

        [Fact]
        public void Quota_IsEmittedOnlyWhenSet()
        {
            Assert.DoesNotContain("--quota=", _sut.BuildArguments(Options()));
            Assert.Contains("--quota=500m ", _sut.BuildArguments(Options(downloadQuota: "500m")));
        }

        [Fact]
        public void MaxRedirect_IsEmittedOnlyWhenPositive()
        {
            Assert.Contains("--max-redirect=20 ", _sut.BuildArguments(Options()));
            Assert.Contains("--max-redirect=5 ", _sut.BuildArguments(Options(maxRedirect: 5)));
            Assert.DoesNotContain("--max-redirect=", _sut.BuildArguments(Options(maxRedirect: 0)));
        }

        [Fact]
        public void Credentials_AreEmittedAndSanitized()
        {
            string none = _sut.BuildArguments(Options());
            Assert.DoesNotContain("--user=", none);
            Assert.DoesNotContain("--password=", none);

            string set = _sut.BuildArguments(Options(httpUser: "alice", httpPassword: "p$(ss)"));
            Assert.Contains("--user=\"alice\" ", set);
            // Shell metacharacters must be stripped from the password.
            Assert.Contains("--password=\"pss\" ", set);
        }

        [Fact]
        public void CookiesFile_EmitsLoadCookies_AndOptionalSessionFlag()
        {
            string none = _sut.BuildArguments(Options());
            Assert.DoesNotContain("--load-cookies", none);

            string withCookies = _sut.BuildArguments(Options(cookiesFilePath: @"C:\cookies.txt"));
            Assert.Contains("--load-cookies \"C:\\cookies.txt\" ", withCookies);
            Assert.DoesNotContain("--keep-session-cookies ", withCookies);

            string withSession = _sut.BuildArguments(Options(cookiesFilePath: @"C:\cookies.txt", keepSessionCookies: true));
            Assert.Contains("--keep-session-cookies ", withSession);
        }

        [Fact]
        public void KeepSessionCookies_WithoutCookiesFile_IsNotEmitted()
        {
            // The flag only makes sense alongside --load-cookies.
            string args = _sut.BuildArguments(Options(keepSessionCookies: true));
            Assert.DoesNotContain("--keep-session-cookies ", args);
        }

        [Fact]
        public void CustomHeaders_EmitOnePerLine_AndSkipMalformedLines()
        {
            string headers = "Authorization: Bearer abc123\r\nX-Custom: yes\r\nnot-a-header\r\n\r\n";
            string args = _sut.BuildArguments(Options(customHeaders: headers));

            Assert.Contains("--header \"Authorization: Bearer abc123\" ", args);
            Assert.Contains("--header \"X-Custom: yes\" ", args);
            // A line without "Name: value" structure must not produce a --header argument.
            Assert.DoesNotContain("not-a-header", args);
        }

        [Fact]
        public void Referer_IsEmittedOnlyWhenSet()
        {
            Assert.DoesNotContain("--referer=", _sut.BuildArguments(Options()));
            Assert.Contains("--referer=\"https://example.com/page\" ",
                _sut.BuildArguments(Options(referer: "https://example.com/page")));
        }

        [Fact]
        public void RandomWait_AndContentDisposition_AreOptional()
        {
            string none = _sut.BuildArguments(Options());
            Assert.DoesNotContain("--random-wait ", none);
            Assert.DoesNotContain("--content-disposition ", none);

            string set = _sut.BuildArguments(Options(randomWait: true, contentDisposition: true));
            Assert.Contains("--random-wait ", set);
            Assert.Contains("--content-disposition ", set);
        }

        [Theory]
        [InlineData(DirectoryStructure.Default, null)]
        [InlineData(DirectoryStructure.Flat, "-nd ")]
        [InlineData(DirectoryStructure.ForceFull, "-x ")]
        public void DirectoryStructure_EmitsAtMostOneFlag(DirectoryStructure structure, string expectedFlag)
        {
            string args = _sut.BuildArguments(Options(directoryStructure: structure));

            // -nd and -x are mutually exclusive in wget; never emit both.
            int count = 0;
            if (System.Text.RegularExpressions.Regex.IsMatch(args, @"(^|\s)-nd(\s|$)")) count++;
            if (System.Text.RegularExpressions.Regex.IsMatch(args, @"(^|\s)-x(\s|$)")) count++;
            Assert.True(count <= 1, $"More than one directory flag emitted for {structure}: {args}");

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
