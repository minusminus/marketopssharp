using MarketOps.DataGen.DataGenerators;
using MarketOps.StockData.Interfaces;

namespace MarketOps.DataGen
{
    /// <summary>
    /// Factory creating specified data generation mechanism
    /// </summary>
    public class DataGenFactory
    {
        public static IDataGen Get(IDataGenProvider dataGenProvider)
        {
            return new DataAggregator(dataGenProvider);
        }
    }
}
