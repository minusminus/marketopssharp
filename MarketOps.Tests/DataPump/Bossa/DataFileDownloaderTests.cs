using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.DataPump;
using MarketOps.DataPump.Bossa;
using System.IO;
using System.Collections.Generic;
using MarketOps.StockData.Types;
using MarketOps.DataPump.Types;
using NUnit.Framework.Internal;

namespace MarketOps.Tests.DataPump.Bossa
{
    [TestFixture]
    public class DataFileDownloaderTests
    {
        private DataFileDownloader TestObj;

        private readonly string _rootPath = Path.Combine(Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath), "DataFileDownloaderTests");

        private const string ZipMstall = "mstall.zip";
        private const string ZipMstnbp = "mstnbp.zip";
        private readonly Dictionary<StockType, DataPumpDownloadDefinition> _downloadDefinitions =
            new Dictionary<StockType, DataPumpDownloadDefinition>()
            {
                {StockType.Stock, new DataPumpDownloadDefinition() {PathDaily = "http://bossa.pl/pub/metastock/mstock/", FileNameDaily = ZipMstall, PathIntra = ""}},
                {StockType.Index, new DataPumpDownloadDefinition() {PathDaily = "http://bossa.pl/pub/metastock/mstock/", FileNameDaily = ZipMstall, PathIntra = ""}},
                {StockType.NBPCurrency, new DataPumpDownloadDefinition() {PathDaily = "https://info.bossa.pl/pub/metastock/waluty/", FileNameDaily = ZipMstnbp, PathIntra = ""}},
            };

        [SetUp]
        public void SetUp()
        {
            DirectoryUtils.ClearDir(_rootPath, true);
            DownloadDirectories downloadDirectories = new DownloadDirectories(_rootPath);
            DownloadUrlPrepator downloadUrlPrepator = new DownloadUrlPrepator(_downloadDefinitions);
            DownloadFilePathPreparator downloadFilePathPreparator = new DownloadFilePathPreparator(_downloadDefinitions, downloadDirectories);
            DownloadUnzipPathPreparator downloadUnzipPathPreparator = new DownloadUnzipPathPreparator(downloadDirectories);
            DownloadFilesQueue downloadFilesQueue = new DownloadFilesQueue();
            DownloadPipe downloadPipe = new DownloadPipe(new WebClientFileDownloader(), new SystemFileUnzipper(), downloadFilesQueue);
            TestObj = new DataFileDownloader(downloadPipe, downloadFilesQueue, downloadUrlPrepator, downloadFilePathPreparator, downloadUnzipPathPreparator);
        }

        [TearDown]
        public void TearDown()
        {
            DirectoryUtils.ClearDir(_rootPath, false);
        }

        [Test]
        public void InitializeDownload_Daily__DownloadsAndUnzipsFile()
        {
            StockDefinition stock = new StockDefinition() { ID = 793, Type = StockType.NBPCurrency, FullName = "USD" };
            string downloadFilePath = TestObj.InitializeDownload(stock, DataPumpDownloadRange.Daily);
            downloadFilePath.ShouldBe(Path.Combine(_rootPath, ZipMstnbp));
            File.Exists(downloadFilePath).ShouldBeTrue();
            Directory.Exists(Path.Combine(_rootPath, "mstnbp")).ShouldBeTrue();
            File.Exists(Path.Combine(_rootPath, "mstnbp", stock.FullName + ".mst")).ShouldBeTrue();
        }

        [Test]
        public void InitializeDownload_Ticks__ThrowsNotImplemented()
        {
            StockDefinition stock = new StockDefinition() { ID = 793, Type = StockType.NBPCurrency, FullName = "USD" };
            Should.Throw<NotImplementedException>(() => TestObj.InitializeDownload(stock, DataPumpDownloadRange.Ticks));
        }

        [Test]
        public void WaitForDownload__ProcessesCorrectly()
        {
            TestObj.WaitForDownload("");
        }
    }
}
