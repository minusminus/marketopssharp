using System;
using System.Collections.Generic;
using MarketOps.StockData.Types;

namespace MarketOps.StockData.Interfaces
{
    /// <summary>
    /// Interface for stock data database provider
    /// </summary>
    public interface IStockDataProvider
    {
        List<StockDefinition> GetAllStockDefinitions();
        StockDefinition GetStockDefinition(int stockID);
        StockDefinition GetStockDefinition(string stockName);
        StockPricesData GetPricesData(StockDefinition stockDef, StockDataRange dataRange, int intradayInterval, DateTime tsFrom, DateTime tsTo);
        DateTime GetNearestTickGE(StockDefinition stockDef, StockDataRange dataRange, int intradayInterval, DateTime ts);
        DateTime GetNearestTickGETicksBefore(StockDefinition stockDef, StockDataRange dataRange, int intradayInterval, DateTime ts, int ticksBefore);
    }
}
