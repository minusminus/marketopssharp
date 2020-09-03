﻿using MarketOps.StockData.Types;
using MarketOps.System.Interfaces;
using NSubstitute;
using System;

namespace MarketOps.System.Tests.Mocks
{
    /// <summary>
    /// Utils for IDataLoader mock.
    /// </summary>
    internal static class DataLoaderUtils
    {
        public static IDataLoader CreateSubstitute(StockPricesData pricesData)
        {
            IDataLoader dataLoader = Substitute.For<IDataLoader>();
            dataLoader.Get(default, default, default, default, default).ReturnsForAnyArgs(pricesData);
            return dataLoader;
        }

        public static IDataLoader CreateSubstitute(int pricesCount, DateTime lastDate)
        {
            return CreateSubstituteWithStartingPrice(pricesCount, 0, lastDate);
        }

        public static IDataLoader CreateSubstituteWithStartingPrice(int pricesCount, float startingPrice, DateTime lastDate)
        {
            StockPricesData pricesData = new StockPricesData(pricesCount);
            for (int i = 0; i < pricesData.Length; i++)
            {
                pricesData.O[i] = startingPrice + i;
                pricesData.H[i] = startingPrice + i;
                pricesData.L[i] = startingPrice + i;
                pricesData.C[i] = startingPrice + i;
                pricesData.TS[i] = lastDate.AddDays(-pricesData.Length + i + 1);
            }
            return CreateSubstitute(pricesData);
        }

        public static IDataLoader CreateSubstituteWithConstantPrice(int pricesCount, float price, DateTime lastDate)
        {
            StockPricesData pricesData = new StockPricesData(pricesCount);
            for (int i = 0; i < pricesData.Length; i++)
            {
                pricesData.O[i] = price;
                pricesData.H[i] = price;
                pricesData.L[i] = price;
                pricesData.C[i] = price;
                pricesData.TS[i] = lastDate.AddDays(-pricesData.Length + i + 1);
            }
            return CreateSubstitute(pricesData);
        }

        public static IDataLoader CreateSubstituteWithConstantPriceInRange(int pricesCount, float price, float priceRange, DateTime lastDate)
        {
            StockPricesData pricesData = new StockPricesData(pricesCount);
            for (int i = 0; i < pricesData.Length; i++)
            {
                pricesData.O[i] = price;
                pricesData.H[i] = price + priceRange;
                pricesData.L[i] = price - priceRange;
                pricesData.C[i] = price;
                pricesData.TS[i] = lastDate.AddDays(-pricesData.Length + i + 1);
            }
            return CreateSubstitute(pricesData);
        }

        public static IDataLoader CreateSubstitute(int pricesCount, float price, DateTime ts)
        {
            StockPricesData pricesData = new StockPricesData(pricesCount);
            for (int i = 0; i < pricesData.Length; i++)
            {
                pricesData.O[i] = price;
                pricesData.H[i] = price;
                pricesData.L[i] = price;
                pricesData.C[i] = price;
                pricesData.TS[i] = ts;
            }
            return CreateSubstitute(pricesData);
        }
    }
}