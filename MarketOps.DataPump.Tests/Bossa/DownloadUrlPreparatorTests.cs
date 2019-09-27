using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using MarketOps.DataPump.Bossa;
using MarketOps.DataPump.Types;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump.Tests.Bossa
{
    [TestFixture]
    public class DownloadUrlPreparatorTests
    {
        private readonly Dictionary<StockType, DataPumpDownloadDefinition> _downloadDefinitions =
            new Dictionary<StockType, DataPumpDownloadDefinition>()
            {
                {StockType.Stock, new DataPumpDownloadDefinition() {PathDaily = "http://testdata.pl/daily/stocks", FileNameDaily = "mstall.zip", PathIntra = "http://testdata.pl/intra/stocks"}},
                {StockType.Index, new DataPumpDownloadDefinition() {PathDaily = "http://testdata.pl/daily/index", FileNameDaily = "mstall.zip", PathIntra = "http://testdata.pl/intra/index"}}
            };
        private DownloadUrlPrepator TestObj;

        [SetUp]
        public void SetUp()
        {
            TestObj = new DownloadUrlPrepator(_downloadDefinitions);
        }

        private void CheckPreparedPathDaily(string path, StockDefinition stock)
        {
            path.ShouldBe(_downloadDefinitions[stock.Type].PathDaily + "/" + _downloadDefinitions[stock.Type].FileNameDaily);
        }

        [Test]
        public void Prepare_Daily__PreparesPath()
        {
            StockDefinition stock = new StockDefinition() {ID = 1, Type = StockType.Stock, Name = "teststock"};
            CheckPreparedPathDaily(TestObj.Prepare(stock, DataPumpDownloadRange.Daily), stock);
        }

        [Test]
        public void Prepare_Daily_TwoTimes__PreparesPaths()
        {
            StockDefinition stock = new StockDefinition() { ID = 1, Type = StockType.Stock, Name = "teststock" };
            CheckPreparedPathDaily(TestObj.Prepare(stock, DataPumpDownloadRange.Daily), stock);
            stock.ID = 2;
            stock.Type = StockType.Index;
            CheckPreparedPathDaily(TestObj.Prepare(stock, DataPumpDownloadRange.Daily), stock);
        }

        [Test]
        public void Prepare_Ticks__ThrowsNotImplemented()
        {
            StockDefinition stock = new StockDefinition() { ID = 1, Type = StockType.Stock, Name = "teststock" };
            Should.Throw<NotImplementedException>(
                () => CheckPreparedPathDaily(TestObj.Prepare(stock, DataPumpDownloadRange.Ticks), stock));
        }
    }
}
