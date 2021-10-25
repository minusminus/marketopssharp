using NUnit.Framework;
using Shouldly;
using MarketOps.SystemData.Extensions;

namespace MarketOps.Tests.SystemData.Extensions
{
    [TestFixture]
    public class MoneyValueExtensionsTests
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(123456, 123456)]
        [TestCase(1.1f, 1.1f)]
        [TestCase(1.01f, 1.01f)]
        [TestCase(1.001f, 1f)]
        [TestCase(-1, -1)]
        [TestCase(-123456, -123456)]
        [TestCase(-1.1f, -1.1f)]
        [TestCase(-1.01f, -1.01f)]
        [TestCase(-1.001f, -1f)]
        public void TruncateTo2ndPlace(float value, float expected)
        {
            value.TruncateTo2ndPlace().ShouldBe(expected);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(123456, 123456)]
        [TestCase(1.1f, 1.1f)]
        [TestCase(1.01f, 1f)]
        [TestCase(1.001f, 1f)]
        [TestCase(-1, -1)]
        [TestCase(-123456, -123456)]
        [TestCase(-1.1f, -1.1f)]
        [TestCase(-1.01f, -1f)]
        [TestCase(-1.001f, -1f)]
        public void TruncateTo1stPlace(float value, float expected)
        {
            value.TruncateTo1stPlace().ShouldBe(expected);
        }

        [TestCase(0, 1, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(123456, 1, 123456)]
        [TestCase(1.1f, 1, 1.1f)]
        [TestCase(1.1f, 2, 1.1f)]
        [TestCase(1.1f, 3, 1.1f)]
        [TestCase(1.01f, 1, 1f)]
        [TestCase(1.01f, 2, 1.01f)]
        [TestCase(1.01f, 3, 1.01f)]
        [TestCase(1.001f, 1, 1f)]
        [TestCase(1.001f, 2, 1f)]
        [TestCase(1.001f, 3, 1.001f)]
        [TestCase(-1, 1, -1)]
        [TestCase(-123456, 1, -123456)]
        [TestCase(-1.1f, 1, -1.1f)]
        [TestCase(-1.1f, 2, -1.1f)]
        [TestCase(-1.1f, 3, -1.1f)]
        [TestCase(-1.01f, 1, -1f)]
        [TestCase(-1.01f, 2, -1.01f)]
        [TestCase(-1.01f, 3, -1.01f)]
        [TestCase(-1.001f, 1, -1f)]
        [TestCase(-1.001f, 2, -1f)]
        [TestCase(-1.001f, 3, -1.001f)]
        public void TruncateToNthPlace(float value, int n, float expected)
        {
            value.TruncateToNthPlace(n).ShouldBe(expected);
        }
    }
}
