﻿using NUnit.Framework;
using Shouldly;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Types;

namespace MarketOps.Tests.SystemData.Extensions
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

        [TestCase(PositionDir.Long, PositionDir.Short)]
        [TestCase(PositionDir.Short, PositionDir.Long)]
        public void ReverseDirection(PositionDir dir, PositionDir expected)
        {
            dir.ReverseDirection().ShouldBe(expected);
        }
    }
}
