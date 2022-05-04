using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Calculators;

namespace MarketOps.Tests.Stats.Calculators
{
    [TestFixture]
    public class HeikinAshiOCTests
    {
        [Test]
        public void Calculate__CalculatesCorrectly()
        {
            float[] opens = new float[4] { 10, 20, 30, 40 };
            float[] closes = new float[4] { 5, 30, 35, 50 };
            float[] highs = new float[4] { 12, 30, 40, 60 };
            float[] lows = new float[4] { 4, 20, 25, 30 };

            var result = HeikinAshiOC.Calculate(opens, highs, lows, closes);

            result.O.ShouldBe(new float[3] { 7.5f, 25f, 32.5f }, 0.0001f);
            result.C.ShouldBe(new float[3] { 25f, 32.5f, 45f }, 0.0001f);
        }

        [Test]
        public void Calculate_TooShortData__ReturnsEmptyresult(
            [Values(true, false)] bool incorrectO, [Values(true, false)] bool incorrectH,
            [Values(true, false)] bool incorrectL, [Values(true, false)] bool incorrectC)
        {
            if (!incorrectO && !incorrectH && !incorrectL && !incorrectC) return;

            float[] correctLength = new float[2] { 1, 2 };
            float[] singleElement = new float[1] { 1 };

            TestEmpty(
                incorrectO ? singleElement : correctLength,
                incorrectH ? singleElement : correctLength,
                incorrectL ? singleElement : correctLength,
                incorrectC ? singleElement : correctLength);
        }

        [Test]
        public void Calculate_EmptyData__ReturnsEmptyResult(
            [Values(true, false)] bool incorrectO, [Values(true, false)] bool incorrectH,
            [Values(true, false)] bool incorrectL, [Values(true, false)] bool incorrectC)
        {
            if (!incorrectO && !incorrectH && !incorrectL && !incorrectC) return;

            float[] correctLength = new float[2] { 1, 2 };
            float[] empty = new float[0];

            TestEmpty(
                incorrectO ? empty : correctLength,
                incorrectH ? empty : correctLength,
                incorrectL ? empty : correctLength,
                incorrectC ? empty : correctLength);
        }

        private void TestEmpty(float[] opens, float[] highs, float[] lows, float[] closes)
        {
            var result = HeikinAshiOC.Calculate(opens, highs, lows, closes);

            result.O.ShouldBeEmpty();
            result.C.ShouldBeEmpty();
        }
    }
}
