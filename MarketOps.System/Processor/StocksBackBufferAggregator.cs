﻿using MarketOps.StockData.Types;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.System.Processor
{
    /// <summary>
    /// Calculates max back buffer for socks from SystemDataDefinition.statsForStocks
    /// </summary>
    internal static class StocksBackBufferAggregator
    {
        public static Dictionary<string, int> Calculate(Dictionary<string, List<StockStat>> statsForStocks)
        {
            return statsForStocks
                .ToDictionary(g => g.Key, g => g.Value.Max(e => e.BackBufferLength));
        }
    }
}
