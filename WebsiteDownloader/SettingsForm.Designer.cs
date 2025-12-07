namespace WebsiteDownloader
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDownload = new System.Windows.Forms.TabPage();
            this.tabAdvanced = new System.Windows.Forms.TabPage();
            this.tabSchedule = new System.Windows.Forms.TabPage();
            this.tabUI = new System.Windows.Forms.TabPage();
            
            // Download tab controls
            this.lblUserAgent = new System.Windows.Forms.Label();
            this.txtUserAgent = new System.Windows.Forms.TextBox();
            this.chkConvertLinks = new System.Windows.Forms.CheckBox();
            this.chkAdjustExtensions = new System.Windows.Forms.CheckBox();
            this.lblMaxDepth = new System.Windows.Forms.Label();
            this.numMaxDepth = new System.Windows.Forms.NumericUpDown();
            this.lblWaitBetweenRequests = new System.Windows.Forms.Label();
            this.numWaitBetweenRequests = new System.Windows.Forms.NumericUpDown();
            this.lblRateLimit = new System.Windows.Forms.Label();
            this.txtRateLimit = new System.Windows.Forms.TextBox();
            this.lblRateLimitHint = new System.Windows.Forms.Label();
            this.chkNoClobber = new System.Windows.Forms.CheckBox();
            this.chkContinueDownload = new System.Windows.Forms.CheckBox();
            this.chkIgnoreSsl = new System.Windows.Forms.CheckBox();
            
            // Advanced tab controls
            this.lblConnectionTimeout = new System.Windows.Forms.Label();
            this.numConnectionTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblReadTimeout = new System.Windows.Forms.Label();
            this.numReadTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblRetryCount = new System.Windows.Forms.Label();
            this.numRetryCount = new System.Windows.Forms.NumericUpDown();
            this.grpPostDownload = new System.Windows.Forms.GroupBox();
            this.chkExportZip = new System.Windows.Forms.CheckBox();
            this.chkDeleteAfterZip = new System.Windows.Forms.CheckBox();
            this.grpMultiThread = new System.Windows.Forms.GroupBox();
            this.chkMultiThreaded = new System.Windows.Forms.CheckBox();
            this.lblThreadCount = new System.Windows.Forms.Label();
            this.numThreadCount = new System.Windows.Forms.NumericUpDown();
            this.chkCheckUpdates = new System.Windows.Forms.CheckBox();
            
            // Schedule tab controls
            this.chkEnableScheduler = new System.Windows.Forms.CheckBox();
            this.grpScheduleSettings = new System.Windows.Forms.GroupBox();
            this.lblPeakRateLimit = new System.Windows.Forms.Label();
            this.txtPeakRateLimit = new System.Windows.Forms.TextBox();
            this.lblOffPeakRateLimit = new System.Windows.Forms.Label();
            this.txtOffPeakRateLimit = new System.Windows.Forms.TextBox();
            this.lblPeakStart = new System.Windows.Forms.Label();
            this.numPeakStart = new System.Windows.Forms.NumericUpDown();
            this.lblPeakEnd = new System.Windows.Forms.Label();
            this.numPeakEnd = new System.Windows.Forms.NumericUpDown();
            this.lblScheduleHint = new System.Windows.Forms.Label();
            
            // UI tab controls
            this.chkOpenFolderAfterDownload = new System.Windows.Forms.CheckBox();
            this.chkShowNotifications = new System.Windows.Forms.CheckBox();
            
            // Form buttons
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnResetDefaults = new System.Windows.Forms.Button();
            
            this.tabControl.SuspendLayout();
            this.tabDownload.SuspendLayout();
            this.tabAdvanced.SuspendLayout();
            this.grpPostDownload.SuspendLayout();
            this.grpMultiThread.SuspendLayout();
            this.tabSchedule.SuspendLayout();
            this.grpScheduleSettings.SuspendLayout();
            this.tabUI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitBetweenRequests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numConnectionTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRetryCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreadCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPeakStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPeakEnd)).BeginInit();
            this.SuspendLayout();
            
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabDownload);
            this.tabControl.Controls.Add(this.tabAdvanced);
            this.tabControl.Controls.Add(this.tabSchedule);
            this.tabControl.Controls.Add(this.tabUI);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(460, 400);
            this.tabControl.TabIndex = 0;
            
            // 
            // tabDownload
            // 
            this.tabDownload.Controls.Add(this.lblUserAgent);
            this.tabDownload.Controls.Add(this.txtUserAgent);
            this.tabDownload.Controls.Add(this.chkConvertLinks);
            this.tabDownload.Controls.Add(this.chkAdjustExtensions);
            this.tabDownload.Controls.Add(this.lblMaxDepth);
            this.tabDownload.Controls.Add(this.numMaxDepth);
            this.tabDownload.Controls.Add(this.lblWaitBetweenRequests);
            this.tabDownload.Controls.Add(this.numWaitBetweenRequests);
            this.tabDownload.Controls.Add(this.lblRateLimit);
            this.tabDownload.Controls.Add(this.txtRateLimit);
            this.tabDownload.Controls.Add(this.lblRateLimitHint);
            this.tabDownload.Controls.Add(this.chkNoClobber);
            this.tabDownload.Controls.Add(this.chkContinueDownload);
            this.tabDownload.Controls.Add(this.chkIgnoreSsl);
            this.tabDownload.Location = new System.Drawing.Point(4, 22);
            this.tabDownload.Name = "tabDownload";
            this.tabDownload.Padding = new System.Windows.Forms.Padding(3);
            this.tabDownload.Size = new System.Drawing.Size(452, 374);
            this.tabDownload.TabIndex = 0;
            this.tabDownload.Text = "Download";
            this.tabDownload.UseVisualStyleBackColor = true;
            
            // 
            // lblUserAgent
            // 
            this.lblUserAgent.AutoSize = true;
            this.lblUserAgent.Location = new System.Drawing.Point(15, 20);
            this.lblUserAgent.Name = "lblUserAgent";
            this.lblUserAgent.Size = new System.Drawing.Size(63, 13);
            this.lblUserAgent.TabIndex = 0;
            this.lblUserAgent.Text = "User Agent:";
            
            // 
            // txtUserAgent
            // 
            this.txtUserAgent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserAgent.Location = new System.Drawing.Point(18, 38);
            this.txtUserAgent.Name = "txtUserAgent";
            this.txtUserAgent.Size = new System.Drawing.Size(416, 20);
            this.txtUserAgent.TabIndex = 1;
            
            // 
            // chkConvertLinks
            // 
            this.chkConvertLinks.AutoSize = true;
            this.chkConvertLinks.Checked = true;
            this.chkConvertLinks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConvertLinks.Location = new System.Drawing.Point(18, 70);
            this.chkConvertLinks.Name = "chkConvertLinks";
            this.chkConvertLinks.Size = new System.Drawing.Size(180, 17);
            this.chkConvertLinks.TabIndex = 2;
            this.chkConvertLinks.Text = "Convert links for offline viewing";
            this.chkConvertLinks.UseVisualStyleBackColor = true;
            
            // 
            // chkAdjustExtensions
            // 
            this.chkAdjustExtensions.AutoSize = true;
            this.chkAdjustExtensions.Checked = true;
            this.chkAdjustExtensions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAdjustExtensions.Location = new System.Drawing.Point(18, 95);
            this.chkAdjustExtensions.Name = "chkAdjustExtensions";
            this.chkAdjustExtensions.Size = new System.Drawing.Size(200, 17);
            this.chkAdjustExtensions.TabIndex = 3;
            this.chkAdjustExtensions.Text = "Adjust file extensions for content type";
            this.chkAdjustExtensions.UseVisualStyleBackColor = true;
            
            // 
            // lblMaxDepth
            // 
            this.lblMaxDepth.AutoSize = true;
            this.lblMaxDepth.Location = new System.Drawing.Point(15, 128);
            this.lblMaxDepth.Name = "lblMaxDepth";
            this.lblMaxDepth.Size = new System.Drawing.Size(100, 13);
            this.lblMaxDepth.TabIndex = 4;
            this.lblMaxDepth.Text = "Max Depth (0=unlimited):";
            
            // 
            // numMaxDepth
            // 
            this.numMaxDepth.Location = new System.Drawing.Point(160, 126);
            this.numMaxDepth.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numMaxDepth.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numMaxDepth.Name = "numMaxDepth";
            this.numMaxDepth.Size = new System.Drawing.Size(60, 20);
            this.numMaxDepth.TabIndex = 5;
            this.numMaxDepth.Value = new decimal(new int[] { 0, 0, 0, 0 });
            
            // 
            // lblWaitBetweenRequests
            // 
            this.lblWaitBetweenRequests.AutoSize = true;
            this.lblWaitBetweenRequests.Location = new System.Drawing.Point(15, 160);
            this.lblWaitBetweenRequests.Name = "lblWaitBetweenRequests";
            this.lblWaitBetweenRequests.Size = new System.Drawing.Size(130, 13);
            this.lblWaitBetweenRequests.TabIndex = 6;
            this.lblWaitBetweenRequests.Text = "Wait Between Requests (sec):";
            
            // 
            // numWaitBetweenRequests
            // 
            this.numWaitBetweenRequests.Location = new System.Drawing.Point(180, 158);
            this.numWaitBetweenRequests.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            this.numWaitBetweenRequests.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numWaitBetweenRequests.Name = "numWaitBetweenRequests";
            this.numWaitBetweenRequests.Size = new System.Drawing.Size(60, 20);
            this.numWaitBetweenRequests.TabIndex = 7;
            this.numWaitBetweenRequests.Value = new decimal(new int[] { 0, 0, 0, 0 });
            
            // 
            // lblRateLimit
            // 
            this.lblRateLimit.AutoSize = true;
            this.lblRateLimit.Location = new System.Drawing.Point(15, 195);
            this.lblRateLimit.Name = "lblRateLimit";
            this.lblRateLimit.Size = new System.Drawing.Size(57, 13);
            this.lblRateLimit.TabIndex = 8;
            this.lblRateLimit.Text = "Rate Limit:";
            
            // 
            // txtRateLimit
            // 
            this.txtRateLimit.Location = new System.Drawing.Point(100, 192);
            this.txtRateLimit.Name = "txtRateLimit";
            this.txtRateLimit.Size = new System.Drawing.Size(80, 20);
            this.txtRateLimit.TabIndex = 9;
            
            // 
            // lblRateLimitHint
            // 
            this.lblRateLimitHint.AutoSize = true;
            this.lblRateLimitHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblRateLimitHint.Location = new System.Drawing.Point(186, 195);
            this.lblRateLimitHint.Name = "lblRateLimitHint";
            this.lblRateLimitHint.Size = new System.Drawing.Size(150, 13);
            this.lblRateLimitHint.TabIndex = 10;
            this.lblRateLimitHint.Text = "(e.g., 500k, 2m - empty = unlimited)";
            
            // 
            // chkNoClobber
            // 
            this.chkNoClobber.AutoSize = true;
            this.chkNoClobber.Location = new System.Drawing.Point(18, 225);
            this.chkNoClobber.Name = "chkNoClobber";
            this.chkNoClobber.Size = new System.Drawing.Size(200, 17);
            this.chkNoClobber.TabIndex = 11;
            this.chkNoClobber.Text = "Don't overwrite existing files (no-clobber)";
            this.chkNoClobber.UseVisualStyleBackColor = true;
            
            // 
            // chkContinueDownload
            // 
            this.chkContinueDownload.AutoSize = true;
            this.chkContinueDownload.Checked = true;
            this.chkContinueDownload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkContinueDownload.Location = new System.Drawing.Point(18, 250);
            this.chkContinueDownload.Name = "chkContinueDownload";
            this.chkContinueDownload.Size = new System.Drawing.Size(215, 17);
            this.chkContinueDownload.TabIndex = 12;
            this.chkContinueDownload.Text = "Continue/Resume incomplete downloads";
            this.chkContinueDownload.UseVisualStyleBackColor = true;
            
            // 
            // chkIgnoreSsl
            // 
            this.chkIgnoreSsl.AutoSize = true;
            this.chkIgnoreSsl.Location = new System.Drawing.Point(18, 275);
            this.chkIgnoreSsl.Name = "chkIgnoreSsl";
            this.chkIgnoreSsl.Size = new System.Drawing.Size(180, 17);
            this.chkIgnoreSsl.TabIndex = 13;
            this.chkIgnoreSsl.Text = "Ignore SSL certificate errors";
            this.chkIgnoreSsl.UseVisualStyleBackColor = true;
            
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.Controls.Add(this.lblConnectionTimeout);
            this.tabAdvanced.Controls.Add(this.numConnectionTimeout);
            this.tabAdvanced.Controls.Add(this.lblReadTimeout);
            this.tabAdvanced.Controls.Add(this.numReadTimeout);
            this.tabAdvanced.Controls.Add(this.lblRetryCount);
            this.tabAdvanced.Controls.Add(this.numRetryCount);
            this.tabAdvanced.Controls.Add(this.grpPostDownload);
            this.tabAdvanced.Controls.Add(this.grpMultiThread);
            this.tabAdvanced.Controls.Add(this.chkCheckUpdates);
            this.tabAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tabAdvanced.Size = new System.Drawing.Size(452, 374);
            this.tabAdvanced.TabIndex = 1;
            this.tabAdvanced.Text = "Advanced";
            this.tabAdvanced.UseVisualStyleBackColor = true;
            
            // 
            // lblConnectionTimeout
            // 
            this.lblConnectionTimeout.AutoSize = true;
            this.lblConnectionTimeout.Location = new System.Drawing.Point(15, 20);
            this.lblConnectionTimeout.Name = "lblConnectionTimeout";
            this.lblConnectionTimeout.Size = new System.Drawing.Size(125, 13);
            this.lblConnectionTimeout.TabIndex = 0;
            this.lblConnectionTimeout.Text = "Connection Timeout (sec):";
            
            // 
            // numConnectionTimeout
            // 
            this.numConnectionTimeout.Location = new System.Drawing.Point(180, 18);
            this.numConnectionTimeout.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            this.numConnectionTimeout.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            this.numConnectionTimeout.Name = "numConnectionTimeout";
            this.numConnectionTimeout.Size = new System.Drawing.Size(80, 20);
            this.numConnectionTimeout.TabIndex = 1;
            this.numConnectionTimeout.Value = new decimal(new int[] { 30, 0, 0, 0 });
            
            // 
            // lblReadTimeout
            // 
            this.lblReadTimeout.AutoSize = true;
            this.lblReadTimeout.Location = new System.Drawing.Point(15, 50);
            this.lblReadTimeout.Name = "lblReadTimeout";
            this.lblReadTimeout.Size = new System.Drawing.Size(100, 13);
            this.lblReadTimeout.TabIndex = 2;
            this.lblReadTimeout.Text = "Read Timeout (sec):";
            
            // 
            // numReadTimeout
            // 
            this.numReadTimeout.Location = new System.Drawing.Point(180, 48);
            this.numReadTimeout.Maximum = new decimal(new int[] { 600, 0, 0, 0 });
            this.numReadTimeout.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numReadTimeout.Name = "numReadTimeout";
            this.numReadTimeout.Size = new System.Drawing.Size(80, 20);
            this.numReadTimeout.TabIndex = 3;
            this.numReadTimeout.Value = new decimal(new int[] { 60, 0, 0, 0 });
            
            // 
            // lblRetryCount
            // 
            this.lblRetryCount.AutoSize = true;
            this.lblRetryCount.Location = new System.Drawing.Point(15, 80);
            this.lblRetryCount.Name = "lblRetryCount";
            this.lblRetryCount.Size = new System.Drawing.Size(75, 13);
            this.lblRetryCount.TabIndex = 4;
            this.lblRetryCount.Text = "Retry Count:";
            
            // 
            // numRetryCount
            // 
            this.numRetryCount.Location = new System.Drawing.Point(180, 78);
            this.numRetryCount.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this.numRetryCount.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numRetryCount.Name = "numRetryCount";
            this.numRetryCount.Size = new System.Drawing.Size(80, 20);
            this.numRetryCount.TabIndex = 5;
            this.numRetryCount.Value = new decimal(new int[] { 3, 0, 0, 0 });
            
            // 
            // grpPostDownload
            // 
            this.grpPostDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPostDownload.Controls.Add(this.chkExportZip);
            this.grpPostDownload.Controls.Add(this.chkDeleteAfterZip);
            this.grpPostDownload.Location = new System.Drawing.Point(18, 115);
            this.grpPostDownload.Name = "grpPostDownload";
            this.grpPostDownload.Size = new System.Drawing.Size(416, 80);
            this.grpPostDownload.TabIndex = 6;
            this.grpPostDownload.TabStop = false;
            this.grpPostDownload.Text = "Post-Download Options";
            
            // 
            // chkExportZip
            // 
            this.chkExportZip.AutoSize = true;
            this.chkExportZip.Location = new System.Drawing.Point(15, 25);
            this.chkExportZip.Name = "chkExportZip";
            this.chkExportZip.Size = new System.Drawing.Size(200, 17);
            this.chkExportZip.TabIndex = 0;
            this.chkExportZip.Text = "Export to ZIP after download";
            this.chkExportZip.UseVisualStyleBackColor = true;
            this.chkExportZip.CheckedChanged += new System.EventHandler(this.chkExportZip_CheckedChanged);
            
            // 
            // chkDeleteAfterZip
            // 
            this.chkDeleteAfterZip.AutoSize = true;
            this.chkDeleteAfterZip.Enabled = false;
            this.chkDeleteAfterZip.Location = new System.Drawing.Point(35, 50);
            this.chkDeleteAfterZip.Name = "chkDeleteAfterZip";
            this.chkDeleteAfterZip.Size = new System.Drawing.Size(200, 17);
            this.chkDeleteAfterZip.TabIndex = 1;
            this.chkDeleteAfterZip.Text = "Delete original folder after zipping";
            this.chkDeleteAfterZip.UseVisualStyleBackColor = true;
            
            // 
            // grpMultiThread
            // 
            this.grpMultiThread.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMultiThread.Controls.Add(this.chkMultiThreaded);
            this.grpMultiThread.Controls.Add(this.lblThreadCount);
            this.grpMultiThread.Controls.Add(this.numThreadCount);
            this.grpMultiThread.Location = new System.Drawing.Point(18, 205);
            this.grpMultiThread.Name = "grpMultiThread";
            this.grpMultiThread.Size = new System.Drawing.Size(416, 85);
            this.grpMultiThread.TabIndex = 7;
            this.grpMultiThread.TabStop = false;
            this.grpMultiThread.Text = "Multi-Threading";
            
            // 
            // chkMultiThreaded
            // 
            this.chkMultiThreaded.AutoSize = true;
            this.chkMultiThreaded.Location = new System.Drawing.Point(15, 25);
            this.chkMultiThreaded.Name = "chkMultiThreaded";
            this.chkMultiThreaded.Size = new System.Drawing.Size(200, 17);
            this.chkMultiThreaded.TabIndex = 0;
            this.chkMultiThreaded.Text = "Enable multi-threaded downloads";
            this.chkMultiThreaded.UseVisualStyleBackColor = true;
            this.chkMultiThreaded.CheckedChanged += new System.EventHandler(this.chkMultiThreaded_CheckedChanged);
            
            // 
            // lblThreadCount
            // 
            this.lblThreadCount.AutoSize = true;
            this.lblThreadCount.Location = new System.Drawing.Point(35, 55);
            this.lblThreadCount.Name = "lblThreadCount";
            this.lblThreadCount.Size = new System.Drawing.Size(100, 13);
            this.lblThreadCount.TabIndex = 1;
            this.lblThreadCount.Text = "Number of Threads:";
            
            // 
            // numThreadCount
            // 
            this.numThreadCount.Enabled = false;
            this.numThreadCount.Location = new System.Drawing.Point(160, 53);
            this.numThreadCount.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            this.numThreadCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numThreadCount.Name = "numThreadCount";
            this.numThreadCount.Size = new System.Drawing.Size(60, 20);
            this.numThreadCount.TabIndex = 2;
            this.numThreadCount.Value = new decimal(new int[] { 4, 0, 0, 0 });
            
            // 
            // chkCheckUpdates
            // 
            this.chkCheckUpdates.AutoSize = true;
            this.chkCheckUpdates.Checked = true;
            this.chkCheckUpdates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCheckUpdates.Location = new System.Drawing.Point(18, 305);
            this.chkCheckUpdates.Name = "chkCheckUpdates";
            this.chkCheckUpdates.Size = new System.Drawing.Size(200, 17);
            this.chkCheckUpdates.TabIndex = 8;
            this.chkCheckUpdates.Text = "Check for updates on startup";
            this.chkCheckUpdates.UseVisualStyleBackColor = true;
            
            // 
            // tabSchedule
            // 
            this.tabSchedule.Controls.Add(this.chkEnableScheduler);
            this.tabSchedule.Controls.Add(this.grpScheduleSettings);
            this.tabSchedule.Location = new System.Drawing.Point(4, 22);
            this.tabSchedule.Name = "tabSchedule";
            this.tabSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tabSchedule.Size = new System.Drawing.Size(452, 374);
            this.tabSchedule.TabIndex = 2;
            this.tabSchedule.Text = "Schedule";
            this.tabSchedule.UseVisualStyleBackColor = true;
            
            // 
            // chkEnableScheduler
            // 
            this.chkEnableScheduler.AutoSize = true;
            this.chkEnableScheduler.Location = new System.Drawing.Point(18, 20);
            this.chkEnableScheduler.Name = "chkEnableScheduler";
            this.chkEnableScheduler.Size = new System.Drawing.Size(180, 17);
            this.chkEnableScheduler.TabIndex = 0;
            this.chkEnableScheduler.Text = "Enable bandwidth scheduling";
            this.chkEnableScheduler.UseVisualStyleBackColor = true;
            this.chkEnableScheduler.CheckedChanged += new System.EventHandler(this.chkEnableScheduler_CheckedChanged);
            
            // 
            // grpScheduleSettings
            // 
            this.grpScheduleSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpScheduleSettings.Controls.Add(this.lblPeakRateLimit);
            this.grpScheduleSettings.Controls.Add(this.txtPeakRateLimit);
            this.grpScheduleSettings.Controls.Add(this.lblOffPeakRateLimit);
            this.grpScheduleSettings.Controls.Add(this.txtOffPeakRateLimit);
            this.grpScheduleSettings.Controls.Add(this.lblPeakStart);
            this.grpScheduleSettings.Controls.Add(this.numPeakStart);
            this.grpScheduleSettings.Controls.Add(this.lblPeakEnd);
            this.grpScheduleSettings.Controls.Add(this.numPeakEnd);
            this.grpScheduleSettings.Controls.Add(this.lblScheduleHint);
            this.grpScheduleSettings.Enabled = false;
            this.grpScheduleSettings.Location = new System.Drawing.Point(18, 50);
            this.grpScheduleSettings.Name = "grpScheduleSettings";
            this.grpScheduleSettings.Size = new System.Drawing.Size(416, 180);
            this.grpScheduleSettings.TabIndex = 1;
            this.grpScheduleSettings.TabStop = false;
            this.grpScheduleSettings.Text = "Schedule Settings";
            
            // 
            // lblPeakRateLimit
            // 
            this.lblPeakRateLimit.AutoSize = true;
            this.lblPeakRateLimit.Location = new System.Drawing.Point(15, 30);
            this.lblPeakRateLimit.Name = "lblPeakRateLimit";
            this.lblPeakRateLimit.Size = new System.Drawing.Size(120, 13);
            this.lblPeakRateLimit.TabIndex = 0;
            this.lblPeakRateLimit.Text = "Peak Hours Rate Limit:";
            
            // 
            // txtPeakRateLimit
            // 
            this.txtPeakRateLimit.Location = new System.Drawing.Point(160, 27);
            this.txtPeakRateLimit.Name = "txtPeakRateLimit";
            this.txtPeakRateLimit.Size = new System.Drawing.Size(80, 20);
            this.txtPeakRateLimit.TabIndex = 1;
            this.txtPeakRateLimit.Text = "100k";
            
            // 
            // lblOffPeakRateLimit
            // 
            this.lblOffPeakRateLimit.AutoSize = true;
            this.lblOffPeakRateLimit.Location = new System.Drawing.Point(15, 60);
            this.lblOffPeakRateLimit.Name = "lblOffPeakRateLimit";
            this.lblOffPeakRateLimit.Size = new System.Drawing.Size(130, 13);
            this.lblOffPeakRateLimit.TabIndex = 2;
            this.lblOffPeakRateLimit.Text = "Off-Peak Hours Rate Limit:";
            
            // 
            // txtOffPeakRateLimit
            // 
            this.txtOffPeakRateLimit.Location = new System.Drawing.Point(160, 57);
            this.txtOffPeakRateLimit.Name = "txtOffPeakRateLimit";
            this.txtOffPeakRateLimit.Size = new System.Drawing.Size(80, 20);
            this.txtOffPeakRateLimit.TabIndex = 3;
            
            // 
            // lblPeakStart
            // 
            this.lblPeakStart.AutoSize = true;
            this.lblPeakStart.Location = new System.Drawing.Point(15, 95);
            this.lblPeakStart.Name = "lblPeakStart";
            this.lblPeakStart.Size = new System.Drawing.Size(120, 13);
            this.lblPeakStart.TabIndex = 4;
            this.lblPeakStart.Text = "Peak Hours Start (24h):";
            
            // 
            // numPeakStart
            // 
            this.numPeakStart.Location = new System.Drawing.Point(160, 93);
            this.numPeakStart.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            this.numPeakStart.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numPeakStart.Name = "numPeakStart";
            this.numPeakStart.Size = new System.Drawing.Size(60, 20);
            this.numPeakStart.TabIndex = 5;
            this.numPeakStart.Value = new decimal(new int[] { 9, 0, 0, 0 });
            
            // 
            // lblPeakEnd
            // 
            this.lblPeakEnd.AutoSize = true;
            this.lblPeakEnd.Location = new System.Drawing.Point(15, 125);
            this.lblPeakEnd.Name = "lblPeakEnd";
            this.lblPeakEnd.Size = new System.Drawing.Size(115, 13);
            this.lblPeakEnd.TabIndex = 6;
            this.lblPeakEnd.Text = "Peak Hours End (24h):";
            
            // 
            // numPeakEnd
            // 
            this.numPeakEnd.Location = new System.Drawing.Point(160, 123);
            this.numPeakEnd.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            this.numPeakEnd.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numPeakEnd.Name = "numPeakEnd";
            this.numPeakEnd.Size = new System.Drawing.Size(60, 20);
            this.numPeakEnd.TabIndex = 7;
            this.numPeakEnd.Value = new decimal(new int[] { 17, 0, 0, 0 });
            
            // 
            // lblScheduleHint
            // 
            this.lblScheduleHint.AutoSize = true;
            this.lblScheduleHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblScheduleHint.Location = new System.Drawing.Point(15, 155);
            this.lblScheduleHint.Name = "lblScheduleHint";
            this.lblScheduleHint.Size = new System.Drawing.Size(350, 13);
            this.lblScheduleHint.TabIndex = 8;
            this.lblScheduleHint.Text = "Limit bandwidth during peak hours, use unlimited/faster speed off-peak.";
            
            // 
            // tabUI
            // 
            this.tabUI.Controls.Add(this.chkOpenFolderAfterDownload);
            this.tabUI.Controls.Add(this.chkShowNotifications);
            this.tabUI.Location = new System.Drawing.Point(4, 22);
            this.tabUI.Name = "tabUI";
            this.tabUI.Padding = new System.Windows.Forms.Padding(3);
            this.tabUI.Size = new System.Drawing.Size(452, 374);
            this.tabUI.TabIndex = 3;
            this.tabUI.Text = "Interface";
            this.tabUI.UseVisualStyleBackColor = true;
            
            // 
            // chkOpenFolderAfterDownload
            // 
            this.chkOpenFolderAfterDownload.AutoSize = true;
            this.chkOpenFolderAfterDownload.Checked = true;
            this.chkOpenFolderAfterDownload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenFolderAfterDownload.Location = new System.Drawing.Point(18, 20);
            this.chkOpenFolderAfterDownload.Name = "chkOpenFolderAfterDownload";
            this.chkOpenFolderAfterDownload.Size = new System.Drawing.Size(200, 17);
            this.chkOpenFolderAfterDownload.TabIndex = 0;
            this.chkOpenFolderAfterDownload.Text = "Open folder after download completes";
            this.chkOpenFolderAfterDownload.UseVisualStyleBackColor = true;
            
            // 
            // chkShowNotifications
            // 
            this.chkShowNotifications.AutoSize = true;
            this.chkShowNotifications.Checked = true;
            this.chkShowNotifications.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowNotifications.Location = new System.Drawing.Point(18, 45);
            this.chkShowNotifications.Name = "chkShowNotifications";
            this.chkShowNotifications.Size = new System.Drawing.Size(200, 17);
            this.chkShowNotifications.TabIndex = 1;
            this.chkShowNotifications.Text = "Show desktop notifications";
            this.chkShowNotifications.UseVisualStyleBackColor = true;
            
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(316, 428);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 28);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(397, 428);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            // 
            // btnResetDefaults
            // 
            this.btnResetDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetDefaults.Location = new System.Drawing.Point(12, 428);
            this.btnResetDefaults.Name = "btnResetDefaults";
            this.btnResetDefaults.Size = new System.Drawing.Size(100, 28);
            this.btnResetDefaults.TabIndex = 3;
            this.btnResetDefaults.Text = "Reset Defaults";
            this.btnResetDefaults.UseVisualStyleBackColor = true;
            this.btnResetDefaults.Click += new System.EventHandler(this.btnResetDefaults_Click);
            
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 471);
            this.Controls.Add(this.btnResetDefaults);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.tabControl.ResumeLayout(false);
            this.tabDownload.ResumeLayout(false);
            this.tabDownload.PerformLayout();
            this.tabAdvanced.ResumeLayout(false);
            this.tabAdvanced.PerformLayout();
            this.grpPostDownload.ResumeLayout(false);
            this.grpPostDownload.PerformLayout();
            this.grpMultiThread.ResumeLayout(false);
            this.grpMultiThread.PerformLayout();
            this.tabSchedule.ResumeLayout(false);
            this.tabSchedule.PerformLayout();
            this.grpScheduleSettings.ResumeLayout(false);
            this.grpScheduleSettings.PerformLayout();
            this.tabUI.ResumeLayout(false);
            this.tabUI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitBetweenRequests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numConnectionTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRetryCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreadCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPeakStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPeakEnd)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDownload;
        private System.Windows.Forms.TabPage tabAdvanced;
        private System.Windows.Forms.TabPage tabSchedule;
        private System.Windows.Forms.TabPage tabUI;
        
        // Download tab controls
        private System.Windows.Forms.Label lblUserAgent;
        private System.Windows.Forms.TextBox txtUserAgent;
        private System.Windows.Forms.CheckBox chkConvertLinks;
        private System.Windows.Forms.CheckBox chkAdjustExtensions;
        private System.Windows.Forms.Label lblMaxDepth;
        private System.Windows.Forms.NumericUpDown numMaxDepth;
        private System.Windows.Forms.Label lblWaitBetweenRequests;
        private System.Windows.Forms.NumericUpDown numWaitBetweenRequests;
        private System.Windows.Forms.Label lblRateLimit;
        private System.Windows.Forms.TextBox txtRateLimit;
        private System.Windows.Forms.Label lblRateLimitHint;
        private System.Windows.Forms.CheckBox chkNoClobber;
        private System.Windows.Forms.CheckBox chkContinueDownload;
        private System.Windows.Forms.CheckBox chkIgnoreSsl;
        
        // Advanced tab controls
        private System.Windows.Forms.Label lblConnectionTimeout;
        private System.Windows.Forms.NumericUpDown numConnectionTimeout;
        private System.Windows.Forms.Label lblReadTimeout;
        private System.Windows.Forms.NumericUpDown numReadTimeout;
        private System.Windows.Forms.Label lblRetryCount;
        private System.Windows.Forms.NumericUpDown numRetryCount;
        private System.Windows.Forms.GroupBox grpPostDownload;
        private System.Windows.Forms.CheckBox chkExportZip;
        private System.Windows.Forms.CheckBox chkDeleteAfterZip;
        private System.Windows.Forms.GroupBox grpMultiThread;
        private System.Windows.Forms.CheckBox chkMultiThreaded;
        private System.Windows.Forms.Label lblThreadCount;
        private System.Windows.Forms.NumericUpDown numThreadCount;
        private System.Windows.Forms.CheckBox chkCheckUpdates;
        
        // Schedule tab controls
        private System.Windows.Forms.CheckBox chkEnableScheduler;
        private System.Windows.Forms.GroupBox grpScheduleSettings;
        private System.Windows.Forms.Label lblPeakRateLimit;
        private System.Windows.Forms.TextBox txtPeakRateLimit;
        private System.Windows.Forms.Label lblOffPeakRateLimit;
        private System.Windows.Forms.TextBox txtOffPeakRateLimit;
        private System.Windows.Forms.Label lblPeakStart;
        private System.Windows.Forms.NumericUpDown numPeakStart;
        private System.Windows.Forms.Label lblPeakEnd;
        private System.Windows.Forms.NumericUpDown numPeakEnd;
        private System.Windows.Forms.Label lblScheduleHint;
        
        // UI tab controls
        private System.Windows.Forms.CheckBox chkOpenFolderAfterDownload;
        private System.Windows.Forms.CheckBox chkShowNotifications;
        
        // Form buttons
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnResetDefaults;
    }
}
