using System;

namespace MarketOps.SystemData.Types
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
