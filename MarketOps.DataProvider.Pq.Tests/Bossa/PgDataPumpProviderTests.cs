using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.DataProvider.Pg.Bossa;
using MarketOps.StockData.Types;
using MarketOps.DataProvider.Pg;

namespace MarketOps.DataProvider.Pq.Tests.Bossa
{
    [TestFixture]
    public class PgDataPumpProviderTests
    {
        private readonly DataTableSelector dataTableSelector = new DataTableSelector();
        private PgDataProvider TestObj;// = new PgDataPumpProvider(dataTableSelector);

        [SetUp]
        public void SetUp()
        {
            TestObj = new PgDataProvider(dataTableSelector);
        }

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
