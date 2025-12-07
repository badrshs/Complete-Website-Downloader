using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WebsiteDownloader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Set up global exception handling
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Handles exceptions on the UI thread.
        /// </summary>
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        /// <summary>
        /// Handles exceptions on non-UI threads.
        /// </summary>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        /// <summary>
        /// Centralized exception handling with logging and user notification.
        /// </summary>
        private static void HandleException(Exception ex)
        {
            if (ex == null) return;

            try
            {
                // Log the exception
                string logPath = Path.Combine(AppConstants.AppDataFolder, "error.log");
                string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex.GetType().Name}: {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}{Environment.NewLine}";
                
                // Ensure directory exists
                if (!Directory.Exists(AppConstants.AppDataFolder))
                    Directory.CreateDirectory(AppConstants.AppDataFolder);

                File.AppendAllText(logPath, logMessage);
                Debug.WriteLine($"Unhandled exception: {ex}");
            }
            catch
            {
                // If logging fails, continue to show the message
            }

            // Show user-friendly error message
            MessageBox.Show(
                $"An unexpected error occurred:\n\n{ex.Message}\n\nThe error has been logged. Please restart the application.",
                $"{AppConstants.AppName} - Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
