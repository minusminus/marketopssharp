using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Stats;

namespace MarketOps.Tests.Stats.Stats
{
    [TestFixture]
    public class StatHeikinAshiOCTests
    {
        [Test]
        public void Create__HasDefaultValues()
        {
            StatHeikinAshiOC testObj = new StatHeikinAshiOC("");
            testObj.BackBufferLength.ShouldBe(1);
        }
    }
}
