using System.Collections.Generic;
using System.Linq;

namespace MarketOps.Config.Stats
{
    public static class StatsFactories
    {
        private static StatsFactory _priceChart;
        private static StatsFactory _additional;

        public static StatsFactory PriceChart => _priceChart;
        public static StatsFactory Additional => _additional;

        public static void Initialize()
        {
            List<ConfigStatDefinition> stats = ConfigStatDefsLoader.Load();
            _priceChart = new StatsFactory(stats.Where(x => x.DisplayStatType == ConfigDisplayStatType.PriceChart));
            _additional = new StatsFactory(stats.Where(x => x.DisplayStatType == ConfigDisplayStatType.Additional));
        }
    }
}
