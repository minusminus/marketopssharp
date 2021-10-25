using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Extensions;

namespace MarketOps.Tests.Stats.Stats
{
    [TestFixture]
    public class StatRangeChangePcntTests
    {
        [Test]
        public void Create__HasDefaultValues()
        {
            StatRangeChangePcnt testObj = new StatRangeChangePcnt("");
            testObj.StatParams.Get(StatRangeChangePcntParams.Range).As<int>().ShouldBeGreaterThan(0);
            testObj.BackBufferLength.ShouldBe(testObj.StatParams.Get(StatRangeChangePcntParams.Range).As<int>());
        }

        [Test]
        public void Create_ParamsSet__CorrectBackBufferLength()
        {
            const int rangeValue = 125;
            StatRangeChangePcnt testObj = new StatRangeChangePcnt("");
            testObj.StatParams.Set(StatRangeChangePcntParams.Range, rangeValue);
            testObj.BackBufferLength.ShouldBe(rangeValue);
        }
    }
}
