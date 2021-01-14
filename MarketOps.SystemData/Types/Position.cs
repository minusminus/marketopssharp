using System;
using MarketOps.StockData.Types;

namespace MarketOps.SystemData.Types
{
    /// <summary>
    /// Position data.
    /// </summary>
    public class Position
    {
        public Signal EntrySignal;
        public StockDefinition Stock;
        public StockDataRange DataRange;
        public int IntradayInterval;
        public PositionDir Direction;
        public DateTime TSOpen;
        public float Open;
        public float OpenCommission;
        public DateTime TSClose;
        public float Close;
        public float CloseCommission;
        public int Volume;
        public int TicksActive;
        public PositionCloseMode CloseMode;
        public float CloseModePrice;
    }
}
