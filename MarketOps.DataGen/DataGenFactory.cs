using MarketOps.DataGen.DataGenerators;
using MarketOps.StockData.Interfaces;

namespace MarketOps.DataGen
{
    /// <summary>
    /// Factory creating specified data generation mechanism
    /// </summary>
    public static class DataGenFactory
    {
        public static IDataGen Get(IDataGenProvider dataGenProvider) => new DataAggregator(dataGenProvider);
    }
}
