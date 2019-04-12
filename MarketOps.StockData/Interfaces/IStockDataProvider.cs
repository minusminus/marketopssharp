using System;
using MarketOps.StockData.Types;

namespace MarketOps.StockData.Interfaces
{
    /// <summary>
    /// Interface for external source data provider
    /// </summary>
    public interface IStockDataProvider
    {
        StockDefinition GetStockDefinition(int stockID);
        StockPricesData GetPricesData(StockDefinition stockDef, StockDataRange dataRange, int intradayInterval, DateTime tsFrom, DateTime tsTo);
    }
}
