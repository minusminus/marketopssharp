using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.SystemExecutor.Extensions;

namespace MarketOps.SystemExecutor.Tests.Extensions
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
    }
}
