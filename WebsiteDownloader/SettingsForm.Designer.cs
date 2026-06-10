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
            this.tabFilters = new System.Windows.Forms.TabPage();
            this.tabAuth = new System.Windows.Forms.TabPage();
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
            this.lblResumeMode = new System.Windows.Forms.Label();
            this.cboResumeMode = new System.Windows.Forms.ComboBox();
            this.lblResumeModeHint = new System.Windows.Forms.Label();
            this.chkIgnoreSsl = new System.Windows.Forms.CheckBox();
            this.chkRandomWait = new System.Windows.Forms.CheckBox();
            this.chkContentDisposition = new System.Windows.Forms.CheckBox();
            this.lblDirectoryStructure = new System.Windows.Forms.Label();
            this.cboDirectoryStructure = new System.Windows.Forms.ComboBox();

            // Filters tab controls
            this.chkNoParent = new System.Windows.Forms.CheckBox();
            this.chkSpanHosts = new System.Windows.Forms.CheckBox();
            this.lblDomains = new System.Windows.Forms.Label();
            this.txtDomains = new System.Windows.Forms.TextBox();
            this.lblDomainsHint = new System.Windows.Forms.Label();
            this.lblAcceptTypes = new System.Windows.Forms.Label();
            this.txtAcceptTypes = new System.Windows.Forms.TextBox();
            this.lblAcceptTypesHint = new System.Windows.Forms.Label();
            this.lblRejectTypes = new System.Windows.Forms.Label();
            this.txtRejectTypes = new System.Windows.Forms.TextBox();
            this.lblRejectTypesHint = new System.Windows.Forms.Label();
            this.lblIncludeDirs = new System.Windows.Forms.Label();
            this.txtIncludeDirs = new System.Windows.Forms.TextBox();
            this.lblIncludeDirsHint = new System.Windows.Forms.Label();
            this.lblExcludeDirs = new System.Windows.Forms.Label();
            this.txtExcludeDirs = new System.Windows.Forms.TextBox();
            this.lblExcludeDirsHint = new System.Windows.Forms.Label();
            this.chkIgnoreFilterCase = new System.Windows.Forms.CheckBox();
            this.lblQuota = new System.Windows.Forms.Label();
            this.txtQuota = new System.Windows.Forms.TextBox();
            this.lblQuotaHint = new System.Windows.Forms.Label();
            this.lblMaxRedirect = new System.Windows.Forms.Label();
            this.numMaxRedirect = new System.Windows.Forms.NumericUpDown();

            // Authentication tab controls
            this.lblHttpUser = new System.Windows.Forms.Label();
            this.txtHttpUser = new System.Windows.Forms.TextBox();
            this.lblHttpPassword = new System.Windows.Forms.Label();
            this.txtHttpPassword = new System.Windows.Forms.TextBox();
            this.lblPasswordWarning = new System.Windows.Forms.Label();
            this.lblCookiesFile = new System.Windows.Forms.Label();
            this.txtCookiesFile = new System.Windows.Forms.TextBox();
            this.btnBrowseCookies = new System.Windows.Forms.Button();
            this.lblCookiesHint = new System.Windows.Forms.Label();
            this.chkKeepSessionCookies = new System.Windows.Forms.CheckBox();
            this.lblReferer = new System.Windows.Forms.Label();
            this.txtReferer = new System.Windows.Forms.TextBox();
            this.lblCustomHeaders = new System.Windows.Forms.Label();
            this.txtCustomHeaders = new System.Windows.Forms.TextBox();

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
            this.grpEngine = new System.Windows.Forms.GroupBox();
            this.cboEngine = new System.Windows.Forms.ComboBox();
            this.lblEngine = new System.Windows.Forms.Label();
            this.lblEngineStatus = new System.Windows.Forms.Label();
            this.btnSetupPlaywright = new System.Windows.Forms.Button();
            this.chkStripAnalytics = new System.Windows.Forms.CheckBox();
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
            this.tabFilters.SuspendLayout();
            this.tabAuth.SuspendLayout();
            this.tabAdvanced.SuspendLayout();
            this.grpPostDownload.SuspendLayout();
            this.grpMultiThread.SuspendLayout();
            this.grpEngine.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.numMaxRedirect)).BeginInit();
            this.SuspendLayout();
            
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabDownload);
            this.tabControl.Controls.Add(this.tabFilters);
            this.tabControl.Controls.Add(this.tabAuth);
            this.tabControl.Controls.Add(this.tabAdvanced);
            this.tabControl.Controls.Add(this.tabSchedule);
            this.tabControl.Controls.Add(this.tabUI);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(460, 470);
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
            this.tabDownload.Controls.Add(this.lblResumeMode);
            this.tabDownload.Controls.Add(this.cboResumeMode);
            this.tabDownload.Controls.Add(this.lblResumeModeHint);
            this.tabDownload.Controls.Add(this.chkIgnoreSsl);
            this.tabDownload.Controls.Add(this.chkRandomWait);
            this.tabDownload.Controls.Add(this.chkContentDisposition);
            this.tabDownload.Controls.Add(this.lblDirectoryStructure);
            this.tabDownload.Controls.Add(this.cboDirectoryStructure);
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
            // lblResumeMode
            //
            this.lblResumeMode.AutoSize = true;
            this.lblResumeMode.Location = new System.Drawing.Point(15, 228);
            this.lblResumeMode.Name = "lblResumeMode";
            this.lblResumeMode.Size = new System.Drawing.Size(120, 13);
            this.lblResumeMode.TabIndex = 11;
            this.lblResumeMode.Text = "On restart (resume/skip):";
            //
            // cboResumeMode
            //
            this.cboResumeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboResumeMode.FormattingEnabled = true;
            this.cboResumeMode.Location = new System.Drawing.Point(150, 225);
            this.cboResumeMode.Name = "cboResumeMode";
            this.cboResumeMode.Size = new System.Drawing.Size(260, 21);
            this.cboResumeMode.TabIndex = 12;
            //
            // lblResumeModeHint
            //
            this.lblResumeModeHint.AutoSize = true;
            this.lblResumeModeHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblResumeModeHint.Location = new System.Drawing.Point(147, 249);
            this.lblResumeModeHint.MaximumSize = new System.Drawing.Size(265, 0);
            this.lblResumeModeHint.Name = "lblResumeModeHint";
            this.lblResumeModeHint.Size = new System.Drawing.Size(150, 13);
            this.lblResumeModeHint.TabIndex = 0;
            this.lblResumeModeHint.Text = "";

            // 
            // chkIgnoreSsl
            // 
            this.chkIgnoreSsl.AutoSize = true;
            this.chkIgnoreSsl.Location = new System.Drawing.Point(18, 290);
            this.chkIgnoreSsl.Name = "chkIgnoreSsl";
            this.chkIgnoreSsl.Size = new System.Drawing.Size(180, 17);
            this.chkIgnoreSsl.TabIndex = 13;
            this.chkIgnoreSsl.Text = "Ignore SSL certificate errors";
            this.chkIgnoreSsl.UseVisualStyleBackColor = true;

            //
            // chkRandomWait
            //
            this.chkRandomWait.AutoSize = true;
            this.chkRandomWait.Location = new System.Drawing.Point(250, 159);
            this.chkRandomWait.Name = "chkRandomWait";
            this.chkRandomWait.Size = new System.Drawing.Size(105, 17);
            this.chkRandomWait.TabIndex = 14;
            this.chkRandomWait.Text = "Randomize wait";
            this.chkRandomWait.UseVisualStyleBackColor = true;

            //
            // chkContentDisposition
            //
            this.chkContentDisposition.AutoSize = true;
            this.chkContentDisposition.Location = new System.Drawing.Point(18, 313);
            this.chkContentDisposition.Name = "chkContentDisposition";
            this.chkContentDisposition.Size = new System.Drawing.Size(300, 17);
            this.chkContentDisposition.TabIndex = 15;
            this.chkContentDisposition.Text = "Use server-suggested file names (Content-Disposition)";
            this.chkContentDisposition.UseVisualStyleBackColor = true;

            //
            // lblDirectoryStructure
            //
            this.lblDirectoryStructure.AutoSize = true;
            this.lblDirectoryStructure.Location = new System.Drawing.Point(15, 343);
            this.lblDirectoryStructure.Name = "lblDirectoryStructure";
            this.lblDirectoryStructure.Size = new System.Drawing.Size(85, 13);
            this.lblDirectoryStructure.TabIndex = 16;
            this.lblDirectoryStructure.Text = "Folder structure:";

            //
            // cboDirectoryStructure
            //
            this.cboDirectoryStructure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDirectoryStructure.FormattingEnabled = true;
            this.cboDirectoryStructure.Location = new System.Drawing.Point(150, 340);
            this.cboDirectoryStructure.Name = "cboDirectoryStructure";
            this.cboDirectoryStructure.Size = new System.Drawing.Size(260, 21);
            this.cboDirectoryStructure.TabIndex = 17;

            //
            // tabFilters
            //
            this.tabFilters.Controls.Add(this.chkNoParent);
            this.tabFilters.Controls.Add(this.chkSpanHosts);
            this.tabFilters.Controls.Add(this.lblDomains);
            this.tabFilters.Controls.Add(this.txtDomains);
            this.tabFilters.Controls.Add(this.lblDomainsHint);
            this.tabFilters.Controls.Add(this.lblAcceptTypes);
            this.tabFilters.Controls.Add(this.txtAcceptTypes);
            this.tabFilters.Controls.Add(this.lblAcceptTypesHint);
            this.tabFilters.Controls.Add(this.lblRejectTypes);
            this.tabFilters.Controls.Add(this.txtRejectTypes);
            this.tabFilters.Controls.Add(this.lblRejectTypesHint);
            this.tabFilters.Controls.Add(this.lblIncludeDirs);
            this.tabFilters.Controls.Add(this.txtIncludeDirs);
            this.tabFilters.Controls.Add(this.lblIncludeDirsHint);
            this.tabFilters.Controls.Add(this.lblExcludeDirs);
            this.tabFilters.Controls.Add(this.txtExcludeDirs);
            this.tabFilters.Controls.Add(this.lblExcludeDirsHint);
            this.tabFilters.Controls.Add(this.chkIgnoreFilterCase);
            this.tabFilters.Controls.Add(this.lblQuota);
            this.tabFilters.Controls.Add(this.txtQuota);
            this.tabFilters.Controls.Add(this.lblQuotaHint);
            this.tabFilters.Controls.Add(this.lblMaxRedirect);
            this.tabFilters.Controls.Add(this.numMaxRedirect);
            this.tabFilters.Location = new System.Drawing.Point(4, 22);
            this.tabFilters.Name = "tabFilters";
            this.tabFilters.Padding = new System.Windows.Forms.Padding(3);
            this.tabFilters.Size = new System.Drawing.Size(452, 374);
            this.tabFilters.TabIndex = 4;
            this.tabFilters.Text = "Filters";
            this.tabFilters.UseVisualStyleBackColor = true;

            //
            // chkNoParent
            //
            this.chkNoParent.AutoSize = true;
            this.chkNoParent.Location = new System.Drawing.Point(18, 15);
            this.chkNoParent.Name = "chkNoParent";
            this.chkNoParent.Size = new System.Drawing.Size(320, 17);
            this.chkNoParent.TabIndex = 0;
            this.chkNoParent.Text = "Stay within the start URL's folder (don't ascend to parent)";
            this.chkNoParent.UseVisualStyleBackColor = true;

            //
            // chkSpanHosts
            //
            this.chkSpanHosts.AutoSize = true;
            this.chkSpanHosts.Location = new System.Drawing.Point(18, 38);
            this.chkSpanHosts.Name = "chkSpanHosts";
            this.chkSpanHosts.Size = new System.Drawing.Size(340, 17);
            this.chkSpanHosts.TabIndex = 1;
            this.chkSpanHosts.Text = "Allow following links to other hosts (use with allowed domains)";
            this.chkSpanHosts.UseVisualStyleBackColor = true;

            //
            // lblDomains
            //
            this.lblDomains.AutoSize = true;
            this.lblDomains.Location = new System.Drawing.Point(15, 68);
            this.lblDomains.Name = "lblDomains";
            this.lblDomains.Size = new System.Drawing.Size(95, 13);
            this.lblDomains.TabIndex = 2;
            this.lblDomains.Text = "Allowed domains:";

            //
            // txtDomains
            //
            this.txtDomains.Location = new System.Drawing.Point(150, 65);
            this.txtDomains.Name = "txtDomains";
            this.txtDomains.Size = new System.Drawing.Size(284, 20);
            this.txtDomains.TabIndex = 3;

            //
            // lblDomainsHint
            //
            this.lblDomainsHint.AutoSize = true;
            this.lblDomainsHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblDomainsHint.Location = new System.Drawing.Point(147, 88);
            this.lblDomainsHint.Name = "lblDomainsHint";
            this.lblDomainsHint.Size = new System.Drawing.Size(280, 13);
            this.lblDomainsHint.TabIndex = 4;
            this.lblDomainsHint.Text = "(comma-separated, e.g. example.com - empty = start host)";

            //
            // lblAcceptTypes
            //
            this.lblAcceptTypes.AutoSize = true;
            this.lblAcceptTypes.Location = new System.Drawing.Point(15, 113);
            this.lblAcceptTypes.Name = "lblAcceptTypes";
            this.lblAcceptTypes.Size = new System.Drawing.Size(110, 13);
            this.lblAcceptTypes.TabIndex = 5;
            this.lblAcceptTypes.Text = "Only download types:";

            //
            // txtAcceptTypes
            //
            this.txtAcceptTypes.Location = new System.Drawing.Point(150, 110);
            this.txtAcceptTypes.Name = "txtAcceptTypes";
            this.txtAcceptTypes.Size = new System.Drawing.Size(284, 20);
            this.txtAcceptTypes.TabIndex = 6;

            //
            // lblAcceptTypesHint
            //
            this.lblAcceptTypesHint.AutoSize = true;
            this.lblAcceptTypesHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAcceptTypesHint.Location = new System.Drawing.Point(147, 133);
            this.lblAcceptTypesHint.Name = "lblAcceptTypesHint";
            this.lblAcceptTypesHint.Size = new System.Drawing.Size(270, 13);
            this.lblAcceptTypesHint.TabIndex = 7;
            this.lblAcceptTypesHint.Text = "(comma-separated, e.g. pdf,jpg,png - empty = everything)";

            //
            // lblRejectTypes
            //
            this.lblRejectTypes.AutoSize = true;
            this.lblRejectTypes.Location = new System.Drawing.Point(15, 158);
            this.lblRejectTypes.Name = "lblRejectTypes";
            this.lblRejectTypes.Size = new System.Drawing.Size(80, 13);
            this.lblRejectTypes.TabIndex = 8;
            this.lblRejectTypes.Text = "Skip file types:";

            //
            // txtRejectTypes
            //
            this.txtRejectTypes.Location = new System.Drawing.Point(150, 155);
            this.txtRejectTypes.Name = "txtRejectTypes";
            this.txtRejectTypes.Size = new System.Drawing.Size(284, 20);
            this.txtRejectTypes.TabIndex = 9;

            //
            // lblRejectTypesHint
            //
            this.lblRejectTypesHint.AutoSize = true;
            this.lblRejectTypesHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblRejectTypesHint.Location = new System.Drawing.Point(147, 178);
            this.lblRejectTypesHint.Name = "lblRejectTypesHint";
            this.lblRejectTypesHint.Size = new System.Drawing.Size(200, 13);
            this.lblRejectTypesHint.TabIndex = 10;
            this.lblRejectTypesHint.Text = "(comma-separated, e.g. zip,exe,iso)";

            //
            // lblIncludeDirs
            //
            this.lblIncludeDirs.AutoSize = true;
            this.lblIncludeDirs.Location = new System.Drawing.Point(15, 203);
            this.lblIncludeDirs.Name = "lblIncludeDirs";
            this.lblIncludeDirs.Size = new System.Drawing.Size(95, 13);
            this.lblIncludeDirs.TabIndex = 11;
            this.lblIncludeDirs.Text = "Only these folders:";

            //
            // txtIncludeDirs
            //
            this.txtIncludeDirs.Location = new System.Drawing.Point(150, 200);
            this.txtIncludeDirs.Name = "txtIncludeDirs";
            this.txtIncludeDirs.Size = new System.Drawing.Size(284, 20);
            this.txtIncludeDirs.TabIndex = 12;

            //
            // lblIncludeDirsHint
            //
            this.lblIncludeDirsHint.AutoSize = true;
            this.lblIncludeDirsHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblIncludeDirsHint.Location = new System.Drawing.Point(147, 223);
            this.lblIncludeDirsHint.Name = "lblIncludeDirsHint";
            this.lblIncludeDirsHint.Size = new System.Drawing.Size(220, 13);
            this.lblIncludeDirsHint.TabIndex = 13;
            this.lblIncludeDirsHint.Text = "(comma-separated paths, e.g. /blog,/docs)";

            //
            // lblExcludeDirs
            //
            this.lblExcludeDirs.AutoSize = true;
            this.lblExcludeDirs.Location = new System.Drawing.Point(15, 248);
            this.lblExcludeDirs.Name = "lblExcludeDirs";
            this.lblExcludeDirs.Size = new System.Drawing.Size(95, 13);
            this.lblExcludeDirs.TabIndex = 14;
            this.lblExcludeDirs.Text = "Skip these folders:";

            //
            // txtExcludeDirs
            //
            this.txtExcludeDirs.Location = new System.Drawing.Point(150, 245);
            this.txtExcludeDirs.Name = "txtExcludeDirs";
            this.txtExcludeDirs.Size = new System.Drawing.Size(284, 20);
            this.txtExcludeDirs.TabIndex = 15;

            //
            // lblExcludeDirsHint
            //
            this.lblExcludeDirsHint.AutoSize = true;
            this.lblExcludeDirsHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblExcludeDirsHint.Location = new System.Drawing.Point(147, 268);
            this.lblExcludeDirsHint.Name = "lblExcludeDirsHint";
            this.lblExcludeDirsHint.Size = new System.Drawing.Size(220, 13);
            this.lblExcludeDirsHint.TabIndex = 16;
            this.lblExcludeDirsHint.Text = "(comma-separated paths, e.g. /forum,/ads)";

            //
            // chkIgnoreFilterCase
            //
            this.chkIgnoreFilterCase.AutoSize = true;
            this.chkIgnoreFilterCase.Location = new System.Drawing.Point(18, 293);
            this.chkIgnoreFilterCase.Name = "chkIgnoreFilterCase";
            this.chkIgnoreFilterCase.Size = new System.Drawing.Size(190, 17);
            this.chkIgnoreFilterCase.TabIndex = 17;
            this.chkIgnoreFilterCase.Text = "Case-insensitive filter matching";
            this.chkIgnoreFilterCase.UseVisualStyleBackColor = true;

            //
            // lblQuota
            //
            this.lblQuota.AutoSize = true;
            this.lblQuota.Location = new System.Drawing.Point(15, 323);
            this.lblQuota.Name = "lblQuota";
            this.lblQuota.Size = new System.Drawing.Size(80, 13);
            this.lblQuota.TabIndex = 18;
            this.lblQuota.Text = "Total size limit:";

            //
            // txtQuota
            //
            this.txtQuota.Location = new System.Drawing.Point(150, 320);
            this.txtQuota.Name = "txtQuota";
            this.txtQuota.Size = new System.Drawing.Size(80, 20);
            this.txtQuota.TabIndex = 19;

            //
            // lblQuotaHint
            //
            this.lblQuotaHint.AutoSize = true;
            this.lblQuotaHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblQuotaHint.Location = new System.Drawing.Point(236, 323);
            this.lblQuotaHint.MaximumSize = new System.Drawing.Size(210, 0);
            this.lblQuotaHint.Name = "lblQuotaHint";
            this.lblQuotaHint.Size = new System.Drawing.Size(200, 13);
            this.lblQuotaHint.TabIndex = 20;
            this.lblQuotaHint.Text = "(e.g. 500m, 2g - empty = unlimited)";

            //
            // lblMaxRedirect
            //
            this.lblMaxRedirect.AutoSize = true;
            this.lblMaxRedirect.Location = new System.Drawing.Point(15, 351);
            this.lblMaxRedirect.Name = "lblMaxRedirect";
            this.lblMaxRedirect.Size = new System.Drawing.Size(75, 13);
            this.lblMaxRedirect.TabIndex = 21;
            this.lblMaxRedirect.Text = "Max redirects:";

            //
            // numMaxRedirect
            //
            this.numMaxRedirect.Location = new System.Drawing.Point(150, 349);
            this.numMaxRedirect.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numMaxRedirect.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numMaxRedirect.Name = "numMaxRedirect";
            this.numMaxRedirect.Size = new System.Drawing.Size(60, 20);
            this.numMaxRedirect.TabIndex = 22;
            this.numMaxRedirect.Value = new decimal(new int[] { 20, 0, 0, 0 });

            //
            // tabAuth
            //
            this.tabAuth.Controls.Add(this.lblHttpUser);
            this.tabAuth.Controls.Add(this.txtHttpUser);
            this.tabAuth.Controls.Add(this.lblHttpPassword);
            this.tabAuth.Controls.Add(this.txtHttpPassword);
            this.tabAuth.Controls.Add(this.lblPasswordWarning);
            this.tabAuth.Controls.Add(this.lblCookiesFile);
            this.tabAuth.Controls.Add(this.txtCookiesFile);
            this.tabAuth.Controls.Add(this.btnBrowseCookies);
            this.tabAuth.Controls.Add(this.lblCookiesHint);
            this.tabAuth.Controls.Add(this.chkKeepSessionCookies);
            this.tabAuth.Controls.Add(this.lblReferer);
            this.tabAuth.Controls.Add(this.txtReferer);
            this.tabAuth.Controls.Add(this.lblCustomHeaders);
            this.tabAuth.Controls.Add(this.txtCustomHeaders);
            this.tabAuth.Location = new System.Drawing.Point(4, 22);
            this.tabAuth.Name = "tabAuth";
            this.tabAuth.Padding = new System.Windows.Forms.Padding(3);
            this.tabAuth.Size = new System.Drawing.Size(452, 374);
            this.tabAuth.TabIndex = 5;
            this.tabAuth.Text = "Authentication";
            this.tabAuth.UseVisualStyleBackColor = true;

            //
            // lblHttpUser
            //
            this.lblHttpUser.AutoSize = true;
            this.lblHttpUser.Location = new System.Drawing.Point(15, 20);
            this.lblHttpUser.Name = "lblHttpUser";
            this.lblHttpUser.Size = new System.Drawing.Size(58, 13);
            this.lblHttpUser.TabIndex = 0;
            this.lblHttpUser.Text = "Username:";

            //
            // txtHttpUser
            //
            this.txtHttpUser.Location = new System.Drawing.Point(130, 17);
            this.txtHttpUser.Name = "txtHttpUser";
            this.txtHttpUser.Size = new System.Drawing.Size(200, 20);
            this.txtHttpUser.TabIndex = 1;

            //
            // lblHttpPassword
            //
            this.lblHttpPassword.AutoSize = true;
            this.lblHttpPassword.Location = new System.Drawing.Point(15, 50);
            this.lblHttpPassword.Name = "lblHttpPassword";
            this.lblHttpPassword.Size = new System.Drawing.Size(56, 13);
            this.lblHttpPassword.TabIndex = 2;
            this.lblHttpPassword.Text = "Password:";

            //
            // txtHttpPassword
            //
            this.txtHttpPassword.Location = new System.Drawing.Point(130, 47);
            this.txtHttpPassword.Name = "txtHttpPassword";
            this.txtHttpPassword.Size = new System.Drawing.Size(200, 20);
            this.txtHttpPassword.TabIndex = 3;
            this.txtHttpPassword.UseSystemPasswordChar = true;

            //
            // lblPasswordWarning
            //
            this.lblPasswordWarning.AutoSize = true;
            this.lblPasswordWarning.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblPasswordWarning.Location = new System.Drawing.Point(127, 72);
            this.lblPasswordWarning.Name = "lblPasswordWarning";
            this.lblPasswordWarning.Size = new System.Drawing.Size(280, 13);
            this.lblPasswordWarning.TabIndex = 4;
            this.lblPasswordWarning.Text = "Credentials are stored in plain text in settings.json.";

            //
            // lblCookiesFile
            //
            this.lblCookiesFile.AutoSize = true;
            this.lblCookiesFile.Location = new System.Drawing.Point(15, 105);
            this.lblCookiesFile.Name = "lblCookiesFile";
            this.lblCookiesFile.Size = new System.Drawing.Size(65, 13);
            this.lblCookiesFile.TabIndex = 5;
            this.lblCookiesFile.Text = "Cookies file:";

            //
            // txtCookiesFile
            //
            this.txtCookiesFile.Location = new System.Drawing.Point(130, 102);
            this.txtCookiesFile.Name = "txtCookiesFile";
            this.txtCookiesFile.Size = new System.Drawing.Size(230, 20);
            this.txtCookiesFile.TabIndex = 6;

            //
            // btnBrowseCookies
            //
            this.btnBrowseCookies.Location = new System.Drawing.Point(366, 100);
            this.btnBrowseCookies.Name = "btnBrowseCookies";
            this.btnBrowseCookies.Size = new System.Drawing.Size(70, 23);
            this.btnBrowseCookies.TabIndex = 7;
            this.btnBrowseCookies.Text = "Browse...";
            this.btnBrowseCookies.UseVisualStyleBackColor = true;
            this.btnBrowseCookies.Click += new System.EventHandler(this.btnBrowseCookies_Click);

            //
            // lblCookiesHint
            //
            this.lblCookiesHint.AutoSize = true;
            this.lblCookiesHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblCookiesHint.Location = new System.Drawing.Point(127, 127);
            this.lblCookiesHint.MaximumSize = new System.Drawing.Size(310, 0);
            this.lblCookiesHint.Name = "lblCookiesHint";
            this.lblCookiesHint.Size = new System.Drawing.Size(300, 13);
            this.lblCookiesHint.TabIndex = 8;
            this.lblCookiesHint.Text = "Export cookies from your browser (Netscape format) to download login-protected sites.";

            //
            // chkKeepSessionCookies
            //
            this.chkKeepSessionCookies.AutoSize = true;
            this.chkKeepSessionCookies.Location = new System.Drawing.Point(130, 160);
            this.chkKeepSessionCookies.Name = "chkKeepSessionCookies";
            this.chkKeepSessionCookies.Size = new System.Drawing.Size(150, 17);
            this.chkKeepSessionCookies.TabIndex = 9;
            this.chkKeepSessionCookies.Text = "Include session cookies";
            this.chkKeepSessionCookies.UseVisualStyleBackColor = true;

            //
            // lblReferer
            //
            this.lblReferer.AutoSize = true;
            this.lblReferer.Location = new System.Drawing.Point(15, 192);
            this.lblReferer.Name = "lblReferer";
            this.lblReferer.Size = new System.Drawing.Size(70, 13);
            this.lblReferer.TabIndex = 10;
            this.lblReferer.Text = "Referer URL:";

            //
            // txtReferer
            //
            this.txtReferer.Location = new System.Drawing.Point(130, 189);
            this.txtReferer.Name = "txtReferer";
            this.txtReferer.Size = new System.Drawing.Size(306, 20);
            this.txtReferer.TabIndex = 11;

            //
            // lblCustomHeaders
            //
            this.lblCustomHeaders.AutoSize = true;
            this.lblCustomHeaders.Location = new System.Drawing.Point(15, 222);
            this.lblCustomHeaders.Name = "lblCustomHeaders";
            this.lblCustomHeaders.Size = new System.Drawing.Size(320, 13);
            this.lblCustomHeaders.TabIndex = 12;
            this.lblCustomHeaders.Text = "Custom headers (one per line, e.g. Authorization: Bearer xyz):";

            //
            // txtCustomHeaders
            //
            this.txtCustomHeaders.Location = new System.Drawing.Point(18, 242);
            this.txtCustomHeaders.Multiline = true;
            this.txtCustomHeaders.Name = "txtCustomHeaders";
            this.txtCustomHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCustomHeaders.Size = new System.Drawing.Size(418, 115);
            this.txtCustomHeaders.TabIndex = 13;

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
            this.tabAdvanced.Controls.Add(this.grpEngine);
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
            // grpEngine
            // 
            this.grpEngine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEngine.Controls.Add(this.cboEngine);
            this.grpEngine.Controls.Add(this.lblEngine);
            this.grpEngine.Controls.Add(this.lblEngineStatus);
            this.grpEngine.Controls.Add(this.btnSetupPlaywright);
            this.grpEngine.Controls.Add(this.chkStripAnalytics);
            this.grpEngine.Location = new System.Drawing.Point(18, 330);
            this.grpEngine.Name = "grpEngine";
            this.grpEngine.Size = new System.Drawing.Size(416, 125);
            this.grpEngine.TabIndex = 9;
            this.grpEngine.TabStop = false;
            this.grpEngine.Text = "Download Engine";
            
            // 
            // lblEngine
            // 
            this.lblEngine.AutoSize = true;
            this.lblEngine.Location = new System.Drawing.Point(15, 28);
            this.lblEngine.Name = "lblEngine";
            this.lblEngine.Size = new System.Drawing.Size(45, 13);
            this.lblEngine.TabIndex = 0;
            this.lblEngine.Text = "Engine:";
            
            // 
            // cboEngine
            // 
            this.cboEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEngine.FormattingEnabled = true;
            this.cboEngine.Items.AddRange(new object[] {
                "wget (fast, no JavaScript)",
                "Playwright (full browser, handles JS sites)"});
            this.cboEngine.Location = new System.Drawing.Point(70, 25);
            this.cboEngine.Name = "cboEngine";
            this.cboEngine.Size = new System.Drawing.Size(270, 21);
            this.cboEngine.TabIndex = 1;
            this.cboEngine.SelectedIndexChanged += new System.EventHandler(this.cboEngine_SelectedIndexChanged);
            
            // 
            // lblEngineStatus
            // 
            this.lblEngineStatus.AutoSize = true;
            this.lblEngineStatus.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblEngineStatus.Location = new System.Drawing.Point(15, 55);
            this.lblEngineStatus.Name = "lblEngineStatus";
            this.lblEngineStatus.Size = new System.Drawing.Size(200, 13);
            this.lblEngineStatus.TabIndex = 2;
            this.lblEngineStatus.Text = "";
            
            // 
            // btnSetupPlaywright
            // 
            this.btnSetupPlaywright.Location = new System.Drawing.Point(15, 72);
            this.btnSetupPlaywright.Name = "btnSetupPlaywright";
            this.btnSetupPlaywright.Size = new System.Drawing.Size(130, 23);
            this.btnSetupPlaywright.TabIndex = 3;
            this.btnSetupPlaywright.Text = "Setup Playwright";
            this.btnSetupPlaywright.UseVisualStyleBackColor = true;
            this.btnSetupPlaywright.Visible = false;
            this.btnSetupPlaywright.Click += new System.EventHandler(this.btnSetupPlaywright_Click);
            
            // 
            // chkStripAnalytics
            // 
            this.chkStripAnalytics.AutoSize = true;
            this.chkStripAnalytics.Location = new System.Drawing.Point(18, 100);
            this.chkStripAnalytics.Name = "chkStripAnalytics";
            this.chkStripAnalytics.Size = new System.Drawing.Size(300, 17);
            this.chkStripAnalytics.TabIndex = 4;
            this.chkStripAnalytics.Text = "Strip analytics/tracking scripts for offline viewing";
            this.chkStripAnalytics.UseVisualStyleBackColor = true;
            
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
            this.btnSave.Location = new System.Drawing.Point(316, 500);
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
            this.btnCancel.Location = new System.Drawing.Point(397, 500);
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
            this.btnResetDefaults.Location = new System.Drawing.Point(12, 500);
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
            this.ClientSize = new System.Drawing.Size(484, 545);
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
            this.tabFilters.ResumeLayout(false);
            this.tabFilters.PerformLayout();
            this.tabAuth.ResumeLayout(false);
            this.tabAuth.PerformLayout();
            this.tabAdvanced.ResumeLayout(false);
            this.tabAdvanced.PerformLayout();
            this.grpPostDownload.ResumeLayout(false);
            this.grpPostDownload.PerformLayout();
            this.grpMultiThread.ResumeLayout(false);
            this.grpMultiThread.PerformLayout();
            this.grpEngine.ResumeLayout(false);
            this.grpEngine.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.numMaxRedirect)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDownload;
        private System.Windows.Forms.TabPage tabFilters;
        private System.Windows.Forms.TabPage tabAuth;
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
        private System.Windows.Forms.Label lblResumeMode;
        private System.Windows.Forms.ComboBox cboResumeMode;
        private System.Windows.Forms.Label lblResumeModeHint;
        private System.Windows.Forms.CheckBox chkIgnoreSsl;
        private System.Windows.Forms.CheckBox chkRandomWait;
        private System.Windows.Forms.CheckBox chkContentDisposition;
        private System.Windows.Forms.Label lblDirectoryStructure;
        private System.Windows.Forms.ComboBox cboDirectoryStructure;

        // Filters tab controls
        private System.Windows.Forms.CheckBox chkNoParent;
        private System.Windows.Forms.CheckBox chkSpanHosts;
        private System.Windows.Forms.Label lblDomains;
        private System.Windows.Forms.TextBox txtDomains;
        private System.Windows.Forms.Label lblDomainsHint;
        private System.Windows.Forms.Label lblAcceptTypes;
        private System.Windows.Forms.TextBox txtAcceptTypes;
        private System.Windows.Forms.Label lblAcceptTypesHint;
        private System.Windows.Forms.Label lblRejectTypes;
        private System.Windows.Forms.TextBox txtRejectTypes;
        private System.Windows.Forms.Label lblRejectTypesHint;
        private System.Windows.Forms.Label lblIncludeDirs;
        private System.Windows.Forms.TextBox txtIncludeDirs;
        private System.Windows.Forms.Label lblIncludeDirsHint;
        private System.Windows.Forms.Label lblExcludeDirs;
        private System.Windows.Forms.TextBox txtExcludeDirs;
        private System.Windows.Forms.Label lblExcludeDirsHint;
        private System.Windows.Forms.CheckBox chkIgnoreFilterCase;
        private System.Windows.Forms.Label lblQuota;
        private System.Windows.Forms.TextBox txtQuota;
        private System.Windows.Forms.Label lblQuotaHint;
        private System.Windows.Forms.Label lblMaxRedirect;
        private System.Windows.Forms.NumericUpDown numMaxRedirect;

        // Authentication tab controls
        private System.Windows.Forms.Label lblHttpUser;
        private System.Windows.Forms.TextBox txtHttpUser;
        private System.Windows.Forms.Label lblHttpPassword;
        private System.Windows.Forms.TextBox txtHttpPassword;
        private System.Windows.Forms.Label lblPasswordWarning;
        private System.Windows.Forms.Label lblCookiesFile;
        private System.Windows.Forms.TextBox txtCookiesFile;
        private System.Windows.Forms.Button btnBrowseCookies;
        private System.Windows.Forms.Label lblCookiesHint;
        private System.Windows.Forms.CheckBox chkKeepSessionCookies;
        private System.Windows.Forms.Label lblReferer;
        private System.Windows.Forms.TextBox txtReferer;
        private System.Windows.Forms.Label lblCustomHeaders;
        private System.Windows.Forms.TextBox txtCustomHeaders;

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
        private System.Windows.Forms.GroupBox grpEngine;
        private System.Windows.Forms.ComboBox cboEngine;
        private System.Windows.Forms.Label lblEngine;
        private System.Windows.Forms.Label lblEngineStatus;
        private System.Windows.Forms.Button btnSetupPlaywright;
        private System.Windows.Forms.CheckBox chkStripAnalytics;
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
