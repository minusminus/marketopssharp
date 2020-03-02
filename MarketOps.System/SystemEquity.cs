using System.Collections.Generic;

namespace MarketOps.System
{
    /// <summary>
    /// System equity data.
    /// </summary>
    public class SystemEquity
    {
        public float Cash;
        public readonly List<Position> PositionsActive = new List<Position>();
        public readonly List<Position> PositionsClosed = new List<Position>();
        public readonly List<SystemValue> Value = new List<SystemValue>();
        public readonly List<SystemValue> ClosedPositionsValue = new List<SystemValue>();
    }
}
