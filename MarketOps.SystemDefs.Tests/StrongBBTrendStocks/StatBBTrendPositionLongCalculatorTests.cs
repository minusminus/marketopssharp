using MarketOps.SystemDefs.StrongBBTrendStocks;
using NUnit.Framework;
using Shouldly;

namespace MarketOps.SystemDefs.Tests.StrongBBTrendStocks
{
    [TestFixture]
    public class StatBBTrendPositionLongCalculatorTests
    {
        [TestCase(new[] { 2f, 2f, 2f, 3f, 4f, 5f }, new[] { 1f, 1f, 1f, 2f, 3f, 4f }, 3, 2f, 3, new[] { 1f, 1f, 1f })]
        [TestCase(new[] { 5f, 4f, 3f, 2f, 2f, 2f }, new[] { 4f, 3f, 2f, 1f, 1f, 1f }, 3, 2f, 3, new[] { 0f, 0f, 0f })]
        [TestCase(new[] { 2f, 2f, 2f, 3f, 4f, 3f }, new[] { 1f, 1f, 1f, 2f, 0.5f, 2f }, 3, 2f, 3, new[] { 1f, 1f, 0f })]
        [TestCase(new[] { 5f, 5f, 5f, 4f, 7f, 8f }, new[] { 4f, 4f, 4f, 3f, 6f, 6f }, 3, 2f, 3, new[] { 0f, 1f, 1f })]
        [TestCase(new[] { 5f, 5f, 5f, 4f, 7f, 5f }, new[] { 4f, 4f, 4f, 3f, 2f, 4f }, 3, 2f, 3, new[] { 0f, 1f, 0f })]
        public void Calculate__CalculatesCorrectly(float[] dataC, float[] dataL, int bbPeriod, float bbSigmaWidth, int trailingStopMinOfN, float[] expected)
        {
            StatBBTrendPositionLongCalculator.Calculate(dataC, dataL, bbPeriod, bbSigmaWidth, trailingStopMinOfN).ShouldBe(expected);
        }
    }
}
