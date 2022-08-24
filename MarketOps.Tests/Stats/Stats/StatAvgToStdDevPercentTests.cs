using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Extensions;

namespace MarketOps.Tests.Stats.Stats
{
    [TestFixture]
    public class StatAvgToStdDevPercentTests
    {
        [Test]
        public void Create__HasDefaultValues()
        {
            StatAvgToStdDevPercent testObj = new StatAvgToStdDevPercent("");
            testObj.StatParams.Get(StatAvgToStdDevPercentParams.Range).As<int>().ShouldBeGreaterThan(0);
            testObj.BackBufferLength.ShouldBe(testObj.StatParams.Get(StatAvgToStdDevPercentParams.Range).As<int>() + 1);
        }

        [Test]
        public void Create_ParamsSet__CorrectBackBufferLength()
        {
            const int rangeValue = 125;
            StatAvgToStdDevPercent testObj = new StatAvgToStdDevPercent("");
            testObj.StatParams.Set(StatAvgToStdDevPercentParams.Range, rangeValue);
            testObj.BackBufferLength.ShouldBe(rangeValue + 1);
        }
    }
}
