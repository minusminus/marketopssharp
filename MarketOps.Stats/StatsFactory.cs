using System;
using System.Collections.Generic;
using System.Linq;
using MarketOps.StockData.Types;

namespace MarketOps.Stats
{
    /// <summary>
    /// Base class for stats factories.
    /// </summary>
    public class StatsFactory
    {
        protected Dictionary<string, Type> Stats;

        public List<string> GetList()
        {
            return Stats.Keys.OrderBy(x => x).ToList();
        }

        public StockStat Get(string statName)
        {
            Type t;
            if (!Stats.TryGetValue(statName, out t))
                throw new Exception($"Not found stat: {statName}");

            return (StockStat)Activator.CreateInstance(t);
        }
    }
}
