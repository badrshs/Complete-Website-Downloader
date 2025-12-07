using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebsiteDownloader.Services
{
    /// <summary>
    /// Interface for website downloading operations.
    /// Enables dependency injection and unit testing.
    /// </summary>
    public interface IWebsiteDownloader : IDisposable
    {
        /// <summary>
        /// Event raised when download progress is reported.
        /// </summary>
        event EventHandler<DownloadProgressEventArgs> ProgressChanged;

        /// <summary>
        /// Event raised when the download operation completes.
        /// </summary>
        event EventHandler<DownloadCompletedEventArgs> DownloadCompleted;

        /// <summary>
        /// Gets a value indicating whether a download is currently in progress.
        /// </summary>
        bool IsDownloading { get; }

        /// <summary>
        /// Downloads a website recursively to the specified output folder.
        /// </summary>
        /// <param name="options">The download configuration options.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DownloadAsync(DownloadOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Cancels the current download operation.
        /// </summary>
        void CancelDownload();
    }
}
