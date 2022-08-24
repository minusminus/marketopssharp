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
    public class MMSignalVolumeForAllCashTests
    {
        [TestCase(0, 1, 0, 0)]
        [TestCase(10, 100, 0, 0)]
        [TestCase(-10, 100, 0, 0)]
        [TestCase(0, 1, 0.5f, 0)]
        [TestCase(10, 100, 0.5f, 0)]
        [TestCase(10, 1, 0, 10)]
        [TestCase(-10, 1, 0, 0)]
        [TestCase(-10, 1, 0.5f, 0)]
        [TestCase(10, 1, 0.5f, 9)]
        [TestCase(10, 1, 1.5f, 8)]
        public void Calculate__ReturnsCorrectValue(float cash, float price, float commission, int expectedVolume)
        {
            ICommission commissionCalc = Substitute.For<ICommission>();
            commissionCalc.Calculate(default, default, default).ReturnsForAnyArgs(commission);
            MMSignalVolumeForAllCash testObj = new MMSignalVolumeForAllCash(commissionCalc);

            testObj.Calculate(new SystemState() { Cash = cash }, StockType.Stock, price, 0).ShouldBe(expectedVolume);
        }
    }
}
