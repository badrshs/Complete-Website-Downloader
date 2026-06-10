using WebsiteDownloader.Services;
using Xunit;

namespace WebsiteDownloader.Tests
{
    /// <summary>
    /// Tests for <see cref="SizeEstimationResult.FormattedSize"/>, the human-readable byte
    /// formatter (B / KB / MB / GB) with thresholds at each 1024 boundary.
    /// </summary>
    public class SizeEstimationResultTests
    {
        private static string Format(long bytes) =>
            new SizeEstimationResult { TotalBytes = bytes }.FormattedSize;

        [Fact]
        public void Zero_IsBytes()
        {
            Assert.Equal("0 B", Format(0));
        }

        [Fact]
        public void JustUnderOneKilobyte_StaysInBytes()
        {
            Assert.Equal("1023 B", Format(1023));
        }

        [Fact]
        public void Kilobytes_UseOneDecimal()
        {
            // 1024 B -> 1.0 KB. Build the expected string with the current culture's separator
            // so the assertion holds regardless of the machine's locale.
            string expected = (1024 / 1024.0).ToString("F1") + " KB";
            Assert.Equal(expected, Format(1024));
        }

        [Fact]
        public void Megabytes_UseOneDecimal()
        {
            long bytes = 5L * 1024 * 1024;        // 5 MB
            string expected = (bytes / (1024.0 * 1024)).ToString("F1") + " MB";
            Assert.Equal(expected, Format(bytes));
        }

        [Fact]
        public void Gigabytes_UseTwoDecimals()
        {
            long bytes = 3L * 1024 * 1024 * 1024; // 3 GB
            string expected = (bytes / (1024.0 * 1024 * 1024)).ToString("F2") + " GB";
            Assert.Equal(expected, Format(bytes));
        }

        [Fact]
        public void BoundaryAtOneMegabyte_SwitchesUnitFromKbToMb()
        {
            Assert.EndsWith(" KB", Format(1024 * 1024 - 1));
            Assert.EndsWith(" MB", Format(1024 * 1024));
        }
    }
}
