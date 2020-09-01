using System;
using System.Linq;
using MarketOps.System.MM;
using NUnit.Framework;
using Shouldly;

namespace MarketOps.System.Tests.MM
{
    [TestFixture]
    public class MMCloseCalculatorTicksPassedOnCloseTests
    {
        private void TestCalculateCloseMode(int requiredTicks)
        {
            MMCloseCalculatorTicksPassedOnClose _testObj = new MMCloseCalculatorTicksPassedOnClose(requiredTicks);
            Position pos = new Position() { TicksActive = 1 };
            for (int i = 1; i < requiredTicks; i++)
            {
                _testObj.CalculateCloseMode(pos, DateTime.Now);
                pos.CloseMode.ShouldBe(PositionCloseMode.DontClose, $"required={requiredTicks}, i={i}");
                pos.TicksActive++;
            }
            _testObj.CalculateCloseMode(pos, DateTime.Now);
            pos.CloseMode.ShouldBe(PositionCloseMode.OnClose, $"required={requiredTicks}, last");
        }


        [Test]
        public void CalculateCloseMode__SetsOnCloseAfterTicksPassed()
        {
            TestCalculateCloseMode(10);
        }

        [Test]
        public void CalculateCloseMode_RandomValues__SetsOnCloseAfterTicksPassed()
        {
            Random r = new Random();
            Enumerable.Range(1, 10).ToList()
                .ForEach(_ => TestCalculateCloseMode(r.Next(100)));
        }
    }
}
