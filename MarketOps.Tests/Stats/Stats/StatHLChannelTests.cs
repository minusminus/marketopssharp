using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Extensions;

namespace MarketOps.Tests.Stats.Stats
{
    [TestFixture]
    public class StatHLChannelTests
    {
        [Test]
        public void Create__HasDefaultValues()
        {
            StatHLChannel testObj = new StatHLChannel("");
            testObj.StatParams.Get(StatHLChannelParams.Period).As<int>().ShouldBeGreaterThan(0);
            testObj.BackBufferLength.ShouldBe(testObj.StatParams.Get(StatHLChannelParams.Period).As<int>());
        }

        [Test]
        public void Create_ParamsSet__CorrectBackBufferLength()
        {
            const int periodValue = 125;
            StatHLChannel testObj = new StatHLChannel("");
            testObj.StatParams.Set(StatHLChannelParams.Period, periodValue);
            testObj.BackBufferLength.ShouldBe(periodValue);
        }
    }
}
