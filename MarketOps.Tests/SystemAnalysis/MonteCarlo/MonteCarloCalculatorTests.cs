using NUnit.Framework;
using Shouldly;
using System.Linq;
using System.Collections.Generic;
using MarketOps.SystemData.Types;
using MarketOps.SystemAnalysis.MonteCarlo;

namespace MarketOps.Tests.SystemAnalysis.MonteCarlo
{
    [TestFixture]
    public class MonteCarloCalculatorTests
    {
        private const int Count = 10;
        private const int Length = 20;

        [Test]
        public void Calculate_AllWins__CalculatesCorrectly()
        {
            MonteCarloResult result = MonteCarloCalculator.Calculate(Count, Length, 1, 0.1f, 0.1f, 1);

            result.Data.GetLength(0).ShouldBe(Count);
            result.Data.GetLength(1).ShouldBe(Length);
            result.Wins.ShouldBe(Count);
            result.Losses.ShouldBe(0);
            result.WinsPcnt.ShouldBe(1f);
            result.LossesPcnt.ShouldBe(0f);
        }

        [Test]
        public void Calculate_AllLosses__CalculatesCorrectly()
        {
            MonteCarloResult result = MonteCarloCalculator.Calculate(Count, Length, 0, 0.1f, 0.1f, 1);

            result.Data.GetLength(0).ShouldBe(Count);
            result.Data.GetLength(1).ShouldBe(Length);
            result.Wins.ShouldBe(0);
            result.Losses.ShouldBe(Count);
            result.WinsPcnt.ShouldBe(0f);
            result.LossesPcnt.ShouldBe(1f);
        }
    }
}
