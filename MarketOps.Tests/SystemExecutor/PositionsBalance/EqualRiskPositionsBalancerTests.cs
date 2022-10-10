using MarketOps.SystemExecutor.PositionsBalance;
using NUnit.Framework;
using Shouldly;
using System;

namespace MarketOps.Tests.SystemExecutor.PositionsBalance
{
    [TestFixture]
    public class EqualRiskPositionsBalancerTests
    {
        [TestCase(new float[1] { 1f }, new float[1] { 10f }, 100f, new float[1] { 10f })]
        [TestCase(new float[2] { 1f, 1f }, new float[2] { 10f, 10f }, 100f, new float[2] { 5f, 5f })]
        [TestCase(new float[2] { 0.5f, 0.5f }, new float[2] { 10f, 10f }, 100f, new float[2] { 5f, 5f })]
        [TestCase(new float[2] { 1f, 0.5f }, new float[2] { 10f, 10f }, 100f, new float[2] { 10f * 1f / 3f, 10f * 2f / 3f })]
        [TestCase(new float[2] { 1f, 0.25f }, new float[2] { 10f, 10f }, 100f, new float[2] { 10f * 1f / 5f, 10f * 4f / 5f })]
        [TestCase(new float[3] { 1f, 1f, 1f }, new float[3] { 10f, 10f, 10f }, 100f, new float[3] { 10f / 3f, 10f / 3f, 10f / 3f })]
        [TestCase(new float[3] { 1f, 0.5f, 1f }, new float[3] { 10f, 10f, 10f }, 100f, new float[3] { 10f * 1f / 4f, 10f * 2f / 4f, 10f * 1f / 4f })]
        public void Calculate__CalculatesCorrectly(float[] expectedRisks, float[] prices, float equityValue, float[] expected)
        {
            EqualRiskPositionsBalancer.Calculate(expectedRisks, prices, equityValue).ShouldBe(expected, 0.01);
        }

        [Test]
        public void Calculate_ZeroLengths__Throws()
        {
            Should.Throw<ArgumentException>(() => EqualRiskPositionsBalancer.Calculate(new float[0], new float[0], 0));
        }

        [Test]
        public void Calculate_UnequalLengths__Throws()
        {
            Should.Throw<ArgumentException>(() => EqualRiskPositionsBalancer.Calculate(new float[1], new float[3], 0));
        }
    }
}
