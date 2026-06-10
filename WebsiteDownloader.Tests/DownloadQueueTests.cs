using System;
using System.Linq;
using WebsiteDownloader.Models;
using Xunit;

namespace WebsiteDownloader.Tests
{
    /// <summary>
    /// Tests for <see cref="DownloadQueue"/> — add/dedup, priority-based selection, status updates,
    /// removal/clearing, priority adjustment, and crash-recovery on load (Downloading -> Pending).
    /// Uses a redirected AppData folder.
    /// </summary>
    [Collection(PersistenceCollection.Name)]
    public class DownloadQueueTests : IDisposable
    {
        private readonly TempDir _temp;

        public DownloadQueueTests()
        {
            _temp = new TempDir();
            AppConstants.AppDataFolderOverride = _temp.Path;
        }

        public void Dispose()
        {
            AppConstants.AppDataFolderOverride = null;
            _temp.Dispose();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Add_NullOrWhitespaceUrl_ReturnsNull(string url)
        {
            var queue = new DownloadQueue();
            Assert.Null(queue.Add(url, @"C:\out"));
            Assert.Empty(queue.Items);
        }

        [Fact]
        public void Add_DuplicatePendingUrl_IsRejected()
        {
            var queue = new DownloadQueue();
            Assert.NotNull(queue.Add("https://a.com", @"C:\out"));
            Assert.Null(queue.Add("https://a.com", @"C:\out"));   // duplicate while still Pending
            Assert.Single(queue.Items);
        }

        [Fact]
        public void PendingCount_CountsOnlyPendingItems()
        {
            var queue = new DownloadQueue();
            var a = queue.Add("https://a.com", @"C:\out");
            queue.Add("https://b.com", @"C:\out");
            queue.UpdateStatus(a.Id, QueueItemStatus.Completed);

            Assert.Equal(1, queue.PendingCount);
        }

        [Fact]
        public void GetNext_PrefersHigherPriority()
        {
            var queue = new DownloadQueue();
            queue.Add("https://low.com", @"C:\out", priority: 1);
            queue.Add("https://high.com", @"C:\out", priority: 5);

            Assert.Equal("https://high.com", queue.GetNext().Url);
        }

        [Fact]
        public void GetNext_IgnoresNonPendingItems()
        {
            var queue = new DownloadQueue();
            var a = queue.Add("https://a.com", @"C:\out");
            queue.UpdateStatus(a.Id, QueueItemStatus.Completed);

            Assert.Null(queue.GetNext());
        }

        [Fact]
        public void AddRange_AddsAll_AndReturnsCount()
        {
            var queue = new DownloadQueue();
            int added = queue.AddRange(new[] { "https://a.com", "https://b.com", "  ", "https://c.com" }, @"C:\out");

            Assert.Equal(3, added);                 // whitespace entry skipped
            Assert.Equal(3, queue.Items.Count);
        }

        [Fact]
        public void UpdateStatus_SetsStatusAndError()
        {
            var queue = new DownloadQueue();
            var a = queue.Add("https://a.com", @"C:\out");
            queue.UpdateStatus(a.Id, QueueItemStatus.Failed, "boom");

            var item = queue.Items.Single();
            Assert.Equal(QueueItemStatus.Failed, item.Status);
            Assert.Equal("boom", item.ErrorMessage);
        }

        [Fact]
        public void Remove_DeletesById_AndReturnsWhetherFound()
        {
            var queue = new DownloadQueue();
            var a = queue.Add("https://a.com", @"C:\out");

            Assert.True(queue.Remove(a.Id));
            Assert.False(queue.Remove(a.Id));       // already gone
            Assert.Empty(queue.Items);
        }

        [Fact]
        public void ClearCompleted_RemovesFinishedButKeepsPending()
        {
            var queue = new DownloadQueue();
            var done = queue.Add("https://done.com", @"C:\out");
            queue.Add("https://pending.com", @"C:\out");
            queue.UpdateStatus(done.Id, QueueItemStatus.Completed);

            queue.ClearCompleted();

            var remaining = queue.Items.Single();
            Assert.Equal("https://pending.com", remaining.Url);
        }

        [Fact]
        public void DecreasePriority_DoesNotGoBelowZero()
        {
            var queue = new DownloadQueue();
            var a = queue.Add("https://a.com", @"C:\out", priority: 0);

            queue.DecreasePriority(a.Id);
            Assert.Equal(0, queue.Items.Single().Priority);

            queue.IncreasePriority(a.Id);
            Assert.Equal(1, queue.Items.Single().Priority);
        }

        [Fact]
        public void Load_ResetsInterruptedDownloadingItemsToPending()
        {
            // First instance: mark an item as Downloading (simulating a crash mid-download), persisted to disk.
            var first = new DownloadQueue();
            var a = first.Add("https://a.com", @"C:\out");
            first.UpdateStatus(a.Id, QueueItemStatus.Downloading);

            // Second instance loads the same file and should recover the stuck item to Pending.
            var reloaded = new DownloadQueue();
            Assert.Equal(QueueItemStatus.Pending, reloaded.Items.Single().Status);
        }
    }
}
