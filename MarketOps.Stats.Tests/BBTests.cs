using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Calculators;

namespace MarketOps.Stats.Tests
{
    [TestFixture]
    public class BBTests
    {
        private readonly BB _testObj = new BB();

        private void TestBB(float[] data, BBData expected, int period, float sigmaWidth)
        {
            BBData res = _testObj.Calculate(data, period, sigmaWidth);

            res.SMA.ShouldBe(expected.SMA, 0.0001f);
            res.BBL.ShouldBe(expected.BBL, 0.0001f);
            res.BBH.ShouldBe(expected.BBH, 0.0001f);
        }

        private void TestBBEmpty(float[] data, int period, float sigmaWidth)
        {
            BBData res = _testObj.Calculate(data, period, sigmaWidth);
            res.SMA.ShouldBeEmpty();
            res.BBL.ShouldBeEmpty();
            res.BBH.ShouldBeEmpty();
        }

        [Test]
        public void Calculate__CalculatesCorrectly()
        {
            TestBB(new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, new BBData()
            {
                SMA = new float[] {2.5f, 3.5f, 4.5f, 5.5f, 6.5f, 7.5f, 8.5f},
                BBL = new float[] {0.263932f, 1.263932f, 2.263932f, 3.263932f, 4.263932f, 5.263932f, 6.263932f},
                BBH = new float[] {4.736068f, 5.736068f, 6.736068f, 7.736068f, 8.736068f, 9.736068f, 10.73607f}
            }, 4, 2f);

            TestBB(new float[] {10, 9, 8, 7, 6, 5, 4, 3, 2, 1}, new BBData()
            {
                SMA = new float[] {8.5f, 7.5f, 6.5f, 5.5f, 4.5f, 3.5f, 2.5f},
                BBL = new float[] {6.263932f, 5.263932f, 4.263932f, 3.263932f, 2.263932f, 1.263932f, 0.263932f},
                BBH = new float[] {10.73607f, 9.736068f, 8.736068f, 7.736068f, 6.736068f, 5.736068f, 4.736068f}
            }, 4, 2f);
        }

        [TestCase(new float[] {1, 2, 3})]
        [TestCase(new float[] {})]
        public void Calculate_TooShortData__EmptyResult(float[] data)
        {
            TestBBEmpty(data, 4, 2f);
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(11)]
        public void Calculate_IncorrectPeriod__EmptyResult(int period)
        {
            TestBBEmpty(new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, period, 2f);
        }

        [TestCase(0f)]
        [TestCase(-1f)]
        public void Calculate_IncorrectSigmaWidth__EmptyResult(float sigmaWidth)
        {
            TestBBEmpty(new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, 4, sigmaWidth);
        }
    }
}
