using NUnit.Framework;
using Shouldly;
using MarketOps.SystemExecutor.Extensions;
using MarketOps.StockData.Types;

namespace MarketOps.Tests.SystemExecutor.Extensions
{
    [TestFixture]
    public class VolumeExtensionsTests
    {
        [TestCase(1.1f, StockType.Undefined, 1f)]
        [TestCase(1.1f, StockType.Stock, 1f)]
        [TestCase(1.1f, StockType.Index, 1f)]
        [TestCase(1.1f, StockType.IndexFuture, 1f)]
        [TestCase(1.1f, StockType.InvestmentFund, 1.1f)]
        [TestCase(1.1f, StockType.NBPCurrency, 1f)]
        [TestCase(1.1f, StockType.Forex, 1f)]
        public void TruncateToAllowedVolume(float volume, StockType stockType, float expectedVolume)
        {
            volume.TruncateToAllowedVolume(stockType).ShouldBe(expectedVolume);
        }
    }
}
