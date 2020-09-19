using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;

namespace MarketOps.Stats.Tests.Stats
{
    [TestFixture]
    public class StatSMATests
    {
        [Test]
        public void Create__HasDefaultValues()
        {
            StatSMA testObj = new StatSMA("");
            testObj.StatParams.Get(StatSMAParams.Period).As<int>().ShouldBeGreaterThan(0);
            testObj.BackBufferLength.ShouldBe(testObj.StatParams.Get(StatSMAParams.Period).As<int>());
        }

        [Test]
        public void Create_ParamsSet__CorrectBackBufferLength()
        {
            const int periodValue = 125;
            StatSMA testObj = new StatSMA("");
            testObj.StatParams.Set(StatSMAParams.Period, periodValue);
            testObj.BackBufferLength.ShouldBe(periodValue);
        }
    }
}
