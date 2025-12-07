using System.Windows.Forms;

namespace WebsiteDownloader
{
    partial class HistoryForm
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
            this.listViewHistory = new System.Windows.Forms.ListView();
            this.columnDate = new System.Windows.Forms.ColumnHeader();
            this.columnUrl = new System.Windows.Forms.ColumnHeader();
            this.columnStatus = new System.Windows.Forms.ColumnHeader();
            this.columnDuration = new System.Windows.Forms.ColumnHeader();
            this.btnUseSelected = new System.Windows.Forms.Button();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(143, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Download History";
            
            // listViewHistory
            this.listViewHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.columnDate,
                this.columnUrl,
                this.columnStatus,
                this.columnDuration
            });
            this.listViewHistory.FullRowSelect = true;
            this.listViewHistory.GridLines = true;
            this.listViewHistory.HideSelection = false;
            this.listViewHistory.Location = new System.Drawing.Point(12, 45);
            this.listViewHistory.MultiSelect = false;
            this.listViewHistory.Name = "listViewHistory";
            this.listViewHistory.Size = new System.Drawing.Size(560, 280);
            this.listViewHistory.TabIndex = 1;
            this.listViewHistory.UseCompatibleStateImageBehavior = false;
            this.listViewHistory.View = System.Windows.Forms.View.Details;
            this.listViewHistory.SelectedIndexChanged += new System.EventHandler(this.listViewHistory_SelectedIndexChanged);
            this.listViewHistory.DoubleClick += new System.EventHandler(this.listViewHistory_DoubleClick);
            
            // columnDate
            this.columnDate.Text = "Date";
            this.columnDate.Width = 120;
            
            // columnUrl
            this.columnUrl.Text = "URL";
            this.columnUrl.Width = 280;
            
            // columnStatus
            this.columnStatus.Text = "Status";
            this.columnStatus.Width = 80;
            
            // columnDuration
            this.columnDuration.Text = "Duration";
            this.columnDuration.Width = 70;
            
            // btnUseSelected
            this.btnUseSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUseSelected.Enabled = false;
            this.btnUseSelected.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnUseSelected.Location = new System.Drawing.Point(292, 335);
            this.btnUseSelected.Name = "btnUseSelected";
            this.btnUseSelected.Size = new System.Drawing.Size(130, 35);
            this.btnUseSelected.TabIndex = 2;
            this.btnUseSelected.Text = "Use Selected URL";
            this.btnUseSelected.UseVisualStyleBackColor = true;
            this.btnUseSelected.Click += new System.EventHandler(this.btnUseSelected_Click);
            
            // btnClearHistory
            this.btnClearHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearHistory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClearHistory.Location = new System.Drawing.Point(12, 335);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(100, 35);
            this.btnClearHistory.TabIndex = 3;
            this.btnClearHistory.Text = "Clear History";
            this.btnClearHistory.UseVisualStyleBackColor = true;
            this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
            
            // btnClose
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClose.Location = new System.Drawing.Point(432, 335);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 35);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            
            // HistoryForm
            this.AcceptButton = this.btnUseSelected;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(584, 381);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClearHistory);
            this.Controls.Add(this.btnUseSelected);
            this.Controls.Add(this.listViewHistory);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 350);
            this.Name = "HistoryForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Download History";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ListView listViewHistory;
        private System.Windows.Forms.ColumnHeader columnDate;
        private System.Windows.Forms.ColumnHeader columnUrl;
        private System.Windows.Forms.ColumnHeader columnStatus;
        private System.Windows.Forms.ColumnHeader columnDuration;
        private System.Windows.Forms.Button btnUseSelected;
        private System.Windows.Forms.Button btnClearHistory;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
    }
}
