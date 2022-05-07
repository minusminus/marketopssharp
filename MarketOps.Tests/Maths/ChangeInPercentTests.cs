using NUnit.Framework;
using Shouldly;
using MarketOps.Maths;

namespace MarketOps.Tests.Maths
{
    [TestFixture]
    public class ChangeInPercentTests
    {
        [TestCase(1, 0, 0)]
        [TestCase(2, 1, 1)]
        [TestCase(5, 2, 1.5f)]
        public void Calculate_Float__CalculatesCorrectly(float current, float previous, float expected)
        {
            ChangeInPercent.Calculate(current, previous).ShouldBe(expected, 0.0001f);
        }

        [TestCase(1, 0, 0)]
        [TestCase(2, 1, 1)]
        [TestCase(5, 2, 1.5)]
        public void Calculate_Double__CalculatesCorrectly(double current, double previous, double expected)
        {
            ChangeInPercent.Calculate(current, previous).ShouldBe(expected, 0.0001);
        }
    }
}
