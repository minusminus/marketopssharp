using MarketOps.StockData.Types;
using MarketOps.SystemData.Types;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.SystemExecutor.Processor
{
    /// <summary>
    /// Calculates max back buffer for socks from SystemDataDefinition stocks list.
    /// </summary>
    internal static class StocksBackBufferAggregator
    {
        public static List<(SystemStockDataDefinition stock, int max)> Calculate(List<SystemStockDataDefinition> defs)
        {
            return defs
                .Select(x => (stock: x, max: CalcMaxBufferLength(x.stats)))
                .ToList();
        }

        private static int CalcMaxBufferLength(List<StockStat> stats) =>
            stats.Count == 0 ? 0 : stats.Max(e => e.BackBufferLength);
    }
}
