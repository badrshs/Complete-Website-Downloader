using WebsiteDownloader.Services;
using Xunit;

namespace WebsiteDownloader.Tests
{
    /// <summary>
    /// Tests for <see cref="ResumeMode"/> and <see cref="ResumeModeExtensions"/> — the mapping
    /// from a resume choice to the single, mutually-exclusive wget flag, and the migration of the
    /// legacy NoClobber/ContinueDownload booleans.
    /// </summary>
    public class ResumeModeTests
    {
        [Theory]
        [InlineData(ResumeMode.Off, "")]
        [InlineData(ResumeMode.Continue, "-c ")]
        [InlineData(ResumeMode.Timestamping, "-N ")]
        [InlineData(ResumeMode.NoClobber, "-nc ")]
        public void ToWgetFlag_MapsEachModeToExactlyOneFlag(ResumeMode mode, string expected)
        {
            Assert.Equal(expected, mode.ToWgetFlag());
        }

        [Fact]
        public void ToWgetFlag_NeverEmitsConflictingFlags()
        {
            // Each flag string must contain at most one of -c / -N / -nc so wget never
            // receives a contradictory combination.
            foreach (ResumeMode mode in new[] { ResumeMode.Off, ResumeMode.Continue, ResumeMode.Timestamping, ResumeMode.NoClobber })
            {
                string flag = mode.ToWgetFlag();
                int count = 0;
                if (flag.Contains("-c ")) count++;
                if (flag.Contains("-N ")) count++;
                if (flag.Contains("-nc ")) count++;
                Assert.True(count <= 1, $"{mode} produced more than one resume flag: '{flag}'");
            }
        }

        [Theory]
        // noClobber wins over continueDownload because it is the stronger "skip existing" intent.
        [InlineData(true, true, ResumeMode.NoClobber)]
        [InlineData(true, false, ResumeMode.NoClobber)]
        [InlineData(false, true, ResumeMode.Continue)]
        [InlineData(false, false, ResumeMode.Off)]
        public void FromLegacyFlags_MapsBooleanPairsToMode(bool noClobber, bool continueDownload, ResumeMode expected)
        {
            Assert.Equal(expected, ResumeModeExtensions.FromLegacyFlags(noClobber, continueDownload));
        }
    }
}
