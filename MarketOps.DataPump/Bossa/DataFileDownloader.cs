using System;
using MarketOps.DataPump.Types;
using MarketOps.StockData.Types;

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
        private readonly DownloadDirectories _downloadDirectories;
        private readonly DownloadUrlPrepator _downloadUrlPrepator;

        public DataFileDownloader(DownloadPipe downloadPipe, DownloadFilesQueue downloadFilesQueue, DownloadDirectories downloadDirectories, DownloadUrlPrepator downloadUrlPrepator)
        {
            _downloadPipe = downloadPipe;
            _downloadFilesQueue = downloadFilesQueue;
            _downloadDirectories = downloadDirectories;
            _downloadUrlPrepator = downloadUrlPrepator;
        }

        public string InitializeDownload(StockDefinition stockDefinition, DataPumpDownloadRange downloadRange)
        {
            string downloadFilePath = GetDownloadFilePath(stockDefinition, downloadRange);
            string downloadUrl = GetDownloadUrl(stockDefinition, downloadRange);

            if (!AlreadyDownloaded(downloadUrl))
                DownloadAndProcess(downloadUrl, downloadFilePath);

            return downloadFilePath;
        }

        public void WaitForDownload(string downloadFilePath)
        {
            //
        }

        private string GetDownloadFilePath(StockDefinition stockDefinition, DataPumpDownloadRange downloadRange)
        {
            return _downloadDirectories.PreparePath(stockDefinition.Type, downloadRange);
        }

        private string GetDownloadUrl(StockDefinition stockDefinition, DataPumpDownloadRange downloadRange)
        {
            return _downloadUrlPrepator.Prepare(stockDefinition, downloadRange);
        }

        private bool AlreadyDownloaded(string downloadUrl)
        {
            return (_downloadFilesQueue.GetStage(downloadUrl) == DownloadFileStage.Done);
        }

        private void DownloadAndProcess(string downloadUrl, string downloadFilePath)
        {
            _downloadFilesQueue.AddToDownload(downloadUrl);
            _downloadFilesQueue.Next();
            _downloadPipe.Process(downloadUrl, downloadFilePath);
        }
    }
}
