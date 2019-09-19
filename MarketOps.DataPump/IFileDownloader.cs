using System;

namespace MarketOps.DataPump
{
    /// <summary>
    /// File downloading interface
    /// </summary>
    internal interface IFileDownloader
    {
        void Download(string url, string destFilePath);
    }
}
