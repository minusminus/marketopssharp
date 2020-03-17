using System;
using MarketOps.StockData.Types;

namespace MarketOps.System
{
    /// <summary>
    /// Position data.
    /// </summary>
    public struct Position
    {
        public StockDefinition Stock;
        public StockDataRange DataRange;
        public int IntradayInterval;
        public PositionDir Direction;
        public DateTime TSOpen;
        public DateTime TSClose;
        public float Open;
        public float Close;
        public int Volume;
        public int Ticks;
    }
}
