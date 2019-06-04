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

        [Test]
        public void SMA__CalculatesCorrectly()
        {
            float[] data = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            float[] expected = {2.5f, 3.5f, 4.5f, 5.5f, 6.5f, 7.5f, 8.5f};

            float[] res = _testObj.Calculate(data, 4);
            res.ShouldBe(expected);
        }
    }
}
