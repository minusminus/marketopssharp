using System;
using MarketOps.DataPump.Types;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump
{
    /// <summary>
    /// Interface for data files downloading
    /// </summary>
    internal interface IDataFileDownloader
    {
        string InitializeDownload(StockDefinition stockDefinition, DataPumpDownloadRange downloadRange);
        void WaitForDownload(string downloadFilePath);
    }
}
