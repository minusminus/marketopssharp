using System;
using System.IO;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Bossa file downloading and unzipping pipe.
    /// Downloaded file is unzipped in download directory.
    /// </summary>
    internal class DownloadPipe
    {
        private readonly IFileDownloader _fileDownloader;
        private readonly IFileUnzipper _fileUnzipper;
        private readonly DownloadFilesQueue _downloadFilesQueue;

        public DownloadPipe(IFileDownloader fileDownloader, IFileUnzipper fileUnzipper, DownloadFilesQueue downloadFilesQueue)
        {
            _fileDownloader = fileDownloader;
            _fileUnzipper = fileUnzipper;
            _downloadFilesQueue = downloadFilesQueue;
        }

        public void Process(string downloadUrl, string destFilePath)
        {
            _downloadFilesQueue.SetStage(downloadUrl, DownloadFileStage.Download);
            Download(downloadUrl, destFilePath);
            _downloadFilesQueue.SetStage(downloadUrl, DownloadFileStage.Unzip);
            Unzip(destFilePath);
            _downloadFilesQueue.SetStage(downloadUrl, DownloadFileStage.Done);
        }

        private void Download(string downloadUrl, string destFilePath)
        {
            _fileDownloader.Download(downloadUrl, destFilePath);
        }

        private void Unzip(string zipFilePath)
        {
            _fileUnzipper.Unzip(zipFilePath, Path.GetDirectoryName(zipFilePath));
        }
    }
}
