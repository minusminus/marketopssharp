using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.DataPump;
using MarketOps.DataPump.Bossa;
using System.IO;
using System.Collections.Generic;
using MarketOps.StockData.Types;
using MarketOps.DataPump.Types;

namespace MarketOps.DataPump.Tests.Bossa
{
    [TestFixture]
    public class DataFileDownloaderTests
    {
        private DataFileDownloader TestObj;

        private readonly string _rootPath = Path.Combine(Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath), "DataFileDownloaderTests");
        private readonly Dictionary<StockType, DataPumpDownloadDefinition> _downloadDefinitions =
            new Dictionary<StockType, DataPumpDownloadDefinition>()
            {
                {StockType.Stock, new DataPumpDownloadDefinition() {PathDaily = "http://bossa.pl/pub/metastock/mstock/", FileNameDaily = "mstall.zip", PathIntra = ""}},
                {StockType.Index, new DataPumpDownloadDefinition() {PathDaily = "http://bossa.pl/pub/metastock/mstock/", FileNameDaily = "mstall.zip", PathIntra = ""}},
                {StockType.NBPCurrency, new DataPumpDownloadDefinition() {PathDaily = "https://info.bossa.pl/pub/metastock/waluty/", FileNameDaily = "mstnbp.zip", PathIntra = ""}},
            };

        [SetUp]
        public void SetUp()
        {
            DirectoryUtils.ClearDir(_rootPath, true);
            DownloadDirectories downloadDirectories = new DownloadDirectories(_rootPath);
            DownloadUrlPrepator downloadUrlPrepator = new DownloadUrlPrepator(_downloadDefinitions);
            DownloadFilesQueue downloadFilesQueue = new DownloadFilesQueue();
            DownloadPipe downloadPipe = new DownloadPipe(new WebClientFileDownloader(), new SystemFileUnzipper(), downloadFilesQueue);
            TestObj = new DataFileDownloader(downloadPipe, downloadFilesQueue, downloadDirectories, downloadUrlPrepator);
        }

        [TearDown]
        public void TearDown()
        {
            DirectoryUtils.ClearDir(_rootPath, false);
        }

        [Test]
        public void InitializeDownload_Daily__DownloadsFile()
        {
            StockDefinition stock = new StockDefinition() { ID = 793, Type = StockType.NBPCurrency, Name = "USDPLN" };
            string downloadFilePath = TestObj.InitializeDownload(stock, DataPumpDownloadRange.Daily);
        }
    }
}
