using WebsiteDownloader.Helpers;
using Xunit;

namespace WebsiteDownloader.Tests
{
    /// <summary>
    /// Tests for the centralized validation regexes used to vet user input
    /// (rate limits and URLs) before it reaches wget.
    /// </summary>
    public class ValidationPatternsTests
    {
        [Theory]
        [InlineData("")]        // empty = unlimited, allowed by the lenient pattern
        [InlineData("200")]
        [InlineData("200k")]
        [InlineData("1m")]
        [InlineData("5g")]
        [InlineData("200K")]    // case-insensitive
        [InlineData("1M")]
        public void RateLimit_AcceptsValidValues(string value)
        {
            Assert.Matches(ValidationPatterns.RateLimit, value);
        }

        [Theory]
        [InlineData("200kb")]   // only a single k/m/g suffix is allowed
        [InlineData("abc")]
        [InlineData("200 k")]   // no embedded spaces
        [InlineData("k200")]
        [InlineData("-5")]
        [InlineData("1.5m")]    // no decimals
        public void RateLimit_RejectsInvalidValues(string value)
        {
            Assert.DoesNotMatch(ValidationPatterns.RateLimit, value);
        }

        [Fact]
        public void RateLimitStrict_RejectsEmptyButLenientAccepts()
        {
            Assert.Matches(ValidationPatterns.RateLimit, "");
            Assert.DoesNotMatch(ValidationPatterns.RateLimitStrict, "");
        }

        [Theory]
        [InlineData("200")]
        [InlineData("200k")]
        [InlineData("1G")]
        public void RateLimitStrict_AcceptsNonEmptyValidValues(string value)
        {
            Assert.Matches(ValidationPatterns.RateLimitStrict, value);
        }

        [Theory]
        [InlineData("http://example.com")]
        [InlineData("https://example.com")]
        [InlineData("HTTPS://EXAMPLE.COM")]   // case-insensitive scheme
        [InlineData("http://sub.example.com/path?q=1")]
        public void HttpUrl_AcceptsHttpAndHttps(string value)
        {
            Assert.Matches(ValidationPatterns.HttpUrl, value);
        }

        [Theory]
        [InlineData("example.com")]            // no scheme
        [InlineData("ftp://example.com")]
        [InlineData("file:///c:/x")]
        [InlineData("  http://example.com")]   // leading whitespace, anchored at start
        public void HttpUrl_RejectsNonHttpSchemes(string value)
        {
            Assert.DoesNotMatch(ValidationPatterns.HttpUrl, value);
        }
    }
}
