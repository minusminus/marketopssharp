using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;

namespace MarketOps.Stats.Tests.Stats
{
    [TestFixture]
    public class StatBBTests
    {
        [Test]
        public void Create__HasDefaultValues()
        {
            StatBB testObj = new StatBB("");
            testObj.StatParams.Get(StatBBParams.Period).As<int>().ShouldBeGreaterThan(0);
            testObj.StatParams.Get(StatBBParams.SigmaWidth).As<float>().ShouldBeGreaterThan(0);
            testObj.BackBufferLength.ShouldBe(testObj.StatParams.Get(StatBBParams.Period).As<int>());
        }

        [Test]
        public void Create_ParamsSet__CorrectBackBufferLength()
        {
            const int periodValue = 125;
            StatBB testObj = new StatBB("");
            testObj.StatParams.Set(StatBBParams.Period, periodValue);
            testObj.BackBufferLength.ShouldBe(periodValue);
        }
    }
}
