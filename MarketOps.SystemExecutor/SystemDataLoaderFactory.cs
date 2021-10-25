using MarketOps.StockData.Interfaces;
using MarketOps.SystemExecutor.DataLoaders;
using MarketOps.SystemData.Interfaces;

namespace MarketOps.SystemExecutor
{
    /// <summary>
    /// Factory providing system data loader interfaces.
    /// </summary>
    public static class SystemDataLoaderFactory
    {
        public static ISystemDataLoader Get(IStockDataProvider dataProvider)
        {
            return new BufferedDataLoader(dataProvider);
        }
    }
}
