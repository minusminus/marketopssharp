using System;
using System.Collections.Generic;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// gathers stock data to be displayed
    /// </summary>
    public class StockDisplayData
    {
        public DateTime TsFrom;
        public DateTime TsTo;
        public StockDefinition Stock;
        public StockPricesData Prices;
        public readonly List<StockStat> Stats = new List<StockStat>();
    }
}
