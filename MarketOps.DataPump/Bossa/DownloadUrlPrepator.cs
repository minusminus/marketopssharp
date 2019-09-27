using System;
using System.Collections.Generic;
using MarketOps.DataPump.Types;
using MarketOps.StockData.Types;
using Flurl;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Bossa download url generator.
    /// </summary>
    internal class DownloadUrlPrepator
    {
        private readonly Dictionary<StockType, DataPumpDownloadDefinition> _downloadDefinitions;

        public DownloadUrlPrepator(Dictionary<StockType, DataPumpDownloadDefinition> downloadDefinitions)
        {
            _downloadDefinitions = downloadDefinitions;
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
            return Url.Combine(_downloadDefinitions[stockDefinition.Type].PathDaily,
                _downloadDefinitions[stockDefinition.Type].FileNameDaily);
        }

        private string GetPathTicks(StockDefinition stockDefinition)
        {
            //return Path.Combine(_downloadDefinitions[stockDefinition.Type].PathIntra, "");
            throw new NotImplementedException();
        }
    }
}
