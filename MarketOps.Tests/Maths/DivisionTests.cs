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
    }
}
