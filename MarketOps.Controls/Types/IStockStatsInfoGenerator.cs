using MarketOps.StockData.Types;

namespace MarketOps.Controls.Types
{
    /// <summary>
    /// interface for generating info text on stock data stats
    /// </summary>
    public interface IStockStatsInfoGenerator
    {
        string GetStatsSelectedInfo(StockDisplayData data, int selectedIndex);
        string GetStatHeader(StockStat stat);
    }
}
