using MarketOps.StockData.Types;
using System.Collections.Generic;

namespace MarketOps.SystemData.Types
{
    /// <summary>
    /// Single stock definition for system.
    /// </summary>
    public class SystemStockDataDefinition
    {
        public StockDefinition stock;
        public StockDataRange dataRange;
        public List<StockStat> stats;
    }
}
