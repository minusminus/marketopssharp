using NUnit.Framework;
using Shouldly;
using System.Linq;
using System.Collections.Generic;
using MarketOps.SystemData.Types;
using MarketOps.SystemAnalysis.Equity;

namespace MarketOps.Tests.SystemAnalysis.Equity
{
    [TestFixture]
    public class EquityDistributionCalculatorTests
    {
        [TestCase(new[] { 1f, 1f, 1f, 1f }, 0f, 0f)]
        [TestCase(new[] { 1f, 2f, 4f, 8f }, 100f, 0f)]
        [TestCase(new[] { 1f, 2f, 2f, 4f, 4f }, 50f, 50f)]
        public void Calculate__CalculateCorrectly(float[] equityValues, float expectedAvg, float expectedStdDev)
        {
            var equity = equityValues.Select(v => new SystemValue() { Value = v }).ToList();
            var result = EquityDistributionCalculator.Calculate(equity);
            result.Average.ToString("F6").ShouldBe(expectedAvg.ToString("F6"));
            result.StdDev.ToString("F6").ShouldBe(expectedStdDev.ToString("F6"));
        }

        [Test]
        public void Calculate_EmptyEquity__EmptyResult()
        {
            CheckForEmpty(EquityDistributionCalculator.Calculate(new List<SystemValue>()));
        }

        [Test]
        public void Calculate_OneElementInEquity__EmptyResult()
        {
            CheckForEmpty(EquityDistributionCalculator.Calculate(new List<SystemValue>() { new SystemValue() }));
        }

        private void CheckForEmpty(EquityDistribution equityDistribution)
        {
            equityDistribution.Average.ShouldBe(0);
            equityDistribution.StdDev.ShouldBe(0);
        }
    }
}
