using System.Drawing;
using System.Windows.Forms;

namespace WebsiteDownloader
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.lblUrlLabel = new System.Windows.Forms.Label();
            this.lblOutputFolderLabel = new System.Windows.Forms.Label();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.tabControlOutput = new System.Windows.Forms.TabControl();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.tabPageErrors = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.listViewErrors = new System.Windows.Forms.ListView();
            this.columnErrorType = new System.Windows.Forms.ColumnHeader();
            this.columnErrorMessage = new System.Windows.Forms.ColumnHeader();
            this.columnErrorTime = new System.Windows.Forms.ColumnHeader();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusErrorCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            
            this.tabControlOutput.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.tabPageErrors.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            
            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.pictureBoxLogo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(634, 60);
            this.panelHeader.TabIndex = 0;
            
            // pictureBoxLogo
            this.pictureBoxLogo.Image = global::WebsiteDownloader.Properties.Resources.Icon1.ToBitmap();
            this.pictureBoxLogo.Location = new System.Drawing.Point(12, 8);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(44, 44);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(62, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(220, 30);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Website Downloader";
            
            // lblUrlLabel
            this.lblUrlLabel.AutoSize = true;
            this.lblUrlLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUrlLabel.Location = new System.Drawing.Point(12, 75);
            this.lblUrlLabel.Name = "lblUrlLabel";
            this.lblUrlLabel.Size = new System.Drawing.Size(87, 19);
            this.lblUrlLabel.TabIndex = 1;
            this.lblUrlLabel.Text = "Website URL:";
            
            // txtUrl
            this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUrl.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUrl.Location = new System.Drawing.Point(12, 97);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(610, 27);
            this.txtUrl.TabIndex = 0;
            // Note: PlaceholderText not available in .NET Framework - using cue banner via MainForm.Load
            
            // lblOutputFolderLabel
            this.lblOutputFolderLabel.AutoSize = true;
            this.lblOutputFolderLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOutputFolderLabel.Location = new System.Drawing.Point(12, 135);
            this.lblOutputFolderLabel.Name = "lblOutputFolderLabel";
            this.lblOutputFolderLabel.Size = new System.Drawing.Size(96, 19);
            this.lblOutputFolderLabel.TabIndex = 3;
            this.lblOutputFolderLabel.Text = "Output Folder:";
            
            // lblOutputFolder
            this.lblOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutputFolder.AutoEllipsis = true;
            this.lblOutputFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOutputFolder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOutputFolder.Location = new System.Drawing.Point(12, 157);
            this.lblOutputFolder.Name = "lblOutputFolder";
            this.lblOutputFolder.Size = new System.Drawing.Size(502, 27);
            this.lblOutputFolder.TabIndex = 4;
            this.lblOutputFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // btnSelectFolder
            this.btnSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFolder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSelectFolder.Location = new System.Drawing.Point(520, 155);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(102, 30);
            this.btnSelectFolder.TabIndex = 1;
            this.btnSelectFolder.Text = "Browse...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            
            // btnDownload
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnDownload.FlatAppearance.BorderSize = 0;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDownload.ForeColor = System.Drawing.Color.White;
            this.btnDownload.Location = new System.Drawing.Point(392, 200);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(230, 40);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "⬇  Download Website";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            
            // btnCancel
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(201, 79, 79);
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(392, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(230, 40);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "✖  Cancel Download";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            // btnSettings
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSettings.Location = new System.Drawing.Point(12, 200);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(90, 40);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "⚙ Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            
            // btnHistory
            this.btnHistory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnHistory.Location = new System.Drawing.Point(108, 200);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(90, 40);
            this.btnHistory.TabIndex = 4;
            this.btnHistory.Text = "📋 History";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            
            // progressBar
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 250);
            this.progressBar.MarqueeAnimationSpeed = 30;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(610, 8);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 9;
            this.progressBar.Visible = false;
            
            // tabControlOutput
            this.tabControlOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlOutput.Controls.Add(this.tabPageLog);
            this.tabControlOutput.Controls.Add(this.tabPageErrors);
            this.tabControlOutput.Location = new System.Drawing.Point(12, 262);
            this.tabControlOutput.Name = "tabControlOutput";
            this.tabControlOutput.SelectedIndex = 0;
            this.tabControlOutput.Size = new System.Drawing.Size(610, 150);
            this.tabControlOutput.TabIndex = 10;
            
            // tabPageLog
            this.tabPageLog.Controls.Add(this.txtLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 24);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLog.Size = new System.Drawing.Size(602, 122);
            this.tabPageLog.TabIndex = 0;
            this.tabPageLog.Text = "📋 Output";
            this.tabPageLog.UseVisualStyleBackColor = true;
            
            // tabPageErrors
            this.tabPageErrors.Controls.Add(this.listViewErrors);
            this.tabPageErrors.Location = new System.Drawing.Point(4, 24);
            this.tabPageErrors.Name = "tabPageErrors";
            this.tabPageErrors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageErrors.Size = new System.Drawing.Size(602, 122);
            this.tabPageErrors.TabIndex = 1;
            this.tabPageErrors.Text = "⚠ Errors (0)";
            this.tabPageErrors.UseVisualStyleBackColor = true;
            
            // txtLog
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(596, 116);
            this.txtLog.TabIndex = 0;
            
            // listViewErrors
            this.listViewErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.columnErrorType,
                this.columnErrorMessage,
                this.columnErrorTime
            });
            this.listViewErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewErrors.FullRowSelect = true;
            this.listViewErrors.GridLines = true;
            this.listViewErrors.Location = new System.Drawing.Point(3, 3);
            this.listViewErrors.Name = "listViewErrors";
            this.listViewErrors.Size = new System.Drawing.Size(596, 116);
            this.listViewErrors.TabIndex = 0;
            this.listViewErrors.UseCompatibleStateImageBehavior = false;
            this.listViewErrors.View = System.Windows.Forms.View.Details;
            
            // columnErrorType
            this.columnErrorType.Text = "Type";
            this.columnErrorType.Width = 70;
            
            // columnErrorMessage
            this.columnErrorMessage.Text = "Message";
            this.columnErrorMessage.Width = 420;
            
            // columnErrorTime
            this.columnErrorTime.Text = "Time";
            this.columnErrorTime.Width = 80;
            
            // statusStrip
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.statusLabel, this.statusErrorCount });
            this.statusStrip.Location = new System.Drawing.Point(0, 419);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(634, 22);
            this.statusStrip.TabIndex = 11;
            
            // statusLabel
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Ready";
            
            // statusErrorCount
            this.statusErrorCount.Name = "statusErrorCount";
            this.statusErrorCount.Size = new System.Drawing.Size(0, 17);
            this.statusErrorCount.ForeColor = System.Drawing.Color.OrangeRed;
            
            // MainForm
            this.AcceptButton = this.btnDownload;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 441);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tabControlOutput);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.lblOutputFolder);
            this.Controls.Add(this.lblOutputFolderLabel);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.lblUrlLabel);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = global::WebsiteDownloader.Properties.Resources.Icon1;
            this.MinimumSize = new System.Drawing.Size(500, 480);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Website Downloader";
            
            this.tabControlOutput.ResumeLayout(false);
            this.tabPageLog.ResumeLayout(false);
            this.tabPageLog.PerformLayout();
            this.tabPageErrors.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label lblUrlLabel;
        private System.Windows.Forms.Label lblOutputFolderLabel;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.TabControl tabControlOutput;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.TabPage tabPageErrors;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ListView listViewErrors;
        private System.Windows.Forms.ColumnHeader columnErrorType;
        private System.Windows.Forms.ColumnHeader columnErrorMessage;
        private System.Windows.Forms.ColumnHeader columnErrorTime;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusErrorCount;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
    }
}
