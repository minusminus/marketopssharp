﻿using MarketOps.StockData.Types;

namespace MarketOps.SystemData.Types
{
    /// <summary>
    /// Signal data.
    /// </summary>
    public class Signal
    {
        public StockDefinition Stock;
        public StockDataRange DataRange;
        public int IntradayInterval;
        public SignalType Type;
        public PositionDir Direction;
        public bool ReversePosition;
        public float Price;
        public float Volume;
    }
}
