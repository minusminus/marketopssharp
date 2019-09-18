using System;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Download definition for data pump
    /// </summary>
    public class DataPumpDownloadDefinition
    {
        public StockType Type;
        public string PathDaily;
        public string PathIntra;
    }
}
