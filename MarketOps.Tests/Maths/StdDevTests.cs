using NUnit.Framework;
using Shouldly;
using MarketOps.Maths;
using System.Linq;

namespace MarketOps.Tests.Maths
{
    [TestFixture]
    public class StdDevTests
    {
        [TestCase(new float[] {2}, 0)]
        [TestCase(new float[] {2, 2, 2, 2, 2}, 0)]
        [TestCase(new float[] {1, 2, 3, 4, 5}, 1.4142f)]
        [TestCase(new float[] {5, 4, 3, 2, 1}, 1.4142f)]
        public void Calculate_WholeTable__CalculatesCorrectly(float[] data, float expected)
        {
            StdDev.Calculate(data, data.Average()).ShouldBe(expected, 0.0001);
        }

        [Test]
        public void Calculate_WholeTableZeroLength__ReturnsZero()
        {
            StdDev.Calculate(new float[0], 0).ShouldBe(0);
        }

        [TestCase(new float[] { 2, 2, 2, 2, 2 }, 0, 5, 0)]
        [TestCase(new float[] { 2, 2, 2, 2, 2 }, 1, 3, 0)]
        [TestCase(new float[] { 1, 2, 3, 4, 5 }, 0, 5, 1.4142f)]
        [TestCase(new float[] { 1, 2, 3, 4, 5 }, 1, 3, 0.8164f)]
        [TestCase(new float[] { 5, 4, 3, 2, 1 }, 0, 5, 1.4142f)]
        [TestCase(new float[] { 5, 4, 3, 2, 1 }, 1, 3, 0.8164f)]
        public void Calculate_PartOfTable__CalculatesCorrectly(float[] data, int startIndex, int length, float expected)
        {
            StdDev.Calculate(data, data.Skip(startIndex).Take(length).Average(), startIndex, length).ShouldBe(expected, 0.0001);
        }

        [Test]
        public void Calculate_PartOfTableZeroLength__ReturnsZero()
        {
            StdDev.Calculate(new float[0], 0, 0, 0).ShouldBe(0);
        }
    }
}
