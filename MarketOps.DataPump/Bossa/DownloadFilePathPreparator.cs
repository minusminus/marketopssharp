using System;
using System.Collections.Generic;
using MarketOps.DataPump.Types;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Bossa download file path preparator.
    /// </summary>
    internal class DownloadFilePathPreparator
    {
        private readonly Dictionary<StockType, DataPumpDownloadDefinition> _downloadDefinitions;
        private readonly DownloadDirectories _downloadDirectories;

        public DownloadFilePathPreparator(Dictionary<StockType, DataPumpDownloadDefinition> downloadDefinitions, DownloadDirectories downloadDirectories)
        {
            _downloadDefinitions = downloadDefinitions;
            _downloadDirectories = downloadDirectories;
        }

        public string Prepare(StockDefinition stockDefinition, DataPumpDownloadRange downloadRange)
        {
            switch (downloadRange)
            {
                case DataPumpDownloadRange.Daily:
                    return GetPathDaily(stockDefinition);
                case DataPumpDownloadRange.Ticks:
                    return GetPathTicks(stockDefinition);
            }
            return "";
        }

        private string GetPathDaily(StockDefinition stockDefinition)
        {
            return _downloadDirectories.GetDownloadPath(_downloadDefinitions[stockDefinition.Type].FileNameDaily);
        }

        private string GetPathTicks(StockDefinition stockDefinition)
        {
            throw new NotImplementedException();
        }
    }
}
