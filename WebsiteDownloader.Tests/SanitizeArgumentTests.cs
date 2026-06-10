using WebsiteDownloader.Services;
using Xunit;

namespace WebsiteDownloader.Tests
{
    /// <summary>
    /// Tests for <see cref="WgetDownloader.SanitizeArgument"/>, the defense against shell
    /// metacharacter / command-injection in values that are interpolated into the wget command line.
    /// </summary>
    public class SanitizeArgumentTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NullOrEmpty_IsReturnedUnchanged(string value)
        {
            Assert.Equal(value, WgetDownloader.SanitizeArgument(value));
        }

        [Fact]
        public void EmbeddedQuotes_AreEscaped()
        {
            Assert.Equal("\\\"hello\\\"", WgetDownloader.SanitizeArgument("\"hello\""));
        }

        [Theory]
        [InlineData("a|b", "ab")]
        [InlineData("a&b", "ab")]
        [InlineData("a;b", "ab")]
        [InlineData("a`b", "ab")]
        [InlineData("a$b", "ab")]
        [InlineData("a(b)c", "abc")]
        [InlineData("a<b>c", "abc")]
        public void ShellMetacharacters_AreStripped(string value, string expected)
        {
            Assert.Equal(expected, WgetDownloader.SanitizeArgument(value));
        }

        [Fact]
        public void CombinedInjectionAttempt_IsNeutralized()
        {
            // A nasty value mixing every special char: quotes escaped, the rest removed.
            string result = WgetDownloader.SanitizeArgument("x\"; rm -rf / & echo $(whoami)`");
            Assert.DoesNotContain("|", result);
            Assert.DoesNotContain("&", result);
            Assert.DoesNotContain(";", result);
            Assert.DoesNotContain("`", result);
            Assert.DoesNotContain("$", result);
            Assert.DoesNotContain("(", result);
            Assert.DoesNotContain(")", result);
            Assert.DoesNotContain("<", result);
            Assert.DoesNotContain(">", result);
            // Any surviving double-quote must be backslash-escaped.
            Assert.DoesNotContain("\"", result.Replace("\\\"", ""));
        }

        [Fact]
        public void OrdinaryValue_IsLeftIntact()
        {
            const string ua = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)";
            // Note: parentheses and semicolons are stripped even from benign values.
            string result = WgetDownloader.SanitizeArgument(ua);
            Assert.Equal("Mozilla/5.0 Windows NT 10.0 Win64 x64", result);
        }
    }
}
