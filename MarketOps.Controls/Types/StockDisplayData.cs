using MarketOps.StockData.Types;
using System.Collections.Generic;
using System;
using MarketOps.SystemData.Types;

namespace MarketOps.Controls.Types
{
    /// <summary>
    /// Gathers stock data to be displayed.
    /// </summary>
    public class StockDisplayData
    {
        public DateTime TsFrom;
        public DateTime TsTo;
        public StockDefinition Stock;
        public StockPricesData Prices;
        public readonly List<StockStat> Stats = new List<StockStat>();
        public readonly List<Position> Positions = new List<Position>();
    }
}
