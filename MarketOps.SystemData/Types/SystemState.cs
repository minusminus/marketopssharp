using System;
using System.Collections.Generic;

namespace MarketOps.SystemData.Types
{
    /// <summary>
    /// System state data.
    /// </summary>
    public class SystemState
    {
        public float InitialCash;
        public float Cash;
        public DateTime LastProcessedTS;
        public readonly List<Signal> Signals = new List<Signal>();
        public readonly List<Position> PositionsActive = new List<Position>();
        public readonly List<Position> PositionsClosed = new List<Position>();
        public readonly List<SystemValue> Equity = new List<SystemValue>();
        public readonly List<SystemValue> EquityCapitalUsage = new List<SystemValue>();
        public readonly List<SystemValue> ClosedPositionsEquity = new List<SystemValue>();
    }
}
