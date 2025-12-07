using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace WebsiteDownloader.Models
{
    /// <summary>
    /// Represents a queued download item.
    /// </summary>
    public class QueuedDownloadItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Url { get; set; }
        public string OutputFolder { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public QueueItemStatus Status { get; set; } = QueueItemStatus.Pending;
        public int Priority { get; set; } = 0;  // Higher = more priority
        public string ErrorMessage { get; set; }
        public int RetryCount { get; set; } = 0;
    }

    /// <summary>
    /// Status of a queued download item.
    /// </summary>
    public enum QueueItemStatus
    {
        Pending,
        Downloading,
        Completed,
        Failed,
        Cancelled,
        Paused
    }

    /// <summary>
    /// Manages the download queue with persistence and thread-safe operations.
    /// </summary>
    public class DownloadQueue
    {
        private List<QueuedDownloadItem> _items;
        private readonly object _lockObject = new object();

        /// <summary>
        /// Event raised when the queue changes.
        /// </summary>
        public event EventHandler QueueChanged;

        /// <summary>
        /// Gets a read-only view of all queue items.
        /// </summary>
        public IReadOnlyList<QueuedDownloadItem> Items
        {
            get
            {
                lock (_lockObject)
                {
                    return _items.ToArray();
                }
            }
        }

        /// <summary>
        /// Gets the count of pending items.
        /// </summary>
        public int PendingCount
        {
            get
            {
                lock (_lockObject)
                {
                    return _items.Count(i => i.Status == QueueItemStatus.Pending);
                }
            }
        }

        public DownloadQueue()
        {
            EnsureAppDataFolderExists();
            Load();
        }

        /// <summary>
        /// Adds a URL to the download queue.
        /// </summary>
        public QueuedDownloadItem Add(string url, string outputFolder, int priority = 0)
        {
            if (string.IsNullOrWhiteSpace(url)) return null;

            var item = new QueuedDownloadItem
            {
                Url = url,
                OutputFolder = outputFolder,
                Priority = priority
            };

            lock (_lockObject)
            {
                // Check for duplicates
                if (_items.Any(i => i.Url == url && i.Status == QueueItemStatus.Pending))
                    return null;

                _items.Add(item);

                // Keep queue size manageable
                if (_items.Count > AppConstants.MaxQueueItems)
                {
                    var completedOrFailed = _items
                        .Where(i => i.Status == QueueItemStatus.Completed || i.Status == QueueItemStatus.Failed)
                        .OrderBy(i => i.AddedDate)
                        .FirstOrDefault();
                    
                    if (completedOrFailed != null)
                        _items.Remove(completedOrFailed);
                }

                Save();
            }

            OnQueueChanged();
            return item;
        }

        /// <summary>
        /// Adds multiple URLs to the queue.
        /// </summary>
        public int AddRange(IEnumerable<string> urls, string outputFolder)
        {
            int addedCount = 0;
            foreach (var url in urls.Where(u => !string.IsNullOrWhiteSpace(u)))
            {
                if (Add(url, outputFolder) != null)
                    addedCount++;
            }
            return addedCount;
        }

        /// <summary>
        /// Gets the next pending item in the queue (highest priority first).
        /// </summary>
        public QueuedDownloadItem GetNext()
        {
            lock (_lockObject)
            {
                return _items
                    .Where(i => i.Status == QueueItemStatus.Pending)
                    .OrderByDescending(i => i.Priority)
                    .ThenBy(i => i.AddedDate)
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Updates the status of a queue item.
        /// </summary>
        public void UpdateStatus(string id, QueueItemStatus status, string errorMessage = null)
        {
            lock (_lockObject)
            {
                var item = _items.FirstOrDefault(i => i.Id == id);
                if (item != null)
                {
                    item.Status = status;
                    if (errorMessage != null)
                        item.ErrorMessage = errorMessage;
                    Save();
                }
            }
            OnQueueChanged();
        }

        /// <summary>
        /// Removes an item from the queue.
        /// </summary>
        public bool Remove(string id)
        {
            lock (_lockObject)
            {
                var item = _items.FirstOrDefault(i => i.Id == id);
                if (item != null)
                {
                    _items.Remove(item);
                    Save();
                    OnQueueChanged();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Clears completed and failed items from the queue.
        /// </summary>
        public void ClearCompleted()
        {
            lock (_lockObject)
            {
                _items.RemoveAll(i => i.Status == QueueItemStatus.Completed || 
                                      i.Status == QueueItemStatus.Failed ||
                                      i.Status == QueueItemStatus.Cancelled);
                Save();
            }
            OnQueueChanged();
        }

        /// <summary>
        /// Clears all items from the queue.
        /// </summary>
        public void ClearAll()
        {
            lock (_lockObject)
            {
                _items.Clear();
                Save();
            }
            OnQueueChanged();
        }

        /// <summary>
        /// Moves an item up in priority.
        /// </summary>
        public void IncreasePriority(string id)
        {
            lock (_lockObject)
            {
                var item = _items.FirstOrDefault(i => i.Id == id);
                if (item != null)
                {
                    item.Priority++;
                    Save();
                }
            }
            OnQueueChanged();
        }

        /// <summary>
        /// Moves an item down in priority.
        /// </summary>
        public void DecreasePriority(string id)
        {
            lock (_lockObject)
            {
                var item = _items.FirstOrDefault(i => i.Id == id);
                if (item != null && item.Priority > 0)
                {
                    item.Priority--;
                    Save();
                }
            }
            OnQueueChanged();
        }

        private void EnsureAppDataFolderExists()
        {
            try
            {
                if (!Directory.Exists(AppConstants.AppDataFolder))
                    Directory.CreateDirectory(AppConstants.AppDataFolder);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to create app data folder: {ex.Message}");
            }
        }

        private void Load()
        {
            _items = new List<QueuedDownloadItem>();
            string queueFilePath = Path.Combine(AppConstants.AppDataFolder, AppConstants.QueueFileName);

            try
            {
                if (File.Exists(queueFilePath))
                {
                    string json = File.ReadAllText(queueFilePath);
                    _items = JsonConvert.DeserializeObject<List<QueuedDownloadItem>>(json) 
                             ?? new List<QueuedDownloadItem>();
                    
                    // Reset any "Downloading" items to "Pending" (app was closed during download)
                    foreach (var item in _items.Where(i => i.Status == QueueItemStatus.Downloading))
                    {
                        item.Status = QueueItemStatus.Pending;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load queue: {ex.Message}");
                _items = new List<QueuedDownloadItem>();
            }
        }

        private void Save()
        {
            string queueFilePath = Path.Combine(AppConstants.AppDataFolder, AppConstants.QueueFileName);

            try
            {
                // Use atomic write pattern
                string tempPath = queueFilePath + ".tmp";
                string json = JsonConvert.SerializeObject(_items, Formatting.Indented);
                
                File.WriteAllText(tempPath, json);
                
                if (File.Exists(queueFilePath))
                    File.Delete(queueFilePath);
                File.Move(tempPath, queueFilePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to save queue: {ex.Message}");
            }
        }

        private void OnQueueChanged()
        {
            QueueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
