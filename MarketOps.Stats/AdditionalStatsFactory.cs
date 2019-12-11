using System;
using System.Collections.Generic;
using MarketOps.Stats.Stats;

namespace MarketOps.Stats
{
    /// <summary>
    /// Factory for stats located under price chart.
    /// </summary>
    internal class AdditionalStatsFactory : StatsFactory
    {
        public AdditionalStatsFactory()
        {
            Stats = new Dictionary<string, Type>()
            {
                {new StatATR("").Name, typeof (StatATR)},
            };
        }
    }
}
