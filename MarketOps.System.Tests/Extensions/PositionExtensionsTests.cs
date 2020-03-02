using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.System.Extensions;

namespace MarketOps.System.Tests.Extensions
{
    [TestFixture]
    public class PositionExtensionsTests
    {
        private Position _testObj;

        [SetUp]
        public void SetUp()
        {
            _testObj = new Position();
        }

        [TestCase(0, 0, 0)]
        [TestCase(10, 0, 0)]
        [TestCase(0, 10, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(10, 15, 150)]
        public void OpenValue(float open, int volume, float expected)
        {
            _testObj.Open = open;
            _testObj.Volume = volume;
            _testObj.OpenValue().ShouldBe(expected);
        }

        [TestCase(0, 0, 0)]
        [TestCase(10, 0, 0)]
        [TestCase(0, 10, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(10, 15, 150)]
        public void CloseValue(float close, int volume, float expected)
        {
            _testObj.Close = close;
            _testObj.Volume = volume;
            _testObj.CloseValue().ShouldBe(expected);
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(1, 0, 0, 0)]
        [TestCase(0, 1, 0, 0)]
        [TestCase(0, 0, 1, 0)]
        [TestCase(1, 0, 1, -1)]
        [TestCase(0, 1, 1, 1)]
        [TestCase(1, 1, 1, 0)]
        [TestCase(1, 0, 10, -10)]
        [TestCase(0, 1, 10, 10)]
        [TestCase(1, 1, 10, 0)]
        public void Value_Long(float open, float close, int volume, float expected)
        {
            _testObj.Direction = PositionDir.Long;
            _testObj.Open = open;
            _testObj.Close = close;
            _testObj.Volume = volume;
            _testObj.Value().ShouldBe(expected);
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(1, 0, 0, 0)]
        [TestCase(0, 1, 0, 0)]
        [TestCase(0, 0, 1, 0)]
        [TestCase(1, 0, 1, 1)]
        [TestCase(0, 1, 1, -1)]
        [TestCase(1, 1, 1, 0)]
        [TestCase(1, 0, 10, 10)]
        [TestCase(0, 1, 10, -10)]
        [TestCase(1, 1, 10, 0)]
        public void Value_Short(float open, float close, int volume, float expected)
        {
            _testObj.Direction = PositionDir.Short;
            _testObj.Open = open;
            _testObj.Close = close;
            _testObj.Volume = volume;
            _testObj.Value().ShouldBe(expected);
        }
    }
}
