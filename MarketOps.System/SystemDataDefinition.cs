using MarketOps.StockData.Types;
using System.Collections.Generic;

namespace MarketOps.System
{
    /// <summary>
    /// System data definition.
    /// </summary>
    public class SystemDataDefinition
    {
        public List<string> stocks;
        public Dictionary<string, StockStat> statsForStocks;
    }
}
