using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using MarketOps.SystemExecutor.Interfaces;
using System;

namespace MarketOps.SystemExecutor.Processor
{
    /// <summary>
    /// Finds system processing range for stock data.
    /// </summary>
    internal static class PricesDataRangeFinder
    {
        public static (int from, int to) Find(StockPricesData leadingPricesData, DateTime tsFrom, DateTime tsTo)
        {
            int ixFrom = leadingPricesData.FindByTSGE(tsFrom);
            int ixTo = leadingPricesData.FindByTSLE(tsTo);
            if ((ixFrom < 0) || (ixTo < 0)) throw new Exception("Configured dates out of range.");
            return (ixFrom, ixTo);
        }
    }
}
