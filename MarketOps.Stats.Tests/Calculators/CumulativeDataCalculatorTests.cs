using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Calculators;

namespace MarketOps.Stats.Tests.Calculators
{
    internal class CumulativeDataCalculatorMock : CumulativeDataCalculator
    {
        public float[] TestCalculateCumulative(int dataLength, int period, Func<int, float> getValue) => CalculateCumulative(dataLength, period, getValue);
    }

    [TestFixture]
    public class CumulativeDataCalculatorTests
    {
        private readonly CumulativeDataCalculatorMock _testObj = new CumulativeDataCalculatorMock();
        private readonly float[] _testData = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

        [TestCase(4, new float[] { 2.5f, 3.5f, 4.5f, 5.5f, 6.5f, 7.5f, 8.5f })]
        [TestCase(1, new float[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
        public void CalculateCumulative__CalculatesCorrectly(int period, float[] expected)
        {
            _testObj.TestCalculateCumulative(_testData.Length, period, i => _testData[i]).ShouldBe(expected);
        }
    }
}
