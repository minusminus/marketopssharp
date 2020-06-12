using MarketOps.StockData.Types;
using System.Collections.Generic;

namespace MarketOps.System
{
    /// <summary>
    /// System data definition.
    /// </summary>
    public class SystemDataDefinition
    {
        public List<SystemStockDataDefinition> stocks;
        public Dictionary<SystemStockDataDefinition, List<StockStat>> statsForStocks;
    }
}
