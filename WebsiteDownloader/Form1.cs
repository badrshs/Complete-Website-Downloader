using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebsiteDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Download.Enabled = false;
        }

        private async void SelectFolderButton_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    outputFolderTextBox.Text = folderDialog.SelectedPath;
                    Download.Enabled = true;
                }
            }
        }

        private async void DownloadButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(urlTextBox.Text))
            {
                MessageBox.Show("Please enter a valid URL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string wgetPath = @"C:\wget.exe";
                if (!File.Exists(wgetPath))
                {
                    using (var fileStream = new FileStream(wgetPath, FileMode.Create, FileAccess.Write))
                    {
                        using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                        {
                            binaryWriter.Write(Properties.Resources.Runner);
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Permission required to run this application, try running it as administrator. ERROR: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
             }

        Uri url;

        if (!Uri.TryCreate(urlTextBox.Text, UriKind.Absolute, out url))
        {
            MessageBox.Show("Please enter a valid URL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string outputFolderTextUrlHost = $"{outputFolderTextBox.Text}\\{url.Host}";
        var processStartInfo = new ProcessStartInfo
        {
            FileName = @"C:\wget.exe",
            WorkingDirectory = outputFolderTextBox.Text,
            Arguments = $"-r -p -e robots=off -U mozilla {url} -P ./{url.Host}",
        };
        var process = new Process { StartInfo = processStartInfo };
        await Task.Run(() =>
        {
            process.Start();
            process.WaitForExit();
        });

        if (Directory.Exists(outputFolderTextUrlHost))
        {
            Process.Start(outputFolderTextUrlHost);
        }
        else
        {
            MessageBox.Show("Something went wrong..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
     }
    }
}
