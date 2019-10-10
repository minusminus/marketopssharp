using System;
using NUnit.Framework;
using Shouldly;
using NSubstitute;
using MarketOps.DataPump.DBWriters;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.DataPump.Types;

namespace MarketOps.DataPump.Tests.DBWriter
{
    [TestFixture]
    public class InsertCommandGeneratorTests
    {
        private InsertCommandGenerator TestObj;

        private IDataPumpProvider _dataPumpProvider;
        private readonly StockDefinition stockDefinition = new StockDefinition() { ID = 1, Type = StockType.Stock };
        private readonly DataPumpStockData stockData = new DataPumpStockData() { O = "1.1", H = "5,5", L = "0.4", C = "4.3", V = "123456", TS = new DateTime(2019, 01, 30) };

        [SetUp]
        public void SetUp()
        {
            _dataPumpProvider = Substitute.For<IDataPumpProvider>();
            _dataPumpProvider.GetTableName(StockType.Stock, StockDataRange.Day, 0).Returns("test_dzienne");
            TestObj = new InsertCommandGenerator(_dataPumpProvider);
        }

        [Test]
        public void InsertDaily__ReturnsQuery()
        {
            const string expectedQuery = "insert into test_dzienne(fk_id_spolki, ts, open, high, low, close, volume) values (1, to_date('2019-01-30', 'YYYY-MM-DD'), 1.1, 5.5, 0.4, 4.3, 123456)";
            TestObj.InsertDaily(stockData, stockDefinition).ShouldBe(expectedQuery);
        }
    }
}
