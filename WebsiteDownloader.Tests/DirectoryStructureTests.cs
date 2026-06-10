using WebsiteDownloader.Services;
using Xunit;

namespace WebsiteDownloader.Tests
{
    /// <summary>
    /// Tests for <see cref="DirectoryStructureExtensions.ToWgetFlag"/> — the mapping of
    /// each layout mode to its single, mutually-exclusive wget flag.
    /// </summary>
    public class DirectoryStructureTests
    {
        [Theory]
        [InlineData(DirectoryStructure.Default, "")]
        [InlineData(DirectoryStructure.Flat, "-nd ")]
        [InlineData(DirectoryStructure.ForceFull, "-x ")]
        public void ToWgetFlag_MapsEachModeToItsFlag(DirectoryStructure structure, string expected)
        {
            Assert.Equal(expected, structure.ToWgetFlag());
        }

        [Fact]
        public void ToWgetFlag_UndefinedValue_FallsBackToEmpty()
        {
            Assert.Equal(string.Empty, ((DirectoryStructure)99).ToWgetFlag());
        }
    }
}
