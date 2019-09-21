using System;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Bossa data files downloading
    /// </summary>
    internal class DataFileDownloader : IDataFileDownloader
    {
        private readonly IFileDownloader _fileDownloader;
        private readonly IFileUnzipper _fileUnzipper;
        private readonly DownloadDirectories _downloadDirectories;

        public DataFileDownloader(IFileDownloader fileDownloader, IFileUnzipper fileUnzipper, DownloadDirectories downloadDirectories)
        {
            _fileDownloader = fileDownloader;
            _fileUnzipper = fileUnzipper;
            _downloadDirectories = downloadDirectories;
        }
    }
}
