using NUnit.Framework;
using Shouldly;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Types;

namespace MarketOps.Tests.SystemData.Extensions
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

        [TestCase(PositionDir.Long, 1)]
        [TestCase(PositionDir.Short, -1)]
        public void DirectionMultiplier__ReturnsCorrectly(PositionDir dir, float expected)
        {
            _testObj.Direction = dir;

            _testObj.DirectionMultiplier().ShouldBe(expected);
        }

        [TestCase(PositionDir.Long, PositionDir.Short)]
        [TestCase(PositionDir.Short, PositionDir.Long)]
        public void ReversedDirection__ReturnsCorrectly(PositionDir dir, PositionDir expected)
        {
            _testObj.Direction = dir;

            _testObj.ReversedDirection().ShouldBe(expected);
        }

        [TestCase(0, 0, 0)]
        [TestCase(10, 0, 0)]
        [TestCase(0, 10, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(10, 15, 150)]
        public void OpenValue__ReturnsCorrectly(float open, int volume, float expected)
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
        public void CloseValue__ReturnsCorrectly(float close, int volume, float expected)
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
        public void Value_Long__ReturnsCorrectly(float open, float close, int volume, float expected)
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
        public void Value_Short__ReturnsCorrectly(float open, float close, int volume, float expected)
        {
            _testObj.Direction = PositionDir.Short;
            _testObj.Open = open;
            _testObj.Close = close;
            _testObj.Volume = volume;

            _testObj.Value().ShouldBe(expected);
        }

        [TestCase(10, 9, PositionDir.Long, 1)]
        [TestCase(10, 11, PositionDir.Short, 1)]
        public void RValue_ExitOnStopHit__ReturnsCorrectly(float open, float initialStop, PositionDir dir, float expected)
        {
            _testObj.EntrySignal = new Signal() { InitialStopValue = initialStop };
            _testObj.CloseMode = PositionCloseMode.OnStopHit;
            _testObj.Open = open;
            _testObj.Direction = dir;

            _testObj.RValue().ShouldBe(expected);
        }

        [Test]
        public void RValue_NoSignal__ReturnsZero([Values(PositionDir.Long, PositionDir.Short)] PositionDir dir)
        {
            _testObj.CloseMode = PositionCloseMode.OnStopHit;
            _testObj.Open = 10;
            _testObj.Direction = dir;

            _testObj.RValue().ShouldBe(0);
        }

        [Test]
        public void RValue_ExitNotOnStoHit__ReturnsZero([Values(PositionDir.Long, PositionDir.Short)] PositionDir dir, 
            [Values(PositionCloseMode.DontClose, PositionCloseMode.OnClose, PositionCloseMode.OnOpen)] PositionCloseMode closeMode)
        {
            _testObj.EntrySignal = new Signal();
            _testObj.CloseMode = closeMode;
            _testObj.Open = 10;
            _testObj.Direction = dir;

            _testObj.RValue().ShouldBe(0);
        }

        [TestCase(10, 9, 12, PositionDir.Long, 2)]
        [TestCase(10, 9, 10, PositionDir.Long, 0)]
        [TestCase(10, 9, 9, PositionDir.Long, -1)]
        [TestCase(10, 9, 8, PositionDir.Long, -2)]
        [TestCase(10, 11, 8, PositionDir.Short, 2)]
        [TestCase(10, 11, 10, PositionDir.Short, 0)]
        [TestCase(10, 11, 11, PositionDir.Short, -1)]
        [TestCase(10, 11, 12, PositionDir.Short, -2)]
        public void CalculateRProfit__ReturnsCorrectlty(float open, float initialStop, float close, PositionDir dir, float expected)
        {
            _testObj.EntrySignal = new Signal() { InitialStopValue = initialStop };
            _testObj.CloseMode = PositionCloseMode.OnStopHit;
            _testObj.Open = open;
            _testObj.Close = close;
            _testObj.Direction = dir;

            _testObj.CalculateRProfit().ShouldBe(expected);
        }

        [Test]
        public void CalculateRProfit_NoSignal__ReturnsZero([Values(PositionDir.Long, PositionDir.Short)] PositionDir dir)
        {
            _testObj.CloseMode = PositionCloseMode.OnStopHit;
            _testObj.Open = 10;
            _testObj.Close = 12;
            _testObj.Direction = dir;

            _testObj.CalculateRProfit().ShouldBe(0);
        }

        [Test]
        public void CalculateRProfit_ExitNotOnStopHit__ReturnsZero([Values(PositionDir.Long, PositionDir.Short)] PositionDir dir,
            [Values(PositionCloseMode.DontClose, PositionCloseMode.OnClose, PositionCloseMode.OnOpen)] PositionCloseMode closeMode)
        {
            _testObj.EntrySignal = new Signal();
            _testObj.CloseMode = closeMode;
            _testObj.Open = 10;
            _testObj.Close = 12;
            _testObj.Direction = dir;

            _testObj.CalculateRProfit().ShouldBe(0);
        }
    }
}
