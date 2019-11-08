using System;
using NUnit.Framework;
using Shouldly;
using NSubstitute;
using System.Collections.Generic;
using System.IO;
using MarketOps.StockData.Types;
using MarketOps.DataPump.Types;
using MarketOps.StockData.Interfaces;
using MarketOps.DataPump.DBWriters;
using MarketOps.DataPump.Bossa;

namespace MarketOps.DataPump.Tests.Bossa
{
    [TestFixture]
    public class DataPumpWithoutDBWritesTests
    {
        private MarketOps.DataPump.Bossa.DataPump TestObj;

        private IDataPumpProvider _dataPumpProvider;
        private IDataFileIterator _dataFileIterator;
        private IDataPumpStockDataToDBWriter _stockDataToDBWriter;
        private IDataFileLineToStockData _lineToStockData;
        private IDataFileDownloader _dataFileDownloader;
        private DownloadDirectories _downloadDirectories;

        private List<string> _executedQueries = new List<string>();

        private readonly string _rootPath = Path.Combine(Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath), "DataPumpWithoutDBWritesTests");
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
            _executedQueries.Clear();
            _dataPumpProvider = Substitute.For<IDataPumpProvider>();
            _dataPumpProvider.GetTableName(StockType.Stock, StockDataRange.Day, 0).Returns("at_dzienne0");
            _dataPumpProvider.GetMaxTS(Arg.Compat.Any<StockDefinition>(), StockDataRange.Day, 0).Returns(new DateTime(2019, 11, 01));
            _dataPumpProvider.ExecuteSQL(Arg.Compat.Do<string>(s => _executedQueries.Add(s)));

            _stockDataToDBWriter = new DataPumpStockDataToDBWriter(_dataPumpProvider, new InsertCommandGenerator(_dataPumpProvider));
            _dataFileIterator = new DailyDataFileIterator();
            _lineToStockData = new DailyDataFileLineToStockData();

            _downloadDirectories = new DownloadDirectories(_rootPath);
            DownloadUrlPrepator downloadUrlPrepator = new DownloadUrlPrepator(_downloadDefinitions);
            DownloadFilePathPreparator downloadFilePathPreparator = new DownloadFilePathPreparator(_downloadDefinitions, _downloadDirectories);
            DownloadUnzipPathPreparator downloadUnzipPathPreparator = new DownloadUnzipPathPreparator(_downloadDirectories);
            DownloadFilesQueue downloadFilesQueue = new DownloadFilesQueue();
            DownloadPipe downloadPipe = new DownloadPipe(new WebClientFileDownloader(), new SystemFileUnzipper(), downloadFilesQueue);
            _dataFileDownloader = new DataFileDownloader(downloadPipe, downloadFilesQueue, downloadUrlPrepator, downloadFilePathPreparator, downloadUnzipPathPreparator);

            TestObj = new MarketOps.DataPump.Bossa.DataPump(_dataPumpProvider, _dataFileIterator, _stockDataToDBWriter, _lineToStockData, _dataFileDownloader, _downloadDirectories);
        }

        [TearDown]
        public void TearDown()
        {
            DirectoryUtils.ClearDir(_rootPath, false);
        }

        [Test]
        public void PumpDaily_WIG__CorrectInsertsExecuted()
        {
            StockDefinition wig = new StockDefinition() { ID = 288, Name = "WIG", Type = StockType.Index };
            TestObj.PumpDaily(wig);
            Console.Write(String.Join(Environment.NewLine, _executedQueries.ToArray()));
            _executedQueries.Count.ShouldBeGreaterThanOrEqualTo(5);
            _executedQueries[0].ShouldBe("insert into (fk_id_spolki, ts, open, high, low, close, refcourse, volume) values (288, to_date('2019-11-04', 'YYYY-MM-DD'), 58294.4300, 59326.5200, 58250.4600, 59326.1300, 57783.0200, 1037276.603)");
            _executedQueries[1].ShouldBe("insert into (fk_id_spolki, ts, open, high, low, close, refcourse, volume) values (288, to_date('2019-11-05', 'YYYY-MM-DD'), 59352.2000, 59651.5200, 59234.5300, 59517.7000, 59326.1300, 816794.889)");
            _executedQueries[2].ShouldBe("insert into (fk_id_spolki, ts, open, high, low, close, refcourse, volume) values (288, to_date('2019-11-06', 'YYYY-MM-DD'), 59378.5200, 59523.8000, 59174.2900, 59174.2900, 59517.7000, 637393.894)");
            _executedQueries[3].ShouldBe("insert into (fk_id_spolki, ts, open, high, low, close, refcourse, volume) values (288, to_date('2019-11-07', 'YYYY-MM-DD'), 59447.2500, 59612.6900, 59376.4500, 59492.2200, 59174.2900, 845697.212)");
            _executedQueries[4].ShouldBe("insert into (fk_id_spolki, ts, open, high, low, close, refcourse, volume) values (288, to_date('2019-11-08', 'YYYY-MM-DD'), 59373.7000, 59407.0900, 59127.9800, 59191.7100, 59492.2200, 901038.557)");
        }
    }
}
