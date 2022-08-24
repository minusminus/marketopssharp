using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using System.Collections.Generic;

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
        public SignalInitialStopMode InitialStopMode;
        public float InitialStopValue;
        public IMMPositionCloseCalculator PositionCloseCalculator;
        public bool Rebalance;
        public List<(StockDefinition stockDef, float balance)> NewBalance;
    }
}
