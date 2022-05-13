using NUnit.Framework;
using Shouldly;
using MarketOps.Maths;

namespace MarketOps.Tests.Maths
{
    [TestFixture]
    public class DivisionTests
    {
        [Test]
        public void DivideByZeroToZero_NonZeroDivider__ReturnsCorrectly()
        {
            4f.DivideByZeroToZero(2f).ShouldBe(2f);
        }

        [Test]
        public void DivideByZeroToZero_ZeroDivider__ReturnsZero()
        {
            4f.DivideByZeroToZero(0f).ShouldBe(0f);
        }

        [TestCase(1f, 0.1f, 0.0001f, 10f)]
        [TestCase(0.5f, 0.1f, 0.0001f, 5f)]
        [TestCase(1f, 0.01f, 0.0001f, 100f)]
        [TestCase(10f, 3f, 0.0001f, 3f)]
        public void FloorDivideWithAccuracy__ReturnsCorrectly(float dividend, float divider, float accuracy, float expected)
        {
            dividend.FloorDivideWithAccuracy(divider, accuracy).ShouldBe(expected);
        }

        [TestCase(1f, 0.1f, 0.0001f, 10f)]
        [TestCase(0.5f, 0.1f, 0.0001f, 5f)]
        [TestCase(1f, 0.01f, 0.0001f, 100f)]
        [TestCase(10f, 3f, 0.0001f, 4f)]
        public void CeilingDivideWithAccuracy__ReturnsCorrectly(float dividend, float divider, float accuracy, float expected)
        {
            dividend.CeilingDivideWithAccuracy(divider, accuracy).ShouldBe(expected);
        }
    }
}
