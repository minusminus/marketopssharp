using MarketOps.StockData.Types;
using MarketOps.System.Interfaces;
using NSubstitute;
using System;

namespace MarketOps.System.Tests
{
    /// <summary>
    /// Utils for IDataLoader mock.
    /// </summary>
    internal static class DataLoaderUtils
    {
        public static IDataLoader CreateSubstitute(int pricesCount, DateTime lastDate)
        {
            IDataLoader dataLoader = Substitute.For<IDataLoader>();
            StockPricesData pricesData = new StockPricesData(pricesCount);
            for (int i = 0; i < pricesData.Length; i++)
            {
                pricesData.O[i] = i;
                pricesData.H[i] = i;
                pricesData.L[i] = i;
                pricesData.C[i] = i;
                pricesData.TS[i] = lastDate.AddDays(-pricesData.Length + i + 1);
            }
            dataLoader.Get(default, default, default, default, default).ReturnsForAnyArgs(pricesData);

            return dataLoader;
        }
    }
}
