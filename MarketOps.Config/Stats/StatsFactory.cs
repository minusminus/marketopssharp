using System;
using System.Collections.Generic;
using System.Linq;
using MarketOps.StockData.Types;

namespace MarketOps.Config.Stats
{
    /// <summary>
    /// Factory of StockStat objects.
    /// </summary>
    public class StatsFactory
    {
        private readonly Dictionary<string, ConfigStatDefinition> _stats = new Dictionary<string, ConfigStatDefinition>();

        public StatsFactory(IEnumerable<ConfigStatDefinition> definitions)
        {
            foreach (var def in definitions)
                _stats.Add(def.StatName, def);
        }

        public List<string> GetList() => _stats.Keys.OrderBy(x => x).ToList();

        public StockStat Get(string statName, string chartArea)
        {
            if (!_stats.TryGetValue(statName, out ConfigStatDefinition config))
                throw new Exception($"Stat not found: {statName}");

            return (StockStat)Activator.CreateInstance(ClassesLoader.FindType(config.ClassLibrary, config.ClassName), chartArea);
        }
    }
}
