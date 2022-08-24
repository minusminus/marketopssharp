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
    public class MMSignalVolumeByTakenRiskPercentTests
    {
        [TestCase(0.01f, 100f, 1f, 0.1f, 0, 10)]
        [TestCase(0.01f, 100f, 10f, 0.1f, 0, 10)]
        [TestCase(0.01f, 100f, 15f, 0.1f, 0, 10)]
        [TestCase(0.01f, 100f, 1f, 0.1f, 1f, 10)]
        [TestCase(0.01f, 100f, 10f, 0.1f, 1f, 9)]
        [TestCase(0.01f, 100f, 15f, 0.1f, 1f, 9)]
        public void Calculate__ReturnsCorrectValue(float riskedPercent, float cash, float price, float initialRisk, float commission, int expectedVolume)
        {
            ICommission commissionCalc = Substitute.For<ICommission>();
            commissionCalc.Calculate(default, default, default).ReturnsForAnyArgs(commission);
            ISystemDataLoader dataLoader = Substitute.For<ISystemDataLoader>();
            MMSignalVolumeByTakenRiskPercent testObj = new MMSignalVolumeByTakenRiskPercent(riskedPercent, commissionCalc, dataLoader);

            testObj.Calculate(new SystemState() { Cash = cash }, StockType.Stock, price, initialRisk).ShouldBe(expectedVolume);
        }
    }
}
