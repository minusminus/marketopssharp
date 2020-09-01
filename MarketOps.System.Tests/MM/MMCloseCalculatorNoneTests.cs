using System;
using MarketOps.System.MM;
using NUnit.Framework;
using Shouldly;

namespace MarketOps.System.Tests.MM
{
    [TestFixture]
    public class MMCloseCalculatorNoneTests
    {
        private readonly MMCloseCalculatorNone _testObj = new MMCloseCalculatorNone();

        [Test]
        public void CalculateCloseMode_AllModes__SetsDontClose()
        {
            foreach (PositionCloseMode mode in Enum.GetValues(typeof(PositionCloseMode)))
            {
                Position pos = new Position() { CloseMode = mode };
                _testObj.CalculateCloseMode(pos, DateTime.Now);
                pos.CloseMode.ShouldBe(PositionCloseMode.DontClose, mode.ToString());
            }
        }
    }
}
