using System;
using System.Collections.Generic;
using MarketOps.Stats.Stats;

namespace MarketOps.Stats
{
    /// <summary>
    /// Factory for stats located on price chart.
    /// </summary>
    internal class PriceChartStatsFactory : StatsFactory
    {
        public PriceChartStatsFactory()
        {
            Stats = new Dictionary<string, Type>()
            {
                {new StatSMA().Name, typeof (StatSMA)},
                {new StatBB().Name, typeof (StatBB)},
            };
        }
    }
}
