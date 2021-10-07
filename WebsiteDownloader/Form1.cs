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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    //  string[] files = Directory.GetFiles(fbd.SelectedPath);
                    //   string[] directories = Directory.GetDirectories(fbd.SelectedPath);

                    outputFolder.Text = fbd.SelectedPath;
                    Download.Enabled = true;
                }
            }
        }

        private void Download_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(urlInput.Text))
            {
                MessageBox.Show("You have to provide the url", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (!File.Exists("C:\\wget.exe"))
                {
                    using (var fileStream = new FileStream(@"C:\wget.exe", FileMode.Create, FileAccess.Write))
                    {
                        using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                        {
                            binaryWriter.Write(Properties.Resources.Runner);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Permission required to run this application, try to run it as administrator, ERROR: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var url = new Uri(urlInput.Text);
            var outputFolderTextUrlHost = $"{outputFolder.Text}\\{url.Host}";
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "C:\\wget.exe",
                    WorkingDirectory = outputFolder.Text,
                    Arguments = $"-r -p -e robots=off -U mozilla  {url} -P ./{url.Host} ",
                }
            };

            Task.Run(() =>
             {
                 process.Start();
                 process.WaitForExit();

                 if (Directory.Exists(outputFolderTextUrlHost))
                 {
                     Process.Start(outputFolderTextUrlHost);
                 }
                 else
                 {
                     MessageBox.Show("Something went wrong..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
             });
        }
    }
}
