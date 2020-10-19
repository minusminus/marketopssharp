using MarketOps.StockData.Types;

namespace MarketOps.SystemExecutor
{
    /// <summary>
    /// Signal data.
    /// </summary>
    public struct Signal
    {
        public StockDefinition Stock;
        public StockDataRange DataRange;
        public int IntradayInterval;
        public SignalType Type;
        public PositionDir Direction;
        public bool ReversePosition;
        public float Price;
        public int Volume;
    }
}
