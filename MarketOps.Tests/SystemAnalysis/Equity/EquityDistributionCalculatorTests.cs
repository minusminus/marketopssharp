using System;
using NUnit.Framework;
using Shouldly;
using System.Linq;
using MarketOps.SystemAnalysis.DrawDowns;
using System.Collections.Generic;
using MarketOps.SystemData.Types;
using MarketOps.SystemAnalysis.Equity;

namespace MarketOps.Tests.SystemAnalysis.Equity
{
    [TestFixture]
    public class EquityDistributionCalculatorTests
    {
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
