using NUnit.Framework;
using Shouldly;
using MarketOps.SystemAnalysis.DrawDowns;
using System.Collections.Generic;

namespace MarketOps.Tests.SystemAnalysis.DrawDowns
{
    [TestFixture]
    public class DrawDownsCalculatorTests
    {
        private class DrawDown
        {
            public int StartIndex;
            public int LastIndex;
            public float TopValue;
            public float BottomValue;
        }

        private List<DrawDown> _drawDowns;

        private void OnDrawDown(int startIndex, int lastIndex, float topValue, float bottomValue) =>
            _drawDowns.Add(
                new DrawDown()
                {
                    StartIndex = startIndex,
                    LastIndex = lastIndex,
                    TopValue = topValue,
                    BottomValue = bottomValue
                });

        [SetUp]
        public void SetUp()
        {
            _drawDowns = new List<DrawDown>();
        }

        [Test]
        public void Calculate_EmptyArray__DoesNotReturnAnyDD()
        {
            DrawDownsCalculator.Calculate(new float[0], OnDrawDown);

            _drawDowns.ShouldBeEmpty();
        }

        [Test]
        public void Calculate_DDExists_NoCallback__DoesNotReturnAnyDD()
        {
            DrawDownsCalculator.Calculate(new float[] { 10, 9, 8 }, null);

            _drawDowns.ShouldBeEmpty();
        }

        [Test]
        public void Calculate_OneItem__DoesNotReturnAnyDD()
        {
            DrawDownsCalculator.Calculate(new float[] { 1 }, OnDrawDown);

            _drawDowns.ShouldBeEmpty();
        }

        [TestCase(new float[] { 1, 1, 1 })]
        [TestCase(new float[] { 1, 2, 3, 4, 5 })]
        [TestCase(new float[] { 1, 2, 3, 3, 3 })]
        [TestCase(new float[] { 1, 1, 2, 3, 3 })]
        public void Calculate_AllNotDown__DoesNotReturnAnyDD(float[] data)
        {
            DrawDownsCalculator.Calculate(data, OnDrawDown);

            _drawDowns.ShouldBeEmpty();
        }

        [TestCase(new float[] { 10, 9, 8, 7, 6 }, 0, 4, 10, 6)]
        [TestCase(new float[] { 10, 9, 8, 7, 6, 7, 8, 9 }, 0, 7, 10, 6)]
        [TestCase(new float[] { 10, 9, 8, 7, 8, 9, 8, 9 }, 0, 7, 10, 7)]
        [TestCase(new float[] { 10, 9, 8, 7, 8, 9, 10, 10 }, 0, 5, 10, 7)]
        [TestCase(new float[] { 10, 10, 8, 7, 8, 9, 10, 10 }, 1, 5, 10, 7)]
        public void Calculate_OneDD__ReturnsOneDD(float[] data, 
            int expectedStartIndex, int expectedLastIndex, float expectedTopValue, float expectedBottomVale)
        {
            DrawDownsCalculator.Calculate(data, OnDrawDown);

            _drawDowns.Count.ShouldBe(1);
            CheckDD(0, expectedStartIndex, expectedLastIndex, expectedTopValue, expectedBottomVale);
        }

        [TestCase(new float[] { 10, 9, 8, 9, 11, 10, 9, 8 }, 0, 3, 10, 8, 4, 7, 11, 8)]
        [TestCase(new float[] { 10, 9, 8, 9, 11, 10, 9, 7, 12, 13 }, 0, 3, 10, 8, 4, 7, 11, 7)]
        public void Calculate_TwoDDs__ReturnsTwoDDs(float[] data,
            int expectedStartIndex, int expectedLastIndex, float expectedTopValue, float expectedBottomVale,
            int expectedStartIndex2, int expectedLastIndex2, float expectedTopValue2, float expectedBottomVale2)
        {
            DrawDownsCalculator.Calculate(data, OnDrawDown);

            _drawDowns.Count.ShouldBe(2);
            CheckDD(0, expectedStartIndex, expectedLastIndex, expectedTopValue, expectedBottomVale);
            CheckDD(1, expectedStartIndex2, expectedLastIndex2, expectedTopValue2, expectedBottomVale2);
        }

        private void CheckDD(int index, int expectedStartIndex, int expectedLastIndex, float expectedTopValue, float expectedBottomVale)
        {
            _drawDowns[index].StartIndex.ShouldBe(expectedStartIndex);
            _drawDowns[index].LastIndex.ShouldBe(expectedLastIndex);
            _drawDowns[index].TopValue.ShouldBe(expectedTopValue);
            _drawDowns[index].BottomValue.ShouldBe(expectedBottomVale);
        }
    }
}
