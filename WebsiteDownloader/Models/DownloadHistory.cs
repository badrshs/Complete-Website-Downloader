using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace WebsiteDownloader.Models
{
    /// <summary>
    /// Represents a download history entry
    /// </summary>
    public class DownloadHistoryItem
    {
        public string Url { get; set; }
        public string OutputFolder { get; set; }
        public DateTime DownloadDate { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Manages download history persistence with thread-safe operations.
    /// </summary>
    public class DownloadHistory
    {
        private List<DownloadHistoryItem> _items;
        private readonly object _lockObject = new object();

        public IReadOnlyList<DownloadHistoryItem> Items
        {
            get
            {
                lock (_lockObject)
                {
                    return _items.ToArray();
                }
            }
        }

        public DownloadHistory()
        {
            EnsureAppDataFolderExists();
            Load();
        }

        public void Add(DownloadHistoryItem item)
        {
            if (item == null) return;

            lock (_lockObject)
            {
                _items.Insert(0, item);

                // Keep only last N items
                if (_items.Count > AppConstants.MaxHistoryItems)
                    _items.RemoveRange(AppConstants.MaxHistoryItems, _items.Count - AppConstants.MaxHistoryItems);

                Save();
            }
        }

        public void Clear()
        {
            lock (_lockObject)
            {
                _items.Clear();
                Save();
            }
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
            _items = new List<DownloadHistoryItem>();

            try
            {
                if (File.Exists(AppConstants.HistoryFilePath))
                {
                    string json = File.ReadAllText(AppConstants.HistoryFilePath);
                    _items = JsonConvert.DeserializeObject<List<DownloadHistoryItem>>(json) 
                             ?? new List<DownloadHistoryItem>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load history: {ex.Message}");
                _items = new List<DownloadHistoryItem>();
            }
        }

        private void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_items, Formatting.Indented);
                File.WriteAllText(AppConstants.HistoryFilePath, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to save history: {ex.Message}");
            }
        }
    }
}
