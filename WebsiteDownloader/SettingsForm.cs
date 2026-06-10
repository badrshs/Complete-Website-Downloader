using System;
using System.Threading;
using System.Windows.Forms;
using WebsiteDownloader.Helpers;
using WebsiteDownloader.Models;
using WebsiteDownloader.Resources;
using WebsiteDownloader.Services;

namespace WebsiteDownloader
{
    public partial class SettingsForm : Form
    {
        private readonly AppSettings _settings;

        public SettingsForm(AppSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            InitializeComponent();
            PopulateResumeModes();
            PopulateDirectoryStructures();
            LoadSettings();
        }

        // Items must be added in DirectoryStructure enum order (Default, Flat, ForceFull)
        // so the combo's SelectedIndex maps directly to the enum value.
        private void PopulateDirectoryStructures()
        {
            cboDirectoryStructure.Items.Clear();
            cboDirectoryStructure.Items.Add("Mirror the site's folder structure (default)");  // Default
            cboDirectoryStructure.Items.Add("Flat - save all files into one folder");         // Flat
            cboDirectoryStructure.Items.Add("Always create the full folder hierarchy");       // ForceFull
        }

        // Items must be added in ResumeMode enum order (Off, Continue, Timestamping,
        // NoClobber) so the combo's SelectedIndex maps directly to the enum value.
        private void PopulateResumeModes()
        {
            cboResumeMode.Items.Clear();
            cboResumeMode.Items.Add("Re-download everything (overwrite)");        // Off
            cboResumeMode.Items.Add("Continue interrupted file");                 // Continue
            cboResumeMode.Items.Add("Skip up-to-date files (recommended)");       // Timestamping
            cboResumeMode.Items.Add("Skip all existing files");                   // NoClobber
            cboResumeMode.SelectedIndexChanged += cboResumeMode_SelectedIndexChanged;
        }

        private void cboResumeMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateResumeModeHint();
        }

        private void UpdateResumeModeHint()
        {
            switch ((ResumeMode)cboResumeMode.SelectedIndex)
            {
                case ResumeMode.Continue:
                    lblResumeModeHint.Text = "Resumes a single partly-downloaded file (-c). Does not skip files that already finished.";
                    break;
                case ResumeMode.Timestamping:
                    lblResumeModeHint.Text = "Skips files already up-to-date, re-fetches only missing or changed ones (-N).";
                    break;
                case ResumeMode.NoClobber:
                    lblResumeModeHint.Text = "Never re-downloads anything already on disk (-nc). Fast, but won't refresh changed files.";
                    break;
                case ResumeMode.Off:
                default:
                    lblResumeModeHint.Text = "No resume: existing files may be re-downloaded and overwritten.";
                    break;
            }
        }

        private void LoadSettings()
        {
            // Download settings
            txtUserAgent.Text = _settings.UserAgent;
            chkConvertLinks.Checked = _settings.ConvertLinksForOffline;
            chkAdjustExtensions.Checked = _settings.AdjustExtensions;
            numMaxDepth.Value = _settings.MaxDepth;
            numWaitBetweenRequests.Value = _settings.WaitBetweenRequests;
            txtRateLimit.Text = _settings.RateLimit;
            cboResumeMode.SelectedIndex = (int)_settings.ResumeMode;
            UpdateResumeModeHint();

            // New download settings
            chkIgnoreSsl.Checked = _settings.IgnoreSslErrors;
            numConnectionTimeout.Value = _settings.ConnectionTimeout;
            numReadTimeout.Value = _settings.ReadTimeout;
            numRetryCount.Value = _settings.RetryCount;

            // Download behaviour
            chkRandomWait.Checked = _settings.RandomWait;
            chkContentDisposition.Checked = _settings.ContentDisposition;
            cboDirectoryStructure.SelectedIndex = (int)_settings.DirectoryStructure;

            // Filters
            chkNoParent.Checked = _settings.NoParent;
            chkSpanHosts.Checked = _settings.SpanHosts;
            txtDomains.Text = _settings.DomainList;
            txtAcceptTypes.Text = _settings.AcceptFileTypes;
            txtRejectTypes.Text = _settings.RejectFileTypes;
            txtIncludeDirs.Text = _settings.IncludeDirectories;
            txtExcludeDirs.Text = _settings.ExcludeDirectories;
            chkIgnoreFilterCase.Checked = _settings.IgnoreFilterCase;
            txtQuota.Text = _settings.DownloadQuota;
            numMaxRedirect.Value = _settings.MaxRedirect;

            // Authentication
            txtHttpUser.Text = _settings.HttpUser;
            txtHttpPassword.Text = _settings.HttpPassword;
            txtCookiesFile.Text = _settings.CookiesFilePath;
            chkKeepSessionCookies.Checked = _settings.KeepSessionCookies;
            txtReferer.Text = _settings.Referer;
            txtCustomHeaders.Text = _settings.CustomHeaders;

            // Post-download options
            chkExportZip.Checked = _settings.ExportToZip;
            chkDeleteAfterZip.Checked = _settings.DeleteAfterZip;
            chkDeleteAfterZip.Enabled = _settings.ExportToZip;

            // Advanced options
            chkMultiThreaded.Checked = _settings.EnableMultiThreaded;
            numThreadCount.Value = _settings.ThreadCount;
            numThreadCount.Enabled = _settings.EnableMultiThreaded;

            // Bandwidth scheduler
            chkEnableScheduler.Checked = _settings.EnableBandwidthScheduler;
            txtPeakRateLimit.Text = _settings.PeakHoursRateLimit;
            txtOffPeakRateLimit.Text = _settings.OffPeakRateLimit;
            numPeakStart.Value = _settings.PeakHoursStart;
            numPeakEnd.Value = _settings.PeakHoursEnd;
            SetSchedulerControlsEnabled(_settings.EnableBandwidthScheduler);

            // Auto-update
            chkCheckUpdates.Checked = _settings.CheckForUpdates;

            // Engine
            cboEngine.SelectedIndex = (int)_settings.Engine;
            chkStripAnalytics.Checked = _settings.StripAnalyticsScripts;
            UpdateEngineStatus();

            // UI settings
            chkOpenFolderAfterDownload.Checked = _settings.OpenFolderAfterDownload;
            chkShowNotifications.Checked = _settings.ShowNotifications;
        }

        private void SaveSettings()
        {
            // Download settings
            _settings.UserAgent = txtUserAgent.Text.Trim();
            _settings.ConvertLinksForOffline = chkConvertLinks.Checked;
            _settings.AdjustExtensions = chkAdjustExtensions.Checked;
            _settings.MaxDepth = (int)numMaxDepth.Value;
            _settings.WaitBetweenRequests = (int)numWaitBetweenRequests.Value;
            _settings.RateLimit = txtRateLimit.Text.Trim();
            _settings.ResumeMode = (ResumeMode)cboResumeMode.SelectedIndex;

            // New download settings
            _settings.IgnoreSslErrors = chkIgnoreSsl.Checked;
            _settings.ConnectionTimeout = (int)numConnectionTimeout.Value;
            _settings.ReadTimeout = (int)numReadTimeout.Value;
            _settings.RetryCount = (int)numRetryCount.Value;

            // Download behaviour
            _settings.RandomWait = chkRandomWait.Checked;
            _settings.ContentDisposition = chkContentDisposition.Checked;
            _settings.DirectoryStructure = (DirectoryStructure)cboDirectoryStructure.SelectedIndex;

            // Filters
            _settings.NoParent = chkNoParent.Checked;
            _settings.SpanHosts = chkSpanHosts.Checked;
            _settings.DomainList = txtDomains.Text.Trim();
            _settings.AcceptFileTypes = txtAcceptTypes.Text.Trim();
            _settings.RejectFileTypes = txtRejectTypes.Text.Trim();
            _settings.IncludeDirectories = txtIncludeDirs.Text.Trim();
            _settings.ExcludeDirectories = txtExcludeDirs.Text.Trim();
            _settings.IgnoreFilterCase = chkIgnoreFilterCase.Checked;
            _settings.DownloadQuota = txtQuota.Text.Trim();
            _settings.MaxRedirect = (int)numMaxRedirect.Value;

            // Authentication
            _settings.HttpUser = txtHttpUser.Text.Trim();
            _settings.HttpPassword = txtHttpPassword.Text;
            _settings.CookiesFilePath = txtCookiesFile.Text.Trim();
            _settings.KeepSessionCookies = chkKeepSessionCookies.Checked;
            _settings.Referer = txtReferer.Text.Trim();
            _settings.CustomHeaders = txtCustomHeaders.Text.Trim();

            // Post-download options
            _settings.ExportToZip = chkExportZip.Checked;
            _settings.DeleteAfterZip = chkDeleteAfterZip.Checked;

            // Advanced options
            _settings.EnableMultiThreaded = chkMultiThreaded.Checked;
            _settings.ThreadCount = (int)numThreadCount.Value;

            // Bandwidth scheduler
            _settings.EnableBandwidthScheduler = chkEnableScheduler.Checked;
            _settings.PeakHoursRateLimit = txtPeakRateLimit.Text.Trim();
            _settings.OffPeakRateLimit = txtOffPeakRateLimit.Text.Trim();
            _settings.PeakHoursStart = (int)numPeakStart.Value;
            _settings.PeakHoursEnd = (int)numPeakEnd.Value;

            // Auto-update
            _settings.CheckForUpdates = chkCheckUpdates.Checked;

            // Engine
            _settings.Engine = (DownloadEngine)cboEngine.SelectedIndex;
            _settings.StripAnalyticsScripts = chkStripAnalytics.Checked;

            // UI settings
            _settings.OpenFolderAfterDownload = chkOpenFolderAfterDownload.Checked;
            _settings.ShowNotifications = chkShowNotifications.Checked;
        }

        private void SetSchedulerControlsEnabled(bool enabled)
        {
            txtPeakRateLimit.Enabled = enabled;
            txtOffPeakRateLimit.Enabled = enabled;
            numPeakStart.Enabled = enabled;
            numPeakEnd.Enabled = enabled;
        }

        private void chkExportZip_CheckedChanged(object sender, EventArgs e)
        {
            chkDeleteAfterZip.Enabled = chkExportZip.Checked;
            if (!chkExportZip.Checked)
                chkDeleteAfterZip.Checked = false;
        }

        private void chkMultiThreaded_CheckedChanged(object sender, EventArgs e)
        {
            numThreadCount.Enabled = chkMultiThreaded.Checked;
        }

        private void chkEnableScheduler_CheckedChanged(object sender, EventArgs e)
        {
            SetSchedulerControlsEnabled(chkEnableScheduler.Checked);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate inputs before saving
            if (!ValidateInputs())
                return;

            SaveSettings();
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Validates all user inputs before saving.
        /// </summary>
        /// <returns>True if all inputs are valid, false otherwise.</returns>
        private bool ValidateInputs()
        {
            // Validate User Agent
            if (string.IsNullOrWhiteSpace(txtUserAgent.Text))
            {
                ShowValidationError(Strings.ValidationUserAgentEmpty, txtUserAgent);
                return false;
            }

            // Validate Rate Limit format
            string rateLimit = txtRateLimit.Text.Trim();
            if (!ValidationPatterns.RateLimit.IsMatch(rateLimit))
            {
                ShowValidationError(Strings.ValidationRateLimitInvalid, txtRateLimit);
                return false;
            }

            // Validate Max Depth (reasonable limits)
            if (numMaxDepth.Value < 0 || numMaxDepth.Value > 100)
            {
                ShowValidationError(Strings.ValidationMaxDepthInvalid, numMaxDepth);
                return false;
            }

            // Validate Wait Between Requests (reasonable limits)
            if (numWaitBetweenRequests.Value < 0 || numWaitBetweenRequests.Value > 300)
            {
                ShowValidationError(Strings.ValidationWaitInvalid, numWaitBetweenRequests);
                return false;
            }

            // Validate quota format (same size format as rate limit)
            if (!ValidationPatterns.RateLimit.IsMatch(txtQuota.Text.Trim()))
            {
                ShowValidationError(Strings.ValidationQuotaInvalid, txtQuota);
                return false;
            }

            // Validate cookies file exists when one is given
            string cookiesFile = txtCookiesFile.Text.Trim();
            if (cookiesFile.Length > 0 && !System.IO.File.Exists(cookiesFile))
            {
                ShowValidationError(string.Format(Strings.ValidationCookiesFileNotFound, cookiesFile), txtCookiesFile);
                return false;
            }

            // Validate custom headers: each non-blank line must be "Name: value"
            foreach (var rawLine in txtCustomHeaders.Lines)
            {
                var header = rawLine.Trim();
                if (header.Length > 0 && !header.Contains(":"))
                {
                    ShowValidationError(string.Format(Strings.ValidationHeaderInvalid, header), txtCustomHeaders);
                    return false;
                }
            }

            // Validate referer is a URL when given
            string referer = txtReferer.Text.Trim();
            if (referer.Length > 0 && !ValidationPatterns.HttpUrl.IsMatch(referer))
            {
                ShowValidationError(Strings.ValidationRefererInvalid, txtReferer);
                return false;
            }

            return true;
        }

        private void btnBrowseCookies_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = Strings.CookiesFileFilter;
                ofd.Title = Strings.SettingsCookiesFile.TrimEnd(':');

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    txtCookiesFile.Text = ofd.FileName;
                }
            }
        }

        private void ShowValidationError(string message, Control control)
        {
            MessageBox.Show(
                message,
                Strings.ValidationError,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            control.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnResetDefaults_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                Strings.ConfirmResetSettingsMessage,
                Strings.ConfirmResetSettingsTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var defaults = new AppSettings();
                
                // Basic download settings
                txtUserAgent.Text = defaults.UserAgent;
                chkConvertLinks.Checked = defaults.ConvertLinksForOffline;
                chkAdjustExtensions.Checked = defaults.AdjustExtensions;
                numMaxDepth.Value = defaults.MaxDepth;
                numWaitBetweenRequests.Value = defaults.WaitBetweenRequests;
                txtRateLimit.Text = defaults.RateLimit;
                cboResumeMode.SelectedIndex = (int)defaults.ResumeMode;
                UpdateResumeModeHint();

                // New download settings
                chkIgnoreSsl.Checked = defaults.IgnoreSslErrors;
                numConnectionTimeout.Value = defaults.ConnectionTimeout;
                numReadTimeout.Value = defaults.ReadTimeout;
                numRetryCount.Value = defaults.RetryCount;

                // Download behaviour
                chkRandomWait.Checked = defaults.RandomWait;
                chkContentDisposition.Checked = defaults.ContentDisposition;
                cboDirectoryStructure.SelectedIndex = (int)defaults.DirectoryStructure;

                // Filters
                chkNoParent.Checked = defaults.NoParent;
                chkSpanHosts.Checked = defaults.SpanHosts;
                txtDomains.Text = defaults.DomainList;
                txtAcceptTypes.Text = defaults.AcceptFileTypes;
                txtRejectTypes.Text = defaults.RejectFileTypes;
                txtIncludeDirs.Text = defaults.IncludeDirectories;
                txtExcludeDirs.Text = defaults.ExcludeDirectories;
                chkIgnoreFilterCase.Checked = defaults.IgnoreFilterCase;
                txtQuota.Text = defaults.DownloadQuota;
                numMaxRedirect.Value = defaults.MaxRedirect;

                // Authentication
                txtHttpUser.Text = defaults.HttpUser;
                txtHttpPassword.Text = defaults.HttpPassword;
                txtCookiesFile.Text = defaults.CookiesFilePath;
                chkKeepSessionCookies.Checked = defaults.KeepSessionCookies;
                txtReferer.Text = defaults.Referer;
                txtCustomHeaders.Text = defaults.CustomHeaders;

                // Post-download options
                chkExportZip.Checked = defaults.ExportToZip;
                chkDeleteAfterZip.Checked = defaults.DeleteAfterZip;
                
                // Advanced options
                chkMultiThreaded.Checked = defaults.EnableMultiThreaded;
                numThreadCount.Value = defaults.ThreadCount;
                
                // Bandwidth scheduler
                chkEnableScheduler.Checked = defaults.EnableBandwidthScheduler;
                txtPeakRateLimit.Text = defaults.PeakHoursRateLimit;
                txtOffPeakRateLimit.Text = defaults.OffPeakRateLimit;
                numPeakStart.Value = defaults.PeakHoursStart;
                numPeakEnd.Value = defaults.PeakHoursEnd;
                
                // Auto-update
                chkCheckUpdates.Checked = defaults.CheckForUpdates;
                
                // UI settings
                chkOpenFolderAfterDownload.Checked = defaults.OpenFolderAfterDownload;
                chkShowNotifications.Checked = defaults.ShowNotifications;
                
                // Update control states
                chkDeleteAfterZip.Enabled = defaults.ExportToZip;
                numThreadCount.Enabled = defaults.EnableMultiThreaded;
                SetSchedulerControlsEnabled(defaults.EnableBandwidthScheduler);
                cboEngine.SelectedIndex = (int)defaults.Engine;
                chkStripAnalytics.Checked = defaults.StripAnalyticsScripts;
            }
        }

        private void cboEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEngineStatus();
        }

        private void UpdateEngineStatus()
        {
            if (cboEngine.SelectedIndex == (int)DownloadEngine.Playwright)
            {
                var reqs = PlaywrightDownloader.CheckRequirements();
                lblEngineStatus.Text = string.Format(Strings.SettingsEngineStatus, reqs.GetStatusMessage());
                btnSetupPlaywright.Visible = reqs.NodeInstalled && !reqs.CrawlerInstalled;
                
                if (!reqs.NodeInstalled)
                    lblEngineStatus.ForeColor = System.Drawing.Color.Red;
                else if (!reqs.CrawlerInstalled)
                    lblEngineStatus.ForeColor = System.Drawing.Color.DarkOrange;
                else
                    lblEngineStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblEngineStatus.Text = "";
                btnSetupPlaywright.Visible = false;
            }
        }

        private async void btnSetupPlaywright_Click(object sender, EventArgs e)
        {
            btnSetupPlaywright.Enabled = false;
            btnSetupPlaywright.Text = "Setting up...";

            var downloader = new PlaywrightDownloader();
            var success = await downloader.SetupAsync(
                msg => BeginInvoke(new Action(() => lblEngineStatus.Text = msg)),
                CancellationToken.None);

            if (success)
            {
                MessageBox.Show(
                    Strings.EngineSetupComplete,
                    Strings.EngineSetupTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    string.Format(Strings.EngineSetupFailed, lblEngineStatus.Text),
                    Strings.EngineSetupTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            btnSetupPlaywright.Text = Strings.SettingsEngineSetup;
            btnSetupPlaywright.Enabled = true;
            UpdateEngineStatus();
        }
    }
}
