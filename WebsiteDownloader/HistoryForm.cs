using System;
using System.Windows.Forms;
using WebsiteDownloader.Models;
using WebsiteDownloader.Resources;

namespace WebsiteDownloader
{
    public partial class HistoryForm : Form
    {
        private readonly DownloadHistory _history;
        public string SelectedUrl { get; private set; }

        public HistoryForm(DownloadHistory history)
        {
            _history = history ?? throw new ArgumentNullException(nameof(history));
            InitializeComponent();
            LoadHistory();
        }

        private void LoadHistory()
        {
            listViewHistory.Items.Clear();

            foreach (var item in _history.Items)
            {
                var listItem = new ListViewItem(new[]
                {
                    item.DownloadDate.ToString("yyyy-MM-dd HH:mm"),
                    item.Url,
                    item.Success ? Strings.StatusSuccess : Strings.StatusFailed,
                    item.Duration.ToString(@"mm\:ss")
                });

                listItem.Tag = item.Url;
                listItem.ForeColor = item.Success 
                    ? System.Drawing.Color.DarkGreen 
                    : System.Drawing.Color.DarkRed;

                listViewHistory.Items.Add(listItem);
            }

            btnClearHistory.Enabled = _history.Items.Count > 0;
            btnUseSelected.Enabled = false;
        }

        private void listViewHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnUseSelected.Enabled = listViewHistory.SelectedItems.Count > 0;
        }

        private void listViewHistory_DoubleClick(object sender, EventArgs e)
        {
            if (listViewHistory.SelectedItems.Count > 0)
            {
                SelectedUrl = listViewHistory.SelectedItems[0].Tag as string;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnUseSelected_Click(object sender, EventArgs e)
        {
            if (listViewHistory.SelectedItems.Count > 0)
            {
                SelectedUrl = listViewHistory.SelectedItems[0].Tag as string;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                Strings.ConfirmClearHistoryMessage,
                Strings.ConfirmClearHistoryTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _history.Clear();
                LoadHistory();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
