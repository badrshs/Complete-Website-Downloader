using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WebsiteDownloader.Models;
using WebsiteDownloader.Resources;

namespace WebsiteDownloader
{
    public partial class SettingsForm : Form
    {
        private readonly AppSettings _settings;

        /// <summary>
        /// Regex pattern for validating rate limit format (e.g., "200k", "1m", "500").
        /// </summary>
        private static readonly Regex RateLimitPattern = new Regex(
            @"^$|^\d+[kmg]?$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public SettingsForm(AppSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            InitializeComponent();
            LoadSettings();
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
            chkNoClobber.Checked = _settings.NoClobber;

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
            _settings.NoClobber = chkNoClobber.Checked;

            // UI settings
            _settings.OpenFolderAfterDownload = chkOpenFolderAfterDownload.Checked;
            _settings.ShowNotifications = chkShowNotifications.Checked;
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
            if (!RateLimitPattern.IsMatch(rateLimit))
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

            return true;
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
                
                txtUserAgent.Text = defaults.UserAgent;
                chkConvertLinks.Checked = defaults.ConvertLinksForOffline;
                chkAdjustExtensions.Checked = defaults.AdjustExtensions;
                numMaxDepth.Value = defaults.MaxDepth;
                numWaitBetweenRequests.Value = defaults.WaitBetweenRequests;
                txtRateLimit.Text = defaults.RateLimit;
                chkNoClobber.Checked = defaults.NoClobber;
                chkOpenFolderAfterDownload.Checked = defaults.OpenFolderAfterDownload;
                chkShowNotifications.Checked = defaults.ShowNotifications;
            }
        }
    }
}
