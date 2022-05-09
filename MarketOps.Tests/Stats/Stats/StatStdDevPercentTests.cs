using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Extensions;

namespace MarketOps.Tests.Stats.Stats
{
    [TestFixture]
    public class StatStdDevPercentTests
    {
        [Test]
        public void Create__HasDefaultValues()
        {
            StatStdDevPercent testObj = new StatStdDevPercent("");
            testObj.StatParams.Get(StatStdDevPercentParams.Range).As<int>().ShouldBeGreaterThan(0);
            testObj.BackBufferLength.ShouldBe(testObj.StatParams.Get(StatStdDevPercentParams.Range).As<int>() + 1);
        }

        [Test]
        public void Create_ParamsSet__CorrectBackBufferLength()
        {
            const int rangeValue = 125;
            StatStdDevPercent testObj = new StatStdDevPercent("");
            testObj.StatParams.Set(StatStdDevPercentParams.Range, rangeValue);
            testObj.BackBufferLength.ShouldBe(rangeValue + 1);
        }
    }
}
