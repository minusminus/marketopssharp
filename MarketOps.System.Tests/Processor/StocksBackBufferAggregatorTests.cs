using NUnit.Framework;
using Shouldly;
using MarketOps.System.Processor;
using MarketOps.System.Interfaces;
using MarketOps.StockData.Types;
using System;
using System.Collections.Generic;
using MarketOps.Stats.Stats;

namespace MarketOps.System.Tests.Processor
{
    [TestFixture]
    public class StocksBackBufferAggregatorTests
    {
        private readonly string Stock1 = "KGHM";

        [Test]
        public void Calculate_EmptyData__ReturnsEmptyDict()
        {
            StocksBackBufferAggregator.Calculate(new Dictionary<string, StockStat>()).ShouldBe(
                new Dictionary<string, int>() { }
                );
        }

        [Test]
        public void Calculate_OneStock_OneValue__ReturnsThisElementData()
        {
            const int value = 35;

            StatSMA stat = new StatSMA("");
            stat.StatParams.Set(StatSMAParams.Period, new StockStatParamInt() { Value = value });
            Dictionary<string, StockStat> testData = new Dictionary<string, StockStat>()
            {
                { Stock1, stat }
            };

            StocksBackBufferAggregator.Calculate(testData).ShouldBe(
                new Dictionary<string, int>()
                {
                    {Stock1, value}
                }
                );
        }

        [Test]
        public void Calculate_OneStock_TwoValues__ReturnsHigher()
        {
            const int valueLow = 35;
            const int valueHigh = 135;

            StatSMA stat = new StatSMA("");
            stat.StatParams.Set(StatSMAParams.Period, new StockStatParamInt() { Value = valueLow });
            StatSMA stat2 = new StatSMA("");
            stat2.StatParams.Set(StatSMAParams.Period, new StockStatParamInt() { Value = valueHigh });
            Dictionary<string, StockStat> testData = new Dictionary<string, StockStat>()
            {
                { Stock1, stat },
                { Stock1, stat2 }
            };

            StocksBackBufferAggregator.Calculate(testData).ShouldBe(
                new Dictionary<string, int>()
                {
                    {Stock1, valueHigh}
                }
                );
        }
    }
}
