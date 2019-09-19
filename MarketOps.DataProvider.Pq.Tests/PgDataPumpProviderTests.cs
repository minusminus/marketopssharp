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

        [Test]
        public void GetAllStockDefinitions__GetsData()
        {
            var list = TestObj.GetAllStockDefinitions();
            list.ShouldNotBeNull();
            list.Count.ShouldBeGreaterThan(0);
            //Console.WriteLine($"count={list.Count}");
        }

        [Test]
        public void GetDownloadDefinitions__GetsData()
        {
            var defs = TestObj.GetDownloadDefinitions();
            defs.ShouldNotBeNull();
            defs.Count.ShouldBeGreaterThan(0);
        }

        [Test]
        public void ExecuteSQL__Executed()
        {
            TestObj.ExecuteSQL("delete from autotest_table");
        }
    }
}
