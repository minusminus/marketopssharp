﻿using NUnit.Framework;
using Shouldly;
using MarketOps.SystemExecutor.Processor;
using MarketOps.StockData.Types;
using System.Collections.Generic;
using MarketOps.Tests.SystemExecutor.Mocks;
using MarketOps.SystemData.Types;

namespace MarketOps.Tests.SystemExecutor.Processor
{
    [TestFixture]
    public class StocksBackBufferAggregatorTests
    {
        private SystemStockDataDefinition Stock1() => new SystemStockDataDefinition()
        {
            stock = new StockDefinition() { FullName = "KGHM" },
            dataRange = StockDataRange.Daily,
            stats = new List<StockStat>()
        };
        private SystemStockDataDefinition Stock2() => new SystemStockDataDefinition()
        {
            stock = new StockDefinition() { FullName = "PKOBP" },
            dataRange = StockDataRange.Daily,
            stats = new List<StockStat>()
        };


        [Test]
        public void Calculate_EmptyData__ReturnsEmptyDict()
        {
            StocksBackBufferAggregator.Calculate(new List<SystemStockDataDefinition>()).ShouldBe(
                new List<(SystemStockDataDefinition, int)>()
                );
        }

        [Test]
        public void Calculate_OneStock_OneValue__ReturnsThisElementData()
        {
            const int value = 35;

            StockStat stat = new StockStatMock("", value);
            SystemStockDataDefinition stock1 = Stock1();
            stock1.stats.Add(stat);
            List<SystemStockDataDefinition> testData = new List<SystemStockDataDefinition>() { stock1 };

            StocksBackBufferAggregator.Calculate(testData).ShouldBe(
                new List<(SystemStockDataDefinition, int)>()
                {
                    (stock1, value)
                }
                );
        }

        [Test]
        public void Calculate_OneStock_NoValue__ReturnsZero()
        {
            SystemStockDataDefinition stock1 = Stock1();
            List<SystemStockDataDefinition> testData = new List<SystemStockDataDefinition>() { stock1 };

            //Should.Throw<InvalidOperationException>(() => StocksBackBufferAggregator.Calculate(testData));
            StocksBackBufferAggregator.Calculate(testData).ShouldBe(
                new List<(SystemStockDataDefinition, int)>()
                {
                    (stock1, 0)
                }
                );
        }

        [Test]
        public void Calculate_OneStock_TwoValues__ReturnsHigher()
        {
            const int valueLow = 35;
            const int valueHigh = 135;

            StockStat stat = new StockStatMock("", valueLow);
            StockStat stat2 = new StockStatMock("", valueHigh);
            SystemStockDataDefinition stock1 = Stock1();
            stock1.stats.Add(stat);
            stock1.stats.Add(stat2);
            List<SystemStockDataDefinition> testData = new List<SystemStockDataDefinition>() { stock1 };

            StocksBackBufferAggregator.Calculate(testData).ShouldBe(
                new List<(SystemStockDataDefinition, int)>()
                {
                    (stock1, valueHigh)
                }
                );
        }

        [Test]
        public void Calculate_TwoStocks_ManyValues__ReturnsHigestForEachStock()
        {
            const int valueLow = 35;
            const int valueMid = 82;
            const int valueHigh = 135;

            StockStat stat = new StockStatMock("", valueLow);
            StockStat stat2 = new StockStatMock("", valueHigh);
            StockStat stat3 = new StockStatMock("", valueMid);
            SystemStockDataDefinition stock1 = Stock1();
            stock1.stats.Add(stat);
            stock1.stats.Add(stat2);
            SystemStockDataDefinition stock2 = Stock2();
            stock2.stats.Add(stat3);
            stock2.stats.Add(stat);
            List<SystemStockDataDefinition> testData = new List<SystemStockDataDefinition>() { stock1, stock2 };

            StocksBackBufferAggregator.Calculate(testData).ShouldBe(
                new List<(SystemStockDataDefinition, int)>()
                {
                    (stock1, valueHigh),
                    (stock2, valueMid),
                }
                );
        }
    }
}
