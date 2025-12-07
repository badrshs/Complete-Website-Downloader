using System.Windows.Forms;

namespace WebsiteDownloader
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDownload = new System.Windows.Forms.TabPage();
            this.tabUI = new System.Windows.Forms.TabPage();
            
            // Download tab controls
            this.lblUserAgent = new System.Windows.Forms.Label();
            this.txtUserAgent = new System.Windows.Forms.TextBox();
            this.chkConvertLinks = new System.Windows.Forms.CheckBox();
            this.chkAdjustExtensions = new System.Windows.Forms.CheckBox();
            this.lblMaxDepth = new System.Windows.Forms.Label();
            this.numMaxDepth = new System.Windows.Forms.NumericUpDown();
            this.lblMaxDepthHint = new System.Windows.Forms.Label();
            this.lblWaitBetweenRequests = new System.Windows.Forms.Label();
            this.numWaitBetweenRequests = new System.Windows.Forms.NumericUpDown();
            this.lblWaitHint = new System.Windows.Forms.Label();
            this.lblRateLimit = new System.Windows.Forms.Label();
            this.txtRateLimit = new System.Windows.Forms.TextBox();
            this.lblRateLimitHint = new System.Windows.Forms.Label();
            this.chkNoClobber = new System.Windows.Forms.CheckBox();
            
            // UI tab controls
            this.chkOpenFolderAfterDownload = new System.Windows.Forms.CheckBox();
            this.chkShowNotifications = new System.Windows.Forms.CheckBox();
            
            // Buttons
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnResetDefaults = new System.Windows.Forms.Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.numMaxDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitBetweenRequests)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabDownload.SuspendLayout();
            this.tabUI.SuspendLayout();
            this.SuspendLayout();
            
            // tabControl
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabDownload);
            this.tabControl.Controls.Add(this.tabUI);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(460, 320);
            this.tabControl.TabIndex = 0;
            
            // tabDownload
            this.tabDownload.Controls.Add(this.lblUserAgent);
            this.tabDownload.Controls.Add(this.txtUserAgent);
            this.tabDownload.Controls.Add(this.chkConvertLinks);
            this.tabDownload.Controls.Add(this.chkAdjustExtensions);
            this.tabDownload.Controls.Add(this.lblMaxDepth);
            this.tabDownload.Controls.Add(this.numMaxDepth);
            this.tabDownload.Controls.Add(this.lblMaxDepthHint);
            this.tabDownload.Controls.Add(this.lblWaitBetweenRequests);
            this.tabDownload.Controls.Add(this.numWaitBetweenRequests);
            this.tabDownload.Controls.Add(this.lblWaitHint);
            this.tabDownload.Controls.Add(this.lblRateLimit);
            this.tabDownload.Controls.Add(this.txtRateLimit);
            this.tabDownload.Controls.Add(this.lblRateLimitHint);
            this.tabDownload.Controls.Add(this.chkNoClobber);
            this.tabDownload.Location = new System.Drawing.Point(4, 24);
            this.tabDownload.Name = "tabDownload";
            this.tabDownload.Padding = new System.Windows.Forms.Padding(12);
            this.tabDownload.Size = new System.Drawing.Size(452, 292);
            this.tabDownload.TabIndex = 0;
            this.tabDownload.Text = "Download";
            this.tabDownload.UseVisualStyleBackColor = true;
            
            // lblUserAgent
            this.lblUserAgent.AutoSize = true;
            this.lblUserAgent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUserAgent.Location = new System.Drawing.Point(12, 15);
            this.lblUserAgent.Name = "lblUserAgent";
            this.lblUserAgent.Size = new System.Drawing.Size(68, 15);
            this.lblUserAgent.TabIndex = 0;
            this.lblUserAgent.Text = "User Agent:";
            
            // txtUserAgent
            this.txtUserAgent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserAgent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUserAgent.Location = new System.Drawing.Point(12, 33);
            this.txtUserAgent.Name = "txtUserAgent";
            this.txtUserAgent.Size = new System.Drawing.Size(420, 23);
            this.txtUserAgent.TabIndex = 1;
            
            // chkConvertLinks
            this.chkConvertLinks.AutoSize = true;
            this.chkConvertLinks.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkConvertLinks.Location = new System.Drawing.Point(12, 68);
            this.chkConvertLinks.Name = "chkConvertLinks";
            this.chkConvertLinks.Size = new System.Drawing.Size(208, 19);
            this.chkConvertLinks.TabIndex = 2;
            this.chkConvertLinks.Text = "Convert links for offline viewing (-k)";
            this.chkConvertLinks.UseVisualStyleBackColor = true;
            
            // chkAdjustExtensions
            this.chkAdjustExtensions.AutoSize = true;
            this.chkAdjustExtensions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkAdjustExtensions.Location = new System.Drawing.Point(12, 93);
            this.chkAdjustExtensions.Name = "chkAdjustExtensions";
            this.chkAdjustExtensions.Size = new System.Drawing.Size(194, 19);
            this.chkAdjustExtensions.TabIndex = 3;
            this.chkAdjustExtensions.Text = "Adjust file extensions to .html (-E)";
            this.chkAdjustExtensions.UseVisualStyleBackColor = true;
            
            // lblMaxDepth
            this.lblMaxDepth.AutoSize = true;
            this.lblMaxDepth.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaxDepth.Location = new System.Drawing.Point(12, 125);
            this.lblMaxDepth.Name = "lblMaxDepth";
            this.lblMaxDepth.Size = new System.Drawing.Size(112, 15);
            this.lblMaxDepth.TabIndex = 4;
            this.lblMaxDepth.Text = "Maximum depth (-l):";
            
            // numMaxDepth
            this.numMaxDepth.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numMaxDepth.Location = new System.Drawing.Point(140, 123);
            this.numMaxDepth.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numMaxDepth.Name = "numMaxDepth";
            this.numMaxDepth.Size = new System.Drawing.Size(60, 23);
            this.numMaxDepth.TabIndex = 5;
            
            // lblMaxDepthHint
            this.lblMaxDepthHint.AutoSize = true;
            this.lblMaxDepthHint.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblMaxDepthHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblMaxDepthHint.Location = new System.Drawing.Point(206, 127);
            this.lblMaxDepthHint.Name = "lblMaxDepthHint";
            this.lblMaxDepthHint.Size = new System.Drawing.Size(78, 13);
            this.lblMaxDepthHint.TabIndex = 6;
            this.lblMaxDepthHint.Text = "(0 = unlimited)";
            
            // lblWaitBetweenRequests
            this.lblWaitBetweenRequests.AutoSize = true;
            this.lblWaitBetweenRequests.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWaitBetweenRequests.Location = new System.Drawing.Point(12, 160);
            this.lblWaitBetweenRequests.Name = "lblWaitBetweenRequests";
            this.lblWaitBetweenRequests.Size = new System.Drawing.Size(175, 15);
            this.lblWaitBetweenRequests.TabIndex = 7;
            this.lblWaitBetweenRequests.Text = "Wait between requests (-w) sec:";
            
            // numWaitBetweenRequests
            this.numWaitBetweenRequests.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numWaitBetweenRequests.Location = new System.Drawing.Point(195, 158);
            this.numWaitBetweenRequests.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            this.numWaitBetweenRequests.Name = "numWaitBetweenRequests";
            this.numWaitBetweenRequests.Size = new System.Drawing.Size(60, 23);
            this.numWaitBetweenRequests.TabIndex = 8;
            
            // lblWaitHint
            this.lblWaitHint.AutoSize = true;
            this.lblWaitHint.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblWaitHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblWaitHint.Location = new System.Drawing.Point(261, 162);
            this.lblWaitHint.Name = "lblWaitHint";
            this.lblWaitHint.Size = new System.Drawing.Size(109, 13);
            this.lblWaitHint.TabIndex = 9;
            this.lblWaitHint.Text = "(be polite to servers)";
            
            // lblRateLimit
            this.lblRateLimit.AutoSize = true;
            this.lblRateLimit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRateLimit.Location = new System.Drawing.Point(12, 195);
            this.lblRateLimit.Name = "lblRateLimit";
            this.lblRateLimit.Size = new System.Drawing.Size(106, 15);
            this.lblRateLimit.TabIndex = 10;
            this.lblRateLimit.Text = "Rate limit (--limit):";
            
            // txtRateLimit
            this.txtRateLimit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRateLimit.Location = new System.Drawing.Point(125, 192);
            this.txtRateLimit.Name = "txtRateLimit";
            this.txtRateLimit.Size = new System.Drawing.Size(80, 23);
            this.txtRateLimit.TabIndex = 11;
            
            // lblRateLimitHint
            this.lblRateLimitHint.AutoSize = true;
            this.lblRateLimitHint.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblRateLimitHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblRateLimitHint.Location = new System.Drawing.Point(211, 196);
            this.lblRateLimitHint.Name = "lblRateLimitHint";
            this.lblRateLimitHint.Size = new System.Drawing.Size(129, 13);
            this.lblRateLimitHint.TabIndex = 12;
            this.lblRateLimitHint.Text = "(e.g., 200k, 1m, or blank)";
            
            // chkNoClobber
            this.chkNoClobber.AutoSize = true;
            this.chkNoClobber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkNoClobber.Location = new System.Drawing.Point(12, 230);
            this.chkNoClobber.Name = "chkNoClobber";
            this.chkNoClobber.Size = new System.Drawing.Size(225, 19);
            this.chkNoClobber.TabIndex = 13;
            this.chkNoClobber.Text = "Don't overwrite existing files (-nc)";
            this.chkNoClobber.UseVisualStyleBackColor = true;
            
            // tabUI
            this.tabUI.Controls.Add(this.chkOpenFolderAfterDownload);
            this.tabUI.Controls.Add(this.chkShowNotifications);
            this.tabUI.Location = new System.Drawing.Point(4, 24);
            this.tabUI.Name = "tabUI";
            this.tabUI.Padding = new System.Windows.Forms.Padding(12);
            this.tabUI.Size = new System.Drawing.Size(452, 292);
            this.tabUI.TabIndex = 1;
            this.tabUI.Text = "Behavior";
            this.tabUI.UseVisualStyleBackColor = true;
            
            // chkOpenFolderAfterDownload
            this.chkOpenFolderAfterDownload.AutoSize = true;
            this.chkOpenFolderAfterDownload.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkOpenFolderAfterDownload.Location = new System.Drawing.Point(12, 15);
            this.chkOpenFolderAfterDownload.Name = "chkOpenFolderAfterDownload";
            this.chkOpenFolderAfterDownload.Size = new System.Drawing.Size(234, 19);
            this.chkOpenFolderAfterDownload.TabIndex = 0;
            this.chkOpenFolderAfterDownload.Text = "Open folder after download completes";
            this.chkOpenFolderAfterDownload.UseVisualStyleBackColor = true;
            
            // chkShowNotifications
            this.chkShowNotifications.AutoSize = true;
            this.chkShowNotifications.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkShowNotifications.Location = new System.Drawing.Point(12, 40);
            this.chkShowNotifications.Name = "chkShowNotifications";
            this.chkShowNotifications.Size = new System.Drawing.Size(218, 19);
            this.chkShowNotifications.TabIndex = 1;
            this.chkShowNotifications.Text = "Show notification when complete";
            this.chkShowNotifications.UseVisualStyleBackColor = true;
            
            // btnSave
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(382, 345);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 35);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // btnCancel
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(282, 345);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            // btnResetDefaults
            this.btnResetDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetDefaults.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnResetDefaults.Location = new System.Drawing.Point(12, 345);
            this.btnResetDefaults.Name = "btnResetDefaults";
            this.btnResetDefaults.Size = new System.Drawing.Size(110, 35);
            this.btnResetDefaults.TabIndex = 3;
            this.btnResetDefaults.Text = "Reset Defaults";
            this.btnResetDefaults.UseVisualStyleBackColor = true;
            this.btnResetDefaults.Click += new System.EventHandler(this.btnResetDefaults_Click);
            
            // SettingsForm
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 391);
            this.Controls.Add(this.btnResetDefaults);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            
            ((System.ComponentModel.ISupportInitialize)(this.numMaxDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitBetweenRequests)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabDownload.ResumeLayout(false);
            this.tabDownload.PerformLayout();
            this.tabUI.ResumeLayout(false);
            this.tabUI.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDownload;
        private System.Windows.Forms.TabPage tabUI;
        private System.Windows.Forms.Label lblUserAgent;
        private System.Windows.Forms.TextBox txtUserAgent;
        private System.Windows.Forms.CheckBox chkConvertLinks;
        private System.Windows.Forms.CheckBox chkAdjustExtensions;
        private System.Windows.Forms.Label lblMaxDepth;
        private System.Windows.Forms.NumericUpDown numMaxDepth;
        private System.Windows.Forms.Label lblMaxDepthHint;
        private System.Windows.Forms.Label lblWaitBetweenRequests;
        private System.Windows.Forms.NumericUpDown numWaitBetweenRequests;
        private System.Windows.Forms.Label lblWaitHint;
        private System.Windows.Forms.Label lblRateLimit;
        private System.Windows.Forms.TextBox txtRateLimit;
        private System.Windows.Forms.Label lblRateLimitHint;
        private System.Windows.Forms.CheckBox chkNoClobber;
        private System.Windows.Forms.CheckBox chkOpenFolderAfterDownload;
        private System.Windows.Forms.CheckBox chkShowNotifications;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnResetDefaults;
    }
}
