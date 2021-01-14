using System;
using NUnit.Framework;
using Shouldly;
using System.Linq;
using MarketOps.SystemAnalysis.DrawDowns;
using System.Collections.Generic;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemAnalysis.Tests.DrawDowns
{
    [TestFixture]
    public class DrawDownsCalculatorTests
    {
        private List<SystemValue> CreateInput(float[] values) =>
            Enumerable
            .Range(0, values.Length)
            .Select(i => new SystemValue() { TS = DateTime.Now.AddDays(i), Value = values[i] })
            .ToList();

        private void CheckOutput(List<SystemDrawDown> output, Tuple<float, float>[] expectedDDs)
        {
            output.Count.ShouldBe(expectedDDs.Length);
            for (int i = 0; i < expectedDDs.Length; i++)
            {
                output[i].TopValue.Value.ShouldBe(expectedDDs[i].Item1);
                output[i].BottomValue.Value.ShouldBe(expectedDDs[i].Item2);
            }
        }

        [Test]
        public void Calculate_EmptyList__ReturnsEmptyList()
        {
            DrawDownsCalculator.Calculate(new List<SystemValue>()).ShouldBeEmpty();
        }

        [Test]
        public void Calculate_OneElement__ReturnsEmptyList()
        {
            DrawDownsCalculator.Calculate(CreateInput(new float[] { 1 })).ShouldBeEmpty();
        }

        [Test]
        public void Calculate_EqualValues__ReturnsEmptyList()
        {
            DrawDownsCalculator.Calculate(CreateInput(new float[] { 1, 1, 1 })).ShouldBeEmpty();
        }

        [Test]
        public void Calculate_RaisingValues__ReturnsEmptyList()
        {
            DrawDownsCalculator.Calculate(CreateInput(new float[] { 1, 2, 3, 4 })).ShouldBeEmpty();
        }

        [Test]
        public void Calculate_FallingValues__ReturnsOneElement()
        {
            CheckOutput(DrawDownsCalculator.Calculate(CreateInput(new float[] { 4, 3, 2, 1 })),
                new Tuple<float, float>[] 
                {
                    Tuple.Create(4f, 1f)
                });
        }

        [Test]
        public void Calculate_TwoDDs__ReturnsTwoDDs()
        {
            CheckOutput(DrawDownsCalculator.Calculate(CreateInput(new float[] { 1, 2, 3, 4, 3, 2, 6, 7, 8, 7, 5, 1, 3 })),
                new Tuple<float, float>[]
                {
                    Tuple.Create(4f, 2f),
                    Tuple.Create(8f, 1f),
                });
        }

        [Test]
        public void Calculate_SawShapedFall__ReturnsCorrectly()
        {
            CheckOutput(DrawDownsCalculator.Calculate(CreateInput(new float[] { 1, 2, 10, 9, 5, 6, 7, 8, 9, 6, 5, 4, 3 })),
                new Tuple<float, float>[]
                {
                    Tuple.Create(10f, 3f)
                });
        }

        [Test]
        public void Calculate_SawShapedRaise__ReturnsCorrectly()
        {
            CheckOutput(DrawDownsCalculator.Calculate(CreateInput(new float[] { 1, 2, 3, 5, 4, 3, 4, 5, 6, 7, 10, 9, 8 })),
                new Tuple<float, float>[]
                {
                    Tuple.Create(5f, 3f),
                    Tuple.Create(10f, 8f),
                });
        }
    }
}
