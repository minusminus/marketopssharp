using NUnit.Framework;
using Shouldly;
using System;

namespace MarketOps.SystemDefs.Tests
{
    [TestFixture]
    public class ModNCounterTests
    {
        private const int TestN = 5;

        private ModNCounter _testObj;

        [SetUp]
        public void SetUp()
        {
            _testObj = new ModNCounter(TestN);
        }

        [Test]
        public void Create__IsZero()
        {
            _testObj.IsZero.ShouldBeTrue();
        }

        [Test]
        public void Next__CorrectlySetsIsZero([Range(0, TestN * 3)]int iterations)
        {
            for (int i = 0; i < iterations; i++)
                _testObj.Next();
            _testObj.IsZero.ShouldBe(iterations % TestN == 0);
        }

        [Test]
        public void Reset__CorrectlySetsIsZero([Range(0, TestN * 3)]int iterations)
        {
            for (int i = 0; i < iterations; i++)
                _testObj.Next();
            _testObj.Reset();
            _testObj.IsZero.ShouldBeTrue();
        }

        [Test]
        public void Create_NLessThanOne__Throws([Range(-10, 0)]int n)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => new ModNCounter(n));
        }
    }
}
