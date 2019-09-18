using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.DataProvider.Pg;
using MarketOps.StockData.Types;

namespace MarketOps.DataProvider.Pq.Tests
{
    [TestFixture]
    public class PgDataPumpProviderTests
    {
        private readonly PgDataPumpProvider TestObj = new PgDataPumpProvider();
    }
}
