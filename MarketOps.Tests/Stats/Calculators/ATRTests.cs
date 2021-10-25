using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Calculators;

namespace MarketOps.Tests.Stats.Calculators
{
    [TestFixture]
    public class ATRTests
    {
        private readonly ATR _testObj = new ATR();

        [TestCase(new float[] { 0, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new float[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 0 },
            new float[] { 0, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, new float[] { 2, 2, 2, 2, 2, 2 }, Description = "C in middle up")]
        [TestCase(new float[] { 0, 10, 9, 8, 7, 6, 5, 4, 3, 2 }, new float[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 0 },
            new float[] { 0, 12, 11, 10, 9, 8, 7, 6, 5, 4 }, new float[] { 2, 2, 2, 2, 2, 2 }, Description = "C in middle down")]
        [TestCase(new float[] { 0, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new float[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 },
            new float[] { 0, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, new float[] { 3, 3, 3, 3, 3, 3 }, Description = "C below")]
        [TestCase(new float[] { 0, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new float[] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 0 },
            new float[] { 0, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, new float[] { 3, 3, 3, 3, 3, 3 }, Description = "C above")]
        public void Calculate__CalculatesCorrectly(float[] dataL, float[] dataC, float[] dataH, float[] expected)
        {
            _testObj.Calculate(dataH, dataL, dataC, 4).ShouldBe(expected);
        }


        [TestCase(new float[] { 1, 2 }, new float[] { 1 }, new float[] { 1, 2 })]
        [TestCase(new float[] { 1, 2 }, new float[] { 1, 2 }, new float[] { 1 })]
        [TestCase(new float[] { 1 }, new float[] { 1, 2 }, new float[] { 1, 2 })]
        public void Calculate_TooShortData__EmptyResult(float[] dataL, float[] dataC, float[] dataH)
        {
            _testObj.Calculate(dataH, dataL, dataC, 4).ShouldBeEmpty();
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(5)]
        [TestCase(6)]
        public void Calculate_IncorrectPeriod__EmptyResult(int period)
        {
            float[] data = { 1, 2, 3, 4, 5 };
            _testObj.Calculate(data, data, data, period).ShouldBeEmpty();
        }
    }
}
