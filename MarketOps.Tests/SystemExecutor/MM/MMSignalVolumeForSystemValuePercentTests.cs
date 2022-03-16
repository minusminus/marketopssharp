using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using MarketOps.SystemExecutor.MM;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace MarketOps.Tests.SystemExecutor.MM
{
    [TestFixture]
    public class MMSignalVolumeForSystemValuePercentTests
    {
        [TestCase(0.1f, 100f, 1f, 0, 10)]
        [TestCase(0.1f, 100f, 10f, 0, 1)]
        [TestCase(0.1f, 100f, 15f, 0, 0)]
        [TestCase(0.1f, 100f, 1f, 1f, 10)]
        [TestCase(0.1f, 100f, 10f, 1f, 1)]
        [TestCase(0.1f, 100f, 15f, 1f, 0)]
        public void Calculate__ReturnsCorrectValue(float percentOfValue, float cash, float price, float commission, int expectedVolume)
        {
            ICommission commissionCalc = Substitute.For<ICommission>();
            commissionCalc.Calculate(default, default, default).ReturnsForAnyArgs(commission);
            ISystemDataLoader dataLoader = Substitute.For<ISystemDataLoader>();
            MMSignalVolumeForSystemValuePercent testObj = new MMSignalVolumeForSystemValuePercent(percentOfValue, commissionCalc, dataLoader);

            testObj.Calculate(new SystemState() { Cash = cash }, StockType.Stock, price).ShouldBe(expectedVolume);
        }
    }
}
