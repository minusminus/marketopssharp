using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using NSubstitute;
using MarketOps.System.Extensions;
using MarketOps.System.Interfaces;

namespace MarketOps.System.Tests.Extensions
{
    [TestFixture]
    public class SystemValueCalculatorTests
    {
        private SystemValueCalculator _testObj;
        private IDataLoader _dataLoader;

        const float CashValue = 100;
        const float PriceL = 100;
        const float PriceH = 150;
        const int Vol = 5;
        private readonly DateTime CurrentTS = DateTime.Now.Date;

        [SetUp]
        public void SetUp()
        {
            _testObj = new SystemValueCalculator();
            _dataLoader = Substitute.For<IDataLoader>();
        }

        [Test]
        public void Calc_Empty__Returns0()
        {
            System sys = new System();
            _testObj.Calc(sys, DateTime.Now, _dataLoader).ShouldBe(0);
        }

        [Test]
        public void Calc_CashOnly__ReturnsCash()
        {
            System sys = new System
            {
                Cash = CashValue
            };
            _testObj.Calc(sys, CurrentTS, _dataLoader).ShouldBe(CashValue);
        }

        [Test]
        public void Calc_CurrentClosedPositionLong__ReturnsPositionValue()
        {
            System sys = new System
            {
                Cash = 0
            };
            sys.PositionsClosed.Add(new Position()
            {
                Direction = PositionDir.Long,
                TSOpen = CurrentTS.AddDays(-10),
                TSClose = CurrentTS,
                Open = PriceL,
                Close = PriceH,
                Volume = Vol
            });
            _testObj.Calc(sys, CurrentTS, _dataLoader).ShouldBe((PriceH - PriceL) * Vol);
        }
    }
}
