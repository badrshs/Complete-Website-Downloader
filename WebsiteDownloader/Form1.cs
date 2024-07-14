using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebsiteDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    outputFolder.Text = fbd.SelectedPath;
                    Download.Enabled = true;
                }
            }
        }

        private void Download_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(urlInput.Text))
            {
                MessageBox.Show("You have to provide the URL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tempPath = Path.Combine(Path.GetTempPath(), "wget.exe");

            try
            {
                if (!File.Exists(tempPath))
                {
                    using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WebsiteDownloader.wget.exe"))
                    {
                        if (resourceStream == null)
                            throw new Exception("Cannot find embedded wget.exe resource.");

                        using (var fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
                        {
                            resourceStream.CopyTo(fileStream);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error extracting wget.exe: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var url = new Uri(urlInput.Text);
            var outputFolderTextUrlHost = Path.Combine(outputFolder.Text, url.Host);
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = tempPath,
                    WorkingDirectory = outputFolder.Text,
                    Arguments = $"-r -p -e robots=off -U mozilla {url} -P ./{url.Host}"
                }
            };

            Task.Run(() =>
            {
                process.Start();
                process.WaitForExit();

                if (Directory.Exists(outputFolderTextUrlHost))
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = outputFolderTextUrlHost,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                else
                {
                    MessageBox.Show("Something went wrong..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }
    }
}
