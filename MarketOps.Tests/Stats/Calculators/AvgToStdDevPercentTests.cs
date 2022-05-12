using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Calculators;

namespace MarketOps.Tests.Stats.Calculators
{
    [TestFixture]
    public class AvgToStdDevPercentTests
    {
        [TestCase(new float[] { 1, 2, 3, 4 }, 2, new float[] { 3f, 5f })]
        [TestCase(new float[] { 1, 2, 3, 4 }, 3, new float[] { 2.1572f })]
        [TestCase(new float[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 4, new float[] { 0f, 0f, 0f, 0f, 0f, 0f })]
        [TestCase(new float[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 4, new float[] { 1.7902f, 2.8172f, 3.7832f, 4.7224f, 5.6475f, 6.5643f })]
        [TestCase(new float[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }, 4, new float[] { -7.4758f, -6.5643f, -5.6475f, -4.7224f, -3.7832f, -2.8172f })]
        public void Calculate__CalculatesCorrectly(float[] data, int range, float[] expected)
        {
            AvgToStdDevPercent.Calculate(data, range).ShouldBe(expected, 0.0001f);
        }

        [TestCase(new float[] { 1, 2, 3, 4 })]
        [TestCase(new float[] { 1, 2, 3 })]
        [TestCase(new float[] { })]
        public void Calculate_TooShortData__EmptyResult(float[] data)
        {
            AvgToStdDevPercent.Calculate(data, 4).ShouldBeEmpty();
        }

        [Test]
        public void Calculate_IncorrectPeriod__EmptyResult([Values(-1, 0, 1, 5)] int range)
        {
            AvgToStdDevPercent.Calculate(new float[] { 1, 2, 3, 4, 5 }, range).ShouldBeEmpty();
        }
    }
}
