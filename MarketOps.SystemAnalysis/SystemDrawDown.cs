using MarketOps.SystemExecutor;
using System;

namespace MarketOps.SystemAnalysis
{
    /// <summary>
    /// System drawdown entry.
    /// </summary>
    public class SystemDrawDown
    {
        public SystemValue TopValue;
        public SystemValue BottomValue;
        public DateTime LastTS;
        public int Ticks;
    }
}
