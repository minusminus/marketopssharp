using NUnit.Framework;
using Shouldly;
using MarketOps.Controls;

namespace MarketOps.Controls.Tests
{
    [TestFixture]
    public class PositionChangeCheckerTests
    {
        [Test]
        public void SetAndCheckChange_NewCoords__ReturnsTrue()
        {
            PositionChangeChecker testObj = new PositionChangeChecker();
            testObj.SetAndCheckChange(100, 100).ShouldBeTrue();
        }

        [Test]
        public void SetAndCheckChange_NewCoordsTwice__ReturnsTrueTwice()
        {
            PositionChangeChecker testObj = new PositionChangeChecker();
            testObj.SetAndCheckChange(100, 100).ShouldBeTrue();
            testObj.SetAndCheckChange(1000, 1000).ShouldBeTrue();
        }

        [Test]
        public void SetAndCheckChange_SameCoords__ReturnsFalse()
        {
            PositionChangeChecker testObj = new PositionChangeChecker();
            testObj.SetAndCheckChange(100, 100).ShouldBeTrue();
            testObj.SetAndCheckChange(100, 100).ShouldBeFalse();
        }

        [Test]
        public void SetAndCheckChange_SameCoordsTwice__ReturnsFalseTwice()
        {
            PositionChangeChecker testObj = new PositionChangeChecker();
            testObj.SetAndCheckChange(100, 100).ShouldBeTrue();
            testObj.SetAndCheckChange(100, 100).ShouldBeFalse();
            testObj.SetAndCheckChange(100, 100).ShouldBeFalse();
        }
    }
}
