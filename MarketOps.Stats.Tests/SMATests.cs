using System;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using MarketOps.Stats.Calculators;

namespace MarketOps.Stats.Tests
{
    [TestFixture]
    public class SMATests
    {
        private readonly SMA _testObj = new SMA();

        [TestCase(new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, new float[] {2.5f, 3.5f, 4.5f, 5.5f, 6.5f, 7.5f, 8.5f}, 4)]
        [TestCase(new float[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }, new float[] { 8.5f, 7.5f, 6.5f, 5.5f, 4.5f, 3.5f, 2.5f }, 4)]
        [TestCase(new float[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, new float[] { 2f, 2f, 2f, 2f, 2f, 2f, 2f }, 4)]
        [TestCase(new float[] { 1, 2, 3, 4 }, new float[] { 1, 2, 3, 4 }, 1)]
        public void SMA__CalculatesCorrectly(float[] data, float[] expected, int period)
        {
            _testObj.Calculate(data, period).ShouldBe(expected);
        }

        [TestCase(new float[] { 1, 2, 3 })]
        [TestCase(new float[] {})]
        public void SMA_TooShortData__EmptyResult(float[] data)
        {
            _testObj.Calculate(data, 4).ShouldBeEmpty();
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void SMA_IncorrectPeriod__EmptyResult(int period)
        {
            _testObj.Calculate(new float[] {1, 2, 3, 4}, period).ShouldBeEmpty();
        }
    }
}
