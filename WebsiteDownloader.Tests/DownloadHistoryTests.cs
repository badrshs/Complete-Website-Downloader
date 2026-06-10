using System;
using System.Linq;
using WebsiteDownloader.Models;
using Xunit;

namespace WebsiteDownloader.Tests
{
    /// <summary>
    /// Tests for <see cref="DownloadHistory"/> — most-recent-first ordering, the retained-item cap,
    /// null handling, clearing, and persistence across instances. Uses a redirected AppData folder.
    /// </summary>
    [Collection(PersistenceCollection.Name)]
    public class DownloadHistoryTests : IDisposable
    {
        private readonly TempDir _temp;

        public DownloadHistoryTests()
        {
            _temp = new TempDir();
            AppConstants.AppDataFolderOverride = _temp.Path;
        }

        public void Dispose()
        {
            AppConstants.AppDataFolderOverride = null;
            _temp.Dispose();
        }

        private static DownloadHistoryItem Item(string url) => new DownloadHistoryItem
        {
            Url = url,
            OutputFolder = @"C:\out",
            DownloadDate = new DateTime(2026, 1, 1),
            Duration = TimeSpan.FromSeconds(5),
            Success = true,
        };

        [Fact]
        public void Add_InsertsMostRecentFirst()
        {
            var history = new DownloadHistory();
            history.Add(Item("https://a.com"));
            history.Add(Item("https://b.com"));

            Assert.Equal("https://b.com", history.Items[0].Url);
            Assert.Equal("https://a.com", history.Items[1].Url);
        }

        [Fact]
        public void Add_Null_IsIgnored()
        {
            var history = new DownloadHistory();
            history.Add(null);
            Assert.Empty(history.Items);
        }

        [Fact]
        public void Add_TrimsToMaxHistoryItems()
        {
            var history = new DownloadHistory();
            for (int i = 0; i < AppConstants.MaxHistoryItems + 10; i++)
                history.Add(Item($"https://site{i}.com"));

            Assert.Equal(AppConstants.MaxHistoryItems, history.Items.Count);
        }

        [Fact]
        public void Clear_RemovesEverything()
        {
            var history = new DownloadHistory();
            history.Add(Item("https://a.com"));
            history.Clear();
            Assert.Empty(history.Items);
        }

        [Fact]
        public void History_PersistsAcrossInstances()
        {
            new DownloadHistory().Add(Item("https://persist.com"));

            // A fresh instance loads from the same (redirected) file.
            var reloaded = new DownloadHistory();
            Assert.Single(reloaded.Items);
            Assert.Equal("https://persist.com", reloaded.Items[0].Url);
        }

        [Fact]
        public void Items_ReturnsSnapshot_NotLiveReference()
        {
            var history = new DownloadHistory();
            history.Add(Item("https://a.com"));
            var snapshot = history.Items;
            history.Add(Item("https://b.com"));

            // The earlier snapshot is unaffected by the later Add.
            Assert.Single(snapshot);
        }
    }
}
