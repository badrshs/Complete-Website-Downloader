using Xunit;

namespace WebsiteDownloader.Tests
{
    /// <summary>
    /// Groups all tests that redirect <see cref="AppConstants.AppDataFolderOverride"/> (a shared
    /// static) into one collection so xUnit runs them serially rather than in parallel, preventing
    /// the override from being clobbered by a concurrently-running test.
    /// </summary>
    [CollectionDefinition(Name)]
    public class PersistenceCollection
    {
        public const string Name = "Persistence (serialized)";
    }
}
