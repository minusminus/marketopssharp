using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Calculators;

namespace MarketOps.Tests.Stats.Calculators
{
    [TestFixture]
    public class RangeChangePcntTests
    {
        private readonly RangeChangePcnt _testObj = new RangeChangePcnt();

        [TestCase(new float[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new float[] { 300f, 150f, 100f, 75f, 60f, 50f, 42.85714f }, 4)]
        [TestCase(new float[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }, new float[] { -30f, -33.33333f, -37.5f, -42.85714f, -50f, -60f, -75f }, 4)]
        [TestCase(new float[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, new float[] { 0f, 0f, 0f, 0f, 0f, 0f, 0f }, 4)]
        [TestCase(new float[] { 1, 2, 3, 4 }, new float[] { 0, 0, 0, 0 }, 1)]
        public void Calculate__CalculatesCorrectly(float[] data, float[] expected, int range)
        {
            _testObj.Calculate(data, range).ShouldBe(expected, 0.0001f);
        }

        [TestCase(new float[] { 1, 2, 3 })]
        [TestCase(new float[] { })]
        public void Calculate_TooShortData__EmptyResult(float[] data)
        {
            _testObj.Calculate(data, 4).ShouldBeEmpty();
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(5)]
        public void Calculate_IncorrectPeriod__EmptyResult(int range)
        {
            _testObj.Calculate(new float[] { 1, 2, 3, 4 }, range).ShouldBeEmpty();
        }
    }
}
