using MarketOps.StockData.Types;
using System.Collections.Generic;

namespace MarketOps.System
{
    /// <summary>
    /// Single stock definition for system.
    /// </summary>
    public class SystemStockDataDefinition
    {
        public string name;
        public StockDataRange dataRange;
        public List<StockStat> stats;
    }
}
