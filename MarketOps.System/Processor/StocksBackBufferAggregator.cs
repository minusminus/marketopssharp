using System.Collections.Generic;
using System.Linq;

namespace MarketOps.System.Processor
{
    /// <summary>
    /// Calculates max back buffer for socks from SystemDataDefinition stocks list.
    /// </summary>
    internal static class StocksBackBufferAggregator
    {
        public static List<(SystemStockDataDefinition stock, int max)> Calculate(List<SystemStockDataDefinition> defs)
        {
            return defs
                .Select(x => (stock: x, max: x.stats.Max(e => e.BackBufferLength)))
                .ToList();
        }
    }
}
