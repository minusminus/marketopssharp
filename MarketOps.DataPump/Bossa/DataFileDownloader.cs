using System;
using MarketOps.DataPump.Types;
using MarketOps.StockData.Types;
using System.IO;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Bossa data files downloading.
    /// 
    /// Simple download on demand in current thread.
    /// </summary>
    internal class DataFileDownloader : IDataFileDownloader
    {
        private readonly DownloadPipe _downloadPipe;
        private readonly DownloadFilesQueue _downloadFilesQueue;
        private readonly DownloadUrlPrepator _downloadUrlPrepator;
        private readonly DownloadFilePathPreparator _downloadFilePathPreparator;
        private readonly DownloadUnzipPathPreparator _downloadUnzipPathPreparator;

        public DataFileDownloader(DownloadPipe downloadPipe, DownloadFilesQueue downloadFilesQueue, DownloadUrlPrepator downloadUrlPrepator, DownloadFilePathPreparator downloadFilePathPreparator, DownloadUnzipPathPreparator downloadUnzipPathPreparator)
        {
            _downloadPipe = downloadPipe;
            _downloadFilesQueue = downloadFilesQueue;
            _downloadUrlPrepator = downloadUrlPrepator;
            _downloadFilePathPreparator = downloadFilePathPreparator;
            _downloadUnzipPathPreparator = downloadUnzipPathPreparator;
        }

        public string InitializeDownload(StockDefinition stockDefinition, DataPumpDownloadRange downloadRange)
        {
            string downloadFilePath = GetDownloadFilePath(stockDefinition, downloadRange);
            string unzipPath = GetUnzipPath(downloadFilePath);
            string downloadUrl = GetDownloadUrl(stockDefinition, downloadRange);

            if (!AlreadyDownloaded(downloadUrl))
                DownloadAndProcess(downloadUrl, downloadFilePath, unzipPath);

            return downloadFilePath;
        }

        public void WaitForDownload(string downloadFilePath)
        {
            //
        }

        private string GetDownloadFilePath(StockDefinition stockDefinition, DataPumpDownloadRange downloadRange)
        {
            return _downloadFilePathPreparator.Prepare(stockDefinition, downloadRange);
        }

        private string GetUnzipPath(string downloadFilePath)
        {
            return _downloadUnzipPathPreparator.Prepare(downloadFilePath);
        }

        private string GetDownloadUrl(StockDefinition stockDefinition, DataPumpDownloadRange downloadRange)
        {
            return _downloadUrlPrepator.Prepare(stockDefinition, downloadRange);
        }

        private bool AlreadyDownloaded(string downloadUrl)
        {
            return (_downloadFilesQueue.GetStage(downloadUrl) == DownloadFileStage.Done);
        }

        private void DownloadAndProcess(string downloadUrl, string downloadFilePath, string unzipPath)
        {
            _downloadFilesQueue.AddToDownload(downloadUrl);
            _downloadFilesQueue.Next();
            _downloadPipe.Process(downloadUrl, downloadFilePath, unzipPath);
        }
    }
}
