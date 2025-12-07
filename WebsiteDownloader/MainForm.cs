using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebsiteDownloader.Helpers;
using WebsiteDownloader.Models;
using WebsiteDownloader.Services;

namespace WebsiteDownloader
{
    public partial class MainForm : Form
    {
        // Win32 API for placeholder text (cue banner)
        private const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        // Error detection patterns from wget output - must be specific to avoid false positives
        // Match HTTP errors and wget-specific error messages
        private static readonly Regex ErrorPatterns = new Regex(
            @"(^ERROR[:\s]|HTTP request sent.*\b(404|403|500|502|503|504)\b|Connection refused|Connection timed out|Name or service not known|No route to host|failed:\s|FAILED)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);
        
        private static readonly Regex WarningPatterns = new Regex(
            @"(^WARNING[:\s]|Unable to establish|certificate|unlink:|Skipping)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);
        
        // Patterns to explicitly EXCLUDE from error detection (progress, downloads, etc.)
        private static readonly Regex IgnorePatterns = new Regex(
            @"(^\s*\d+K[\s\.]+|saved \[|Saving to:|Length:|Converting links|saved$|\d+%\s+\d+)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private readonly AppSettings _settings;
        private readonly DownloadHistory _history;
        private IWebsiteDownloader _downloader;
        private CancellationTokenSource _cts;
        private int _errorCount;
        private int _warningCount;

        public MainForm()
        {
            InitializeComponent();

            // Load persisted settings and history
            _settings = AppSettings.Load();
            _history = new DownloadHistory();

            // Initialize downloader with extracted wget
            InitializeDownloader();

            // Apply saved settings
            ApplySettings();

            // Set placeholder text
            SendMessage(txtUrl.Handle, EM_SETCUEBANNER, IntPtr.Zero, "https://example.com");

            // Wire up form events
            this.FormClosing += MainForm_FormClosing;
            this.Load += MainForm_Load;
        }

        private void InitializeDownloader()
        {
            try
            {
                string wgetPath = ResourceExtractor.ExtractWget();
                _downloader = new WgetDownloader(wgetPath);
                _downloader.ProgressChanged += Downloader_ProgressChanged;
                _downloader.DownloadCompleted += Downloader_DownloadCompleted;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to initialize wget: {ex.Message}\n\nThe application cannot function without wget.exe.",
                    "Initialization Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        private void ApplySettings()
        {
            if (!string.IsNullOrEmpty(_settings.DefaultOutputFolder))
            {
                lblOutputFolder.Text = _settings.DefaultOutputFolder;
            }

            // Restore window position if valid
            if (_settings.WindowX >= 0 && _settings.WindowY >= 0)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(_settings.WindowX, _settings.WindowY);
            }

            if (_settings.WindowWidth > 0 && _settings.WindowHeight > 0)
            {
                this.Size = new Size(_settings.WindowWidth, _settings.WindowHeight);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Populate recent URLs from history
            PopulateRecentUrls();
            UpdateUIState();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cancel any running download
            if (_downloader?.IsDownloading == true)
            {
                var result = MessageBox.Show(
                    "A download is in progress. Are you sure you want to exit?",
                    "Download in Progress",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

                _cts?.Cancel();
            }

            // Save window state
            _settings.WindowX = this.Location.X;
            _settings.WindowY = this.Location.Y;
            _settings.WindowWidth = this.Width;
            _settings.WindowHeight = this.Height;
            _settings.Save();

            // Cleanup
            _downloader?.Dispose();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select output folder for downloaded website";
                fbd.SelectedPath = lblOutputFolder.Text;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    lblOutputFolder.Text = fbd.SelectedPath;
                    _settings.DefaultOutputFolder = fbd.SelectedPath;
                    _settings.Save();
                    UpdateUIState();
                }
            }
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            // Validate URL
            string urlText = txtUrl.Text.Trim();
            if (string.IsNullOrEmpty(urlText))
            {
                ShowError("Please enter a website URL.");
                txtUrl.Focus();
                return;
            }

            if (!Uri.TryCreate(urlText, UriKind.Absolute, out Uri url) ||
                (url.Scheme != Uri.UriSchemeHttp && url.Scheme != Uri.UriSchemeHttps))
            {
                ShowError("Please enter a valid HTTP or HTTPS URL.\n\nExample: https://example.com");
                txtUrl.Focus();
                txtUrl.SelectAll();
                return;
            }

            // Validate output folder
            if (string.IsNullOrEmpty(lblOutputFolder.Text))
            {
                ShowError("Please select an output folder.");
                btnSelectFolder.Focus();
                return;
            }

            // Start download
            await StartDownloadAsync(url);
        }

        private async Task StartDownloadAsync(Uri url)
        {
            _cts = new CancellationTokenSource();

            var options = new DownloadOptions(
                url: url,
                outputFolder: lblOutputFolder.Text,
                userAgent: _settings.UserAgent,
                convertLinks: _settings.ConvertLinksForOffline,
                adjustExtensions: _settings.AdjustExtensions,
                maxDepth: _settings.MaxDepth,
                waitBetweenRequests: _settings.WaitBetweenRequests,
                rateLimit: _settings.RateLimit,
                noClobber: _settings.NoClobber
            );

            SetDownloadingState(true);
            ClearLog();
            LogMessage($"Starting download: {url}");
            LogMessage($"Output folder: {lblOutputFolder.Text}");
            LogMessage("---");

            try
            {
                await _downloader.DownloadAsync(options, _cts.Token);
            }
            catch (OperationCanceledException)
            {
                LogMessage("Download cancelled by user.");
            }
            catch (Exception ex)
            {
                LogMessage($"Error: {ex.Message}");
                ShowError($"Download failed: {ex.Message}");
            }
            finally
            {
                SetDownloadingState(false);
                _cts?.Dispose();
                _cts = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                LogMessage("Cancelling download...");
                _cts.Cancel();
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var settingsForm = new SettingsForm(_settings))
            {
                if (settingsForm.ShowDialog(this) == DialogResult.OK)
                {
                    _settings.Save();
                }
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            using (var historyForm = new HistoryForm(_history))
            {
                if (historyForm.ShowDialog(this) == DialogResult.OK && historyForm.SelectedUrl != null)
                {
                    txtUrl.Text = historyForm.SelectedUrl;
                }
            }
        }

        private void Downloader_ProgressChanged(object sender, DownloadProgressEventArgs e)
        {
            // Marshal to UI thread
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => HandleProgressMessage(e.Message)));
            }
            else
            {
                HandleProgressMessage(e.Message);
            }
        }

        private void HandleProgressMessage(string message)
        {
            LogMessage(message);
            
            // Skip empty messages or messages that should be ignored (progress bars, downloads, etc.)
            if (string.IsNullOrWhiteSpace(message) || IgnorePatterns.IsMatch(message))
            {
                return;
            }
            
            // Check for errors and warnings in the message
            if (ErrorPatterns.IsMatch(message))
            {
                AddError("Error", message);
            }
            else if (WarningPatterns.IsMatch(message))
            {
                AddError("Warning", message);
            }
        }

        private void AddError(string type, string message)
        {
            if (type == "Error")
                _errorCount++;
            else
                _warningCount++;
            
            var item = new ListViewItem(new[]
            {
                type,
                message.Length > 200 ? message.Substring(0, 200) + "..." : message,
                DateTime.Now.ToString("HH:mm:ss")
            });
            
            item.ForeColor = type == "Error" ? Color.Red : Color.DarkOrange;
            listViewErrors.Items.Add(item);
            
            // Update tab header and status bar
            UpdateErrorCount();
        }

        private void UpdateErrorCount()
        {
            int totalIssues = _errorCount + _warningCount;
            tabPageErrors.Text = $"⚠ Issues ({totalIssues})";
            
            if (totalIssues > 0)
            {
                statusErrorCount.Text = $"  |  ⚠ {_errorCount} error(s), {_warningCount} warning(s)";
            }
            else
            {
                statusErrorCount.Text = "";
            }
        }

        private void Downloader_DownloadCompleted(object sender, DownloadCompletedEventArgs e)
        {
            // Marshal to UI thread
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => HandleDownloadCompleted(e)));
            }
            else
            {
                HandleDownloadCompleted(e);
            }
        }

        private void HandleDownloadCompleted(DownloadCompletedEventArgs e)
        {
            int totalIssues = _errorCount + _warningCount;
            
            LogMessage("---");
            if (e.Cancelled)
            {
                LogMessage("Download cancelled.");
            }
            else if (e.Success)
            {
                LogMessage($"✓ Download completed successfully! Duration: {e.Duration:mm\\:ss}");
                if (totalIssues > 0)
                {
                    LogMessage($"  ⚠ {_errorCount} error(s), {_warningCount} warning(s) detected - check the Issues tab for details");
                }
                if (e.ExitCode != 0)
                {
                    LogMessage($"  (wget exit code {e.ExitCode} - some resources may have been unavailable)");
                }
            }
            else
            {
                LogMessage($"✗ Download failed. wget exit code: {e.ExitCode}");
                if (totalIssues > 0)
                {
                    LogMessage($"  Check the Issues tab for {_errorCount} error(s), {_warningCount} warning(s)");
                    // Switch to errors tab on failure
                    tabControlOutput.SelectedTab = tabPageErrors;
                }
            }

            // Add to history
            _history.Add(new DownloadHistoryItem
            {
                Url = e.Url.ToString(),
                OutputFolder = e.OutputFolder,
                DownloadDate = DateTime.Now,
                Duration = e.Duration,
                Success = e.Success,
                ErrorMessage = e.ErrorMessage
            });

            // Open folder if successful and setting enabled
            if (e.Success && _settings.OpenFolderAfterDownload && !e.Cancelled)
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = e.OutputFolder,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                catch
                {
                    // Ignore if folder can't be opened
                }
            }

            // Show notification
            if (_settings.ShowNotifications && !e.Cancelled)
            {
                string message = e.Success
                    ? $"Website downloaded successfully!\n{e.Url.Host}"
                    : $"Download failed for {e.Url.Host}";

                MessageBox.Show(message,
                    e.Success ? "Download Complete" : "Download Failed",
                    MessageBoxButtons.OK,
                    e.Success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
        }

        private void SetDownloadingState(bool downloading)
        {
            txtUrl.Enabled = !downloading;
            btnSelectFolder.Enabled = !downloading;
            btnDownload.Visible = !downloading;
            btnCancel.Visible = downloading;
            btnSettings.Enabled = !downloading;
            progressBar.Visible = downloading;

            if (downloading)
            {
                progressBar.Style = ProgressBarStyle.Marquee;
                statusLabel.Text = "Downloading...";
            }
            else
            {
                progressBar.Style = ProgressBarStyle.Blocks;
                statusLabel.Text = "Ready";
            }
        }

        private void UpdateUIState()
        {
            btnDownload.Enabled = !string.IsNullOrEmpty(lblOutputFolder.Text);
        }

        private void PopulateRecentUrls()
        {
            // Add recent URLs to context menu or autocomplete if desired
            if (_history.Items.Count > 0)
            {
                var recentUrl = _history.Items[0].Url;
                // Optionally pre-fill or show as placeholder
            }
        }

        private void LogMessage(string message)
        {
            if (string.IsNullOrEmpty(message)) return;

            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            txtLog.ScrollToCaret();
        }

        private void ClearLog()
        {
            txtLog.Clear();
            listViewErrors.Items.Clear();
            _errorCount = 0;
            _warningCount = 0;
            UpdateErrorCount();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
