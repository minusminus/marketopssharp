using MarketOps.StockData.Types;

namespace MarketOps.System
{
    /// <summary>
    /// Signal data.
    /// </summary>
    public struct Signal
    {
        public StockDefinition Stock;
        public SignalType Type;
        public PositionDir Direction;
        public float Price;
        public int Volume;
    }
}
