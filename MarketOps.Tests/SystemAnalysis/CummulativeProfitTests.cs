using NUnit.Framework;
using Shouldly;
using MarketOps.SystemAnalysis;

namespace MarketOps.Tests.SystemAnalysis
{
    [TestFixture]
    public class CummulativeProfitTests
    {
        [TestCase(1, 2, 1, 1)]
        [TestCase(1, 4, 2, 1)]
        [TestCase(1, 2, 0.5, 3)]
        [TestCase(1, 4, 0.5, 15)]
        [TestCase(1, 1.5, 1, 0.5)]
        [TestCase(2, 2, 1, 0)]
        [TestCase(2, 3, 1, 0.5)]
        [TestCase(2, 4, 1, 1)]
        [TestCase(2, 8, 1, 3)]
        public void Calculate__CalculatesCorrectly(double initialValue, double finalValue, double numberOfIntervals, double expected)
        {
            CummulativeProfit.Calculate(initialValue, finalValue, numberOfIntervals).ShouldBe(expected);
        }
    }
}
