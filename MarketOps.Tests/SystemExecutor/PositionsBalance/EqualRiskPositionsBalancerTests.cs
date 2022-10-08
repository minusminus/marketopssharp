using MarketOps.SystemExecutor.PositionsBalance;
using NUnit.Framework;
using Shouldly;
using System;

namespace MarketOps.Tests.SystemExecutor.PositionsBalance
{
    [TestFixture]
    public class EqualRiskPositionsBalancerTests
    {
        [TestCase(new float[1] { 1f }, new float[1] { 1f }, new float[1] { 10f }, 100f, new float[1] { 10f })]
        [TestCase(new float[2] { 1f, 1f }, new float[2] { 0.5f, 0.5f }, new float[2] { 10f, 10f }, 100f, new float[2] { 5f, 5f })]
        [TestCase(new float[2] { 1f, 0.5f }, new float[2] { 0.5f, 0.5f }, new float[2] { 10f, 10f }, 100f, new float[2] { 10f * 1f / 3f, 10f * 2f / 3f })]
        [TestCase(new float[2] { 1f, 1f }, new float[2] { 0.5f, 0.25f }, new float[2] { 10f, 10f }, 100f, new float[2] { 5f, 2.5f })]
        [TestCase(new float[3] { 1f, 1f, 1f }, new float[3] { 1f / 3f, 1f / 3f, 1f / 3f }, new float[3] { 10f, 10f, 10f }, 100f, new float[3] { 10f / 3f, 10f / 3f, 10f / 3f })]
        [TestCase(new float[3] { 1f, 0.5f, 1f }, new float[3] { 1f / 3f, 1f / 3f, 1f / 3f }, new float[3] { 10f, 10f, 10f }, 100f, new float[3] { 10f * 1f / 4f, 10f * 2f / 4f, 10f * 1f / 4f })]
        public void CalculateBalance__CalculatesCorrectly(float[] expectedRisks, float[] positionsWeights, float[] prices, float equityValue, float[] expected)
        {
            EqualRiskPositionsBalancer.CalculateBalance(expectedRisks, positionsWeights, prices, equityValue).ShouldBe(expected, 0.01);
        }

        [Test]
        public void CalculateBalance_ZeroLengths__Throws()
        {
            Should.Throw<ArgumentException>(() => EqualRiskPositionsBalancer.CalculateBalance(new float[0], new float[0], new float[0], 0));
        }

        [Test]
        public void CalculateBalance_DifferentLengths__Throws()
        {
            Should.Throw<ArgumentException>(() => EqualRiskPositionsBalancer.CalculateBalance(new float[1], new float[2], new float[3], 0));
        }
    }
}
