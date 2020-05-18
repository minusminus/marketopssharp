using MarketOps.StockData.Types;
using System;
using System.Collections.Generic;

namespace MarketOps.System
{
    /// <summary>
    /// System configuration data.
    /// </summary>
    public class SystemConfiguration
    {
        public DateTime tsFrom;
        public DateTime tsTo;
        public List<string> stocks;
        public Dictionary<string, StockStat> stats;
    }
}
