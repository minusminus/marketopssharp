﻿using System;
using MarketOps.StockData.Types;

namespace MarketOps.System.Interfaces
{
    /// <summary>
    /// Interface for stock data loading.
    /// </summary>
    internal interface IDataLoader
    {
        StockPricesData Get(string stockName, StockDataRange dataRange, int intradayInterval, DateTime tsFrom, DateTime tsTo);
    }
}