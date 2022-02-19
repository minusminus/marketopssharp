using System;
using System.Collections.Generic;
using MarketOps.StockData.Types;

namespace MarketOps.SystemData.Types
{
    public class PositionTrailingStopData
    {
        public DateTime TS;
        public float Value;
    }

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
        public float Volume;
        public int TicksActive;
        public PositionCloseMode CloseMode;
        public float CloseModePrice;
        public readonly List<PositionTrailingStopData> TrailingStop = new List<PositionTrailingStopData>();
    }
}
