using NUnit.Framework;
using Shouldly;
using MarketOps.SystemAnalysis.MonteCarlo;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.Tests.SystemAnalysis.MonteCarlo
{
    [TestFixture]
    public class MonteCarloStreaksCalculatorTests
    {
        [Test]
        public void Calculate_AllUp__CalculatesCorrectly()
        {
            float[][] testData = new float[2][];
            testData[0] = new float[5] { 1, 2, 3, 4, 5 };
            testData[1] = new float[5] { 10, 20, 30, 40, 50 };

            MonteCarloStreaksCalculator.Calculate(testData, out var winningStreaks, out var losingStreaks);

            winningStreaks.Count.ShouldBe(1);
            CheckStreak(winningStreaks, 0, 4, 2);
            losingStreaks.ShouldBeEmpty();
            winningStreaks.Sum(x => x.Length * x.Count).ShouldBe(10 - 2);
        }

        [Test]
        public void Calculate_AllDown__CalculatesCorrectly()
        {
            float[][] testData = new float[2][];
            testData[0] = new float[5] { 5, 4, 3, 2, 1 };
            testData[1] = new float[5] { 50, 40, 30, 20, 10 };

            MonteCarloStreaksCalculator.Calculate(testData, out var winningStreaks, out var losingStreaks);

            winningStreaks.ShouldBeEmpty(); 
            losingStreaks.Count.ShouldBe(1);
            CheckStreak(losingStreaks, 0, 4, 2);
            losingStreaks.Sum(x => x.Length * x.Count).ShouldBe(10 - 2);
        }

        [Test]
        public void Calculate_MixedCase__CalculatesCorrectly()
        {
            float[][] testData = new float[3][];
            testData[0] = new float[5] { 1, 2, 3, 2, 1 };
            testData[1] = new float[5] { 9, 8, 7, 8, 8 };
            testData[2] = new float[5] { 1, 2, 1, 2, 1 };

            MonteCarloStreaksCalculator.Calculate(testData, out var winningStreaks, out var losingStreaks);

            winningStreaks.Count.ShouldBe(2);
            CheckStreak(winningStreaks, 0, 1, 3);
            CheckStreak(winningStreaks, 1, 2, 1);
            losingStreaks.Count.ShouldBe(2);
            CheckStreak(losingStreaks, 0, 1, 3);
            CheckStreak(losingStreaks, 1, 2, 2);
            (losingStreaks.Sum(x => x.Length * x.Count) + winningStreaks.Sum(x => x.Length * x.Count)).ShouldBe(15 - 3);
        }

        private void CheckStreak(List<MonteCarloStreakData> streaks, int index, int expectedLength, int expectedCount)
        {
            streaks[index].Length.ShouldBe(expectedLength);
            streaks[index].Count.ShouldBe(expectedCount);
        }
    }
}
