using System;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Bossa data files downloading
    /// </summary>
    internal class DataFileDownloader : IDataFileDownloader
    {
        private readonly DownloadPipe _downloadPipe;
        private readonly DownloadFilesQueue _downloadFilesQueue;
        private readonly DownloadDirectories _downloadDirectories;

        public DataFileDownloader(DownloadPipe downloadPipe, DownloadFilesQueue downloadFilesQueue, DownloadDirectories downloadDirectories)
        {
            _downloadPipe = downloadPipe;
            _downloadFilesQueue = downloadFilesQueue;
            _downloadDirectories = downloadDirectories;
        }
    }
}
