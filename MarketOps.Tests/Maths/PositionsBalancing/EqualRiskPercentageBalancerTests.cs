using MarketOps.Maths.PositionsBalancing;
using NUnit.Framework;
using Shouldly;

namespace MarketOps.Tests.Maths.PositionsBalancing
{
    [TestFixture]
    public class EqualRiskPercentageBalancerTests
    {
        [TestCase(new float[2] { 0.5f, 0.5f }, 0.25f)]
        [TestCase(new float[2] { 0.1f, 0.3f }, 0.075f)]
        [TestCase(new float[2] { 0.1f, 0.5f }, 0.083f)]
        [TestCase(new float[2] { 0.2f, 0.5f }, 0.142f)]
        [TestCase(new float[3] { 0.5f, 0.5f, 0.5f }, 0.166f)]
        [TestCase(new float[3] { 0.1f, 0.1f, 0.1f }, 0.033f)]
        [TestCase(new float[3] { 0.1f, 0.2f, 0.3f }, 0.054f)]
        public void Calculate__ReturnsCorrectly(float[] risks, float expectedRiskedValue)
        {
            float[] balance = EqualRiskPercentageBalancer.Calculate(risks);

            balance.Length.ShouldBe(risks.Length);
            for (int i = 0; i < risks.Length; i++)
                (risks[i] * balance[i]).ShouldBe(expectedRiskedValue, 0.001);
        }

        [Test]
        public void Calculate_OneElement__ReturnsOne([Values(0.5f, 1f, 0.1f)] float risk)
        {
            EqualRiskPercentageBalancer.Calculate(new float[1] { risk }).ShouldBe(new float[1] { 1f });
        }

        [Test]
        public void Calculate_ZeroLength__ReturnsEmptyArray()
        {
            EqualRiskPercentageBalancer.Calculate(new float[0]).ShouldBeEmpty();
        }
    }
}
