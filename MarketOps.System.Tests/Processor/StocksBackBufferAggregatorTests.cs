using NUnit.Framework;
using Shouldly;
using MarketOps.System.Processor;
using MarketOps.StockData.Types;
using System;
using System.Collections.Generic;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Extensions;
using System.Linq;

namespace MarketOps.System.Tests.Processor
{
    [TestFixture]
    public class StocksBackBufferAggregatorTests
    {
        private readonly SystemStockDataDefinition Stock1 = new SystemStockDataDefinition() { name = "KGHM", dataRange = StockDataRange.Daily };
        private readonly SystemStockDataDefinition Stock2 = new SystemStockDataDefinition() { name = "PKOBP", dataRange = StockDataRange.Daily };

        [Test]
        public void Calculate_EmptyData__ReturnsEmptyDict()
        {
            StocksBackBufferAggregator.Calculate(new Dictionary<SystemStockDataDefinition, List<StockStat>>()).ShouldBe(
                new Dictionary<SystemStockDataDefinition, int>()
                );
        }

        [Test]
        public void Calculate_OneStock_OneValue__ReturnsThisElementData()
        {
            const int value = 35;

            StockStat stat = new StatSMA("")
                .SetParam(StatSMAParams.Period, new StockStatParamInt() { Value = value });
            Dictionary<SystemStockDataDefinition, List<StockStat>> testData = new Dictionary<SystemStockDataDefinition, List<StockStat>>()
            {
                [Stock1] = new List<StockStat>() { stat }
            };

            StocksBackBufferAggregator.Calculate(testData).ShouldBe(
                new Dictionary<SystemStockDataDefinition, int>()
                {
                    [Stock1] = value
                }
                );
        }

        [Test]
        public void Calculate_OneStock_NoValue__ThrowsException()
        {
            Dictionary<SystemStockDataDefinition, List<StockStat>> testData = new Dictionary<SystemStockDataDefinition, List<StockStat>>()
            {
                [Stock1] = new List<StockStat>()
            };

            Should.Throw<InvalidOperationException>(() => StocksBackBufferAggregator.Calculate(testData));
        }

        [Test]
        public void Calculate_OneStock_TwoValues__ReturnsHigher()
        {
            const int valueLow = 35;
            const int valueHigh = 135;

            StockStat stat = new StatSMA("")
                .SetParam(StatSMAParams.Period, new StockStatParamInt() { Value = valueLow });
            StockStat stat2 = new StatSMA("")
                .SetParam(StatSMAParams.Period, new StockStatParamInt() { Value = valueHigh });
            Dictionary<SystemStockDataDefinition, List<StockStat>> testData = new Dictionary<SystemStockDataDefinition, List<StockStat>>()
            {
                [Stock1] = new List<StockStat>() { stat, stat2 }
            };

            StocksBackBufferAggregator.Calculate(testData).ShouldBe(
                new Dictionary<SystemStockDataDefinition, int>()
                {
                    [Stock1] = valueHigh
                }
                );
        }

        [Test]
        public void Calculate_TwoStocks_ManyValues__ReturnsHigestForEachStock()
        {
            const int valueLow = 35;
            const int valueMid = 82;
            const int valueHigh = 135;

            StockStat stat = new StatSMA("")
                .SetParam(StatSMAParams.Period, new StockStatParamInt() { Value = valueLow });
            StockStat stat2 = new StatSMA("")
                .SetParam(StatSMAParams.Period, new StockStatParamInt() { Value = valueHigh });
            StockStat stat3 = new StatSMA("")
                .SetParam(StatSMAParams.Period, new StockStatParamInt() { Value = valueMid });
            Dictionary<SystemStockDataDefinition, List<StockStat>> testData = new Dictionary<SystemStockDataDefinition, List<StockStat>>()
            {
                [Stock1] = new List<StockStat>() { stat, stat2 },
                [Stock2] = new List<StockStat>() { stat3, stat }
            };

            StocksBackBufferAggregator.Calculate(testData).ShouldBe(
                new Dictionary<SystemStockDataDefinition, int>()
                {
                    [Stock1] = valueHigh,
                    [Stock2] = valueMid,
                }
                );
        }
    }
}
