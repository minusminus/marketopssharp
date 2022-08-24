using NUnit.Framework;
using Shouldly;
using MarketOps.SystemData.Extensions;

namespace MarketOps.Tests.SystemData.Extensions
{
    [TestFixture]
    public class FloatExtensionsTests
    {
        [TestCase(0, 1, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 0.5f, 1)]
        [TestCase(1, 1.5f, 0)]
        [TestCase(2.2f, 0.1f, 2.2f)]
        [TestCase(2.2f, 0.2f, 2.2f)]
        [TestCase(2.2f, 0.3f, 2.1f)]
        [TestCase(2.2f, 0.4f, 2f)]
        [TestCase(2.2f, 0.5f, 2f)]
        [TestCase(2.2f, 0.6f, 1.8f)]
        [TestCase(2.2f, 0.7f, 2.1f)]
        [TestCase(2.2f, 0.8f, 1.6f)]
        [TestCase(2.2f, 0.9f, 1.8f)]
        [TestCase(2.2f, 1f, 2f)]
        public void FlooredMultipleOfN__ReturnsCorrectly(float value, float n, float expected)
        {
            value.FlooredMultipleOfN(n).ToString("F2").ShouldBe(expected.ToString("F2"));
        }

        [Test]
        public void FlooredMultipleOfN_ZeroN__ReturnsNaN([Range(1f, 10f, 1f)] float value)
        {
            value.FlooredMultipleOfN(0).ShouldBe(float.NaN);
        }
    }
}
