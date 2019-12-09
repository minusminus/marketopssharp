using System;

namespace MarketOps.Stats
{
    public static class StatsFactories
    {
        public static readonly StatsFactory PriceChart = new PriceChartStatsFactory();
        public static readonly StatsFactory Additional = new AdditionalStatsFactory();
    }
}
