﻿using MarketOps.StockData.Types;

namespace MarketOps.Tests.SystemExecutor.Mocks
{
    /// <summary>
    /// Utils for StockPricesData.
    /// </summary>
    public static class StockPricesDataUtils
    {
        public static StockPricesData CreatePricesData(float o, float h, float l, float c)
        {
            StockPricesData res = new StockPricesData(1);
            res.O[0] = o;
            res.H[0] = h;
            res.L[0] = l;
            res.C[0] = c;
            return res;
        }
    }
}
