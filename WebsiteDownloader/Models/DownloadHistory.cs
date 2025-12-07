using System;
using System.Collections.Generic;
using System.IO;
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
    /// Manages download history persistence
    /// </summary>
    public class DownloadHistory
    {
        private const int MaxHistoryItems = 50;
        private readonly string _historyFilePath;
        private List<DownloadHistoryItem> _items;

        public IReadOnlyList<DownloadHistoryItem> Items => _items.AsReadOnly();

        public DownloadHistory()
        {
            string appDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WebsiteDownloader");

            if (!Directory.Exists(appDataFolder))
                Directory.CreateDirectory(appDataFolder);

            _historyFilePath = Path.Combine(appDataFolder, "history.json");
            Load();
        }

        public void Add(DownloadHistoryItem item)
        {
            if (item == null) return;

            _items.Insert(0, item);

            // Keep only last N items
            if (_items.Count > MaxHistoryItems)
                _items.RemoveRange(MaxHistoryItems, _items.Count - MaxHistoryItems);

            Save();
        }

        public void Clear()
        {
            _items.Clear();
            Save();
        }

        private void Load()
        {
            _items = new List<DownloadHistoryItem>();

            try
            {
                if (File.Exists(_historyFilePath))
                {
                    string json = File.ReadAllText(_historyFilePath);
                    _items = JsonConvert.DeserializeObject<List<DownloadHistoryItem>>(json) 
                             ?? new List<DownloadHistoryItem>();
                }
            }
            catch
            {
                // If file is corrupted, start fresh
                _items = new List<DownloadHistoryItem>();
            }
        }

        private void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_items, Formatting.Indented);
                File.WriteAllText(_historyFilePath, json);
            }
            catch
            {
                // Saving history is best-effort
            }
        }
    }
}
