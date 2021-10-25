using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Extensions;

namespace MarketOps.Tests.Stats.Stats
{
    [TestFixture]
    public class StatATRTests
    {
        [Test]
        public void Create__HasDefaultValues()
        {
            StatATR testObj = new StatATR("");
            testObj.StatParams.Get(StatATRParams.Period).As<int>().ShouldBeGreaterThan(0);
            testObj.BackBufferLength.ShouldBe(testObj.StatParams.Get(StatSMAParams.Period).As<int>());
        }

        [Test]
        public void Create_ParamsSet__CorrectBackBufferLength()
        {
            const int periodValue = 125;
            StatATR testObj = new StatATR("");
            testObj.StatParams.Set(StatATRParams.Period, periodValue);
            testObj.BackBufferLength.ShouldBe(periodValue);
        }
    }
}
