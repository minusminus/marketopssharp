using System;

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
    }
}
