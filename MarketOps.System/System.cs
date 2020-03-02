using System;
using System.Collections.Generic;

namespace MarketOps.System
{
    /// <summary>
    /// System equity data.
    /// </summary>
    public class System
    {
        public float Cash;
        public readonly List<Position> PositionsActive = new List<Position>();
        public readonly List<Position> PositionsClosed = new List<Position>();
        public readonly List<float> Value = new List<float>();
        public readonly List<DateTime> ValueTS = new List<DateTime>();
        public readonly List<float> ValueOnPositions = new List<float>();
        public readonly List<DateTime> ValueOnPositionsTS = new List<DateTime>();
    }
}
