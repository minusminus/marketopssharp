using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Calculators;

namespace MarketOps.Tests.Stats.Calculators
{
    [TestFixture]
    public class StdDevPercentTests
    {
        [TestCase(new float[] { 1, 2, 3, 4 }, 2, new float[] { 25f, 8.3333f })]
        [TestCase(new float[] { 1, 2, 3, 4 }, 3, new float[] { 28.3278f })]
        [TestCase(new float[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 4, new float[] { 0f, 0f, 0f, 0f, 0f, 0f })]
        [TestCase(new float[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 4, new float[] { 29.0921f, 11.3880f, 6.2777f, 4.0208f, 2.8088f, 2.0780f })]
        [TestCase(new float[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }, 4, new float[] { 1.6017f, 2.0780f, 2.8088f, 4.0208f, 6.2777f, 11.3880f })]
        public void Calculate__CalculatesCorrectly(float[] data, int range, float[] expected)
        {
            StdDevPercent.Calculate(data, range).ShouldBe(expected, 0.0001f);
        }

        [TestCase(new float[] { 1, 2, 3, 4 })]
        [TestCase(new float[] { 1, 2, 3 })]
        [TestCase(new float[] { })]
        public void Calculate_TooShortData__EmptyResult(float[] data)
        {
            StdDevPercent.Calculate(data, 4).ShouldBeEmpty();
        }

        [Test]
        public void Calculate_IncorrectPeriod__EmptyResult([Values(-1, 0, 1, 5)] int range)
        {
            StdDevPercent.Calculate(new float[] { 1, 2, 3, 4, 5 }, range).ShouldBeEmpty();
        }
    }
}
