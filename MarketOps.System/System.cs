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
        public readonly List<float> ValueOnPositions = new List<float>();
    }
}
