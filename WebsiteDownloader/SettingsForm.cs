using System;
using System.Windows.Forms;
using WebsiteDownloader.Models;

namespace WebsiteDownloader
{
    public partial class SettingsForm : Form
    {
        private readonly AppSettings _settings;

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
            SaveSettings();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnResetDefaults_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to reset all settings to their default values?",
                "Reset Settings",
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
