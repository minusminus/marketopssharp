using MarketOps.StockData.Interfaces;
using NSubstitute;
using System;

namespace MarketOps.SystemExecutor.Tests.Mocks
{
    /// <summary>
    /// Utils for IStockDataProvider mock.
    /// </summary>
    public static class StockDataProviderUtils
    {
        public static IStockDataProvider CreateSubstitute(DateTime nearestTickGETicksBefore)
        {
            IStockDataProvider dataProvider = Substitute.For<IStockDataProvider>();
            dataProvider.GetNearestTickGETicksBefore(default, default, default, default, default).ReturnsForAnyArgs(nearestTickGETicksBefore);
            return dataProvider;
        }
    }
}
