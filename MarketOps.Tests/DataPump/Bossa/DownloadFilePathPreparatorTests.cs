using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Shouldly;
using MarketOps.DataPump.Bossa;
using MarketOps.DataPump.Types;
using MarketOps.StockData.Types;

namespace MarketOps.Tests.DataPump.Bossa
{
    [TestFixture]
    public class DownloadFilePathPreparatorTests
    {
        private DownloadFilePathPreparator TestObj;

        private readonly Dictionary<StockType, DataPumpDownloadDefinition> _downloadDefinitions =
            new Dictionary<StockType, DataPumpDownloadDefinition>()
            {
                {StockType.Stock, new DataPumpDownloadDefinition() {PathDaily = "http://testdata.pl/daily/stocks", FileNameDaily = "mstall.zip", PathIntra = "http://testdata.pl/intra/stocks"}},
                {StockType.Index, new DataPumpDownloadDefinition() {PathDaily = "http://testdata.pl/daily/index", FileNameDaily = "mstall.zip", PathIntra = "http://testdata.pl/intra/index"}}
            };
        private readonly string _rootPath = Path.Combine(Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath), "DownloadFilePathPreparatorTests");
        private DownloadDirectories _downloadDirectories;

        [SetUp]
        public void SetUp()
        {
            DirectoryUtils.ClearDir(_rootPath, true);
            _downloadDirectories = new DownloadDirectories(_rootPath);
            TestObj = new DownloadFilePathPreparator(_downloadDefinitions, _downloadDirectories);
        }

        [TearDown]
        public void TearDown()
        {
            DirectoryUtils.ClearDir(_rootPath, false);
        }

        [Test]
        public void Prepare_Daily__PreparesPath()
        {
            StockDefinition stock = new StockDefinition() { ID = 1, Type = StockType.Stock, Name = "teststock" };
            TestObj.Prepare(stock, DataPumpDownloadRange.Daily).ShouldBe(_rootPath + "\\" + "mstall.zip");
        }

        [Test]
        public void Prepare_Daily_SameTypeTwoTimes__PreparesSamePath()
        {
            StockDefinition stock = new StockDefinition() { ID = 1, Type = StockType.Stock, Name = "teststock" };
            TestObj.Prepare(stock, DataPumpDownloadRange.Daily).ShouldBe(_rootPath + "\\" + "mstall.zip");
            stock.ID = 2;
            TestObj.Prepare(stock, DataPumpDownloadRange.Daily).ShouldBe(_rootPath + "\\" + "mstall.zip");
        }

        [Test]
        public void Prepare_Ticks__ThrowsNotImplemented()
        {
            StockDefinition stock = new StockDefinition() { ID = 1, Type = StockType.Stock, Name = "teststock" };
            Should.Throw<NotImplementedException>(() => TestObj.Prepare(stock, DataPumpDownloadRange.Ticks));
        }
    }
}
