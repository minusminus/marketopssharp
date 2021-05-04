using System;
using NUnit.Framework;
using Shouldly;
using NSubstitute;
using System.Collections.Generic;
using System.IO;
using MarketOps.StockData.Types;
using MarketOps.StockData.Interfaces;
using MarketOps.DataPump.DBWriters;
using MarketOps.DataPump.Bossa;
using MarketOps.DataPump;

namespace MarketOps.Tests.DataPump.Bossa
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

        [OneTimeSetUp]
        public void Init()
        {
            DirectoryUtils.ClearDir(_rootPath, true);
            _downloadDirectories = new DownloadDirectories(_rootPath);
            DownloadUrlPrepator downloadUrlPrepator = new DownloadUrlPrepator(_downloadDefinitions);
            DownloadFilePathPreparator downloadFilePathPreparator = new DownloadFilePathPreparator(_downloadDefinitions, _downloadDirectories);
            DownloadUnzipPathPreparator downloadUnzipPathPreparator = new DownloadUnzipPathPreparator(_downloadDirectories);
            DownloadFilesQueue downloadFilesQueue = new DownloadFilesQueue();
            DownloadPipe downloadPipe = new DownloadPipe(new WebClientFileDownloader(), new SystemFileUnzipper(), downloadFilesQueue);
            _dataFileDownloader = new DataFileDownloader(downloadPipe, downloadFilesQueue, downloadUrlPrepator, downloadFilePathPreparator, downloadUnzipPathPreparator);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            DirectoryUtils.ClearDir(_rootPath, false);
        }

        [SetUp]
        public void SetUp()
        {
            _executedQueries.Clear();
            _dataPumpProvider = Substitute.For<IDataPumpProvider>();
            _dataPumpProvider.GetTableName(StockType.Stock, StockDataRange.Daily, 0).Returns("at_dzienne0");
            _dataPumpProvider.GetTableName(StockType.Index, StockDataRange.Daily, 0).Returns("at_dzienne1");
            _dataPumpProvider.ExecuteSQL(Arg.Compat.Do<string>(s => _executedQueries.Add(s)));

            _stockDataToDBWriter = new DataPumpStockDataToDBWriter(_dataPumpProvider, new InsertCommandGenerator(_dataPumpProvider));
            _dataFileIterator = new DailyDataFileIterator();
            _lineToStockData = new DailyDataFileLineToStockData();

            TestObj = new MarketOps.DataPump.Bossa.DataPump(_dataPumpProvider, _dataFileIterator, _stockDataToDBWriter, _lineToStockData, _dataFileDownloader, _downloadDirectories);
        }

        [TearDown]
        public void TearDown()
        {
        }

        private void TestDailyFor20191104(StockDefinition stock, DateTime initialDateTime, List<string> expectedFirst5Inserts)
        {
            expectedFirst5Inserts.Count.ShouldBe(5);
            _executedQueries.Clear();
            _dataPumpProvider.GetMaxTS(Arg.Compat.Any<StockDefinition>(), StockDataRange.Daily, 0).Returns(initialDateTime);
            TestObj.PumpDaily(stock);
            Console.WriteLine($"{stock.Name}:");
            Console.WriteLine(string.Join(Environment.NewLine, _executedQueries.ToArray()));
            _executedQueries.Count.ShouldBeGreaterThanOrEqualTo(5);
            for (int i = 0; i < 5; i++)
                _executedQueries[i].ShouldBe(expectedFirst5Inserts[i]);
        }

        private void TestDailyWIGFor20191104(DateTime initialDateTime)
        {
            StockDefinition wig = new StockDefinition() { ID = 288, Name = "WIG", Type = StockType.Index, StockName = "WIG" };
            List<string> expectedInserts = new List<string>() {
                "insert into at_dzienne1(fk_id_spolki, ts, open, high, low, close, refcourse, volume) values (288, to_date('2019-11-04', 'YYYY-MM-DD'), 58294.4300, 59326.5200, 58250.4600, 59326.1300, 57783.0200, 1037276.603)",
                "insert into at_dzienne1(fk_id_spolki, ts, open, high, low, close, refcourse, volume) values (288, to_date('2019-11-05', 'YYYY-MM-DD'), 59352.2000, 59651.5200, 59234.5300, 59517.7000, 59326.1300, 816794.889)",
                "insert into at_dzienne1(fk_id_spolki, ts, open, high, low, close, refcourse, volume) values (288, to_date('2019-11-06', 'YYYY-MM-DD'), 59378.5200, 59523.8000, 59174.2900, 59174.2900, 59517.7000, 637393.894)",
                "insert into at_dzienne1(fk_id_spolki, ts, open, high, low, close, refcourse, volume) values (288, to_date('2019-11-07', 'YYYY-MM-DD'), 59447.2500, 59612.6900, 59376.4500, 59492.2200, 59174.2900, 845697.212)",
                "insert into at_dzienne1(fk_id_spolki, ts, open, high, low, close, refcourse, volume) values (288, to_date('2019-11-08', 'YYYY-MM-DD'), 59373.7000, 59407.0900, 59127.9800, 59191.7100, 59492.2200, 901038.557)"
            };
            TestDailyFor20191104(wig, initialDateTime, expectedInserts);
        }

        private void TestDailyKGHMFor20191104(DateTime initialDateTime)
        {
            StockDefinition kghm = new StockDefinition() { ID = 125, Name = "KGHM", Type = StockType.Stock, StockName = "KGHM" };
            List<string> expectedInserts = new List<string>() {
                "insert into at_dzienne0(fk_id_spolki, ts, open, high, low, close, refcourse, volume) values (125, to_date('2019-11-04', 'YYYY-MM-DD'), 85.3200, 89.0000, 85.1200, 88.9200, 83.6400, 974976)",
                "insert into at_dzienne0(fk_id_spolki, ts, open, high, low, close, refcourse, volume) values (125, to_date('2019-11-05', 'YYYY-MM-DD'), 89.9000, 92.6200, 89.3800, 91.6400, 88.9200, 962176)",
                "insert into at_dzienne0(fk_id_spolki, ts, open, high, low, close, refcourse, volume) values (125, to_date('2019-11-06', 'YYYY-MM-DD'), 90.6400, 91.5400, 90.3000, 91.0000, 91.6400, 396956)",
                "insert into at_dzienne0(fk_id_spolki, ts, open, high, low, close, refcourse, volume) values (125, to_date('2019-11-07', 'YYYY-MM-DD'), 91.6000, 96.7000, 91.6000, 96.7000, 91.0000, 1425770)",
                "insert into at_dzienne0(fk_id_spolki, ts, open, high, low, close, refcourse, volume) values (125, to_date('2019-11-08', 'YYYY-MM-DD'), 95.5000, 96.9400, 94.6200, 96.4000, 96.7000, 897528)"
            };
            TestDailyFor20191104(kghm, initialDateTime, expectedInserts);
        }

        [Test]
        public void PumpDaily_WIG_DTAfterLastTSAndBeforeNextTS__CorrectInsertsExecuted()
        {
            TestDailyWIGFor20191104(new DateTime(2019, 11, 01));
        }

        [Test]
        public void PumpDaily_WIG_DTOnLastTS__CorrectInsertsExecuted()
        {
            TestDailyWIGFor20191104(new DateTime(2019, 10, 31));
        }

        [Test]
        public void PumpDaily_WIGThenKGHM__CorrectInsertsExecuted_FileDownloadedOnce()
        {
            TestDailyWIGFor20191104(new DateTime(2019, 10, 31));
            TestDailyKGHMFor20191104(new DateTime(2019, 10, 31));
        }

    }
}
