using System;
using NUnit.Framework;
using Shouldly;
using NSubstitute;
using MarketOps.System.Extensions;
using MarketOps.System.Interfaces;
using MarketOps.StockData.Types;

namespace MarketOps.System.Tests.Extensions
{
    [TestFixture]
    public class SystemValueCalculatorTests
    {
        private SystemValueCalculator _testObj;
        private IDataLoader _dataLoader;
        private SystemState _testSys;
        private StockPricesData _stockPrices;

        const float CashValue = 100;
        const float PriceL = 100;
        const float PriceH = 150;
        const int Vol = 5;
        private readonly DateTime CurrentTS = DateTime.Now.Date;

        [SetUp]
        public void SetUp()
        {
            _dataLoader = Substitute.For<IDataLoader>();
            _testSys = new SystemState() { Cash = CashValue };
            _stockPrices = new StockPricesData(1);
            _testObj = new SystemValueCalculator();

            _stockPrices.TS[0] = CurrentTS;
            _stockPrices.C[0] = PriceL;
            _dataLoader
                .Get(Arg.Compat.Any<string>(), Arg.Compat.Any<StockDataRange>(), Arg.Compat.Any<int>(), Arg.Compat.Any<DateTime>(), Arg.Compat.Any<DateTime>())
                .Returns<StockPricesData>(_stockPrices);
        }

        [Test]
        public void Calc_Empty__Returns0()
        {
            _testObj.Calc(new SystemState(), DateTime.Now, _dataLoader).ShouldBe(0);
        }

        [Test]
        public void Calc_CashOnly__ReturnsCash()
        {
            _testObj.Calc(_testSys, CurrentTS, _dataLoader).ShouldBe(CashValue);
        }

        [Test]
        public void Calc_WithClosedPosition__ClosedDoesNotCount()
        {
            _testSys.PositionsClosed.Add(new Position()
            {
                Direction = PositionDir.Long,
                TSOpen = CurrentTS.AddDays(-10),
                TSClose = CurrentTS,
                Open = PriceL,
                Close = PriceH,
                Volume = Vol
            });
            _testObj.Calc(_testSys, CurrentTS, _dataLoader).ShouldBe(CashValue);
        }

        [Test]
        public void Calc_WithActivePosition__ReturnsCashAndPositionValue()
        {
            _testSys.PositionsActive.Add(new Position()
            {
                Stock = new StockDefinition(),
                Direction = PositionDir.Long,
                Volume = Vol
            });
            _testObj.Calc(_testSys, CurrentTS, _dataLoader).ShouldBe(CashValue + PriceL * Vol);
        }

        [Test]
        public void Calc_WithTwoActivePositions__ReturnsCashAndPositionsValue()
        {
            const int posCount = 2;
            for (int i = 0; i < posCount; i++)
                _testSys.PositionsActive.Add(new Position()
                {
                    Stock = new StockDefinition(),
                    Direction = PositionDir.Long,
                    Volume = Vol
                });
            _testObj.Calc(_testSys, CurrentTS, _dataLoader).ShouldBe(CashValue + (PriceL * Vol) * posCount);
        }
    }
}
