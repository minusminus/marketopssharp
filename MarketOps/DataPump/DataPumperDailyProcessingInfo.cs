using MarketOps.StockData.Types;

namespace MarketOps.DataPump
{
    /// <summary>
    /// Daily data processing information.
    /// </summary>
    public class DataPumperDailyProcessingInfo
    {
        public StockDefinition Stock;
        public int CurrentPosition;
        public int TotalCount;
    }
}
