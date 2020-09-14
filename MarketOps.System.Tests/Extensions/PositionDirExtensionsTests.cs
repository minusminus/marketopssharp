using NUnit.Framework;
using Shouldly;
using MarketOps.System.Extensions;

namespace MarketOps.System.Tests.Extensions
{
    [TestFixture]
    public class PositionDirExtensionsTests
    {
        [TestCase(PositionDir.Long, 1)]
        [TestCase(PositionDir.Short, -1)]
        public void DirectionMultiplier(PositionDir dir, float expected)
        {
            dir.DirectionMultiplier().ShouldBe(expected);
        }
    }
}
