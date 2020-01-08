using MarketOps.DataProvider.Pg;
using MarketOps.DataProvider.Pg.Bossa;
using MarketOps.StockData.Interfaces;

namespace MarketOps
{
    /// <summary>
    /// Factory providing data access interfaces
    /// </summary>
    internal static class DataProvidersFactory
    {
        public static IStockDataProvider GetStockDataProvider()
        {
            return new PgStockDataProvider();
        }

        public static IDataPumpProvider GetDataPumpProvider()
        {
            DataTableSelector selector = new DataTableSelector();
            return new PgDataProvider(selector);
        }

        public static IDataGenProvider GetDataGenProvider()
        {
            DataTableSelector selector = new DataTableSelector();
            return new PgDataProvider(selector);
        }
    }
}
