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
    public class DataPumpStockDataToDBWriterTests
    {
        private DataPumpStockDataToDBWriter TestObj;

        private IDataPumpProvider _dataPumpProvider;
        private InsertCommandGenerator _commandGenerator;
        private readonly StockDefinition stockDefinition = new StockDefinition() { ID = 1, Type = StockType.Stock };
        private readonly DataPumpStockData stockData = new DataPumpStockData() { O = "1.1", H = "5,5", L = "0.4", C = "4.3", RefCourse = "1.0", V = "123456", TS = new DateTime(2019, 01, 30) };

        [SetUp]
        public void Setup()
        {
            _dataPumpProvider = Substitute.For<IDataPumpProvider>();
            _dataPumpProvider.GetTableName(StockType.Stock, StockDataRange.Daily, 0).Returns("test_dzienne");
            _commandGenerator = new InsertCommandGenerator(_dataPumpProvider);
            TestObj = new DataPumpStockDataToDBWriter(_dataPumpProvider, _commandGenerator);
        }

        [Test]
        public void StartSession__DoesNothing()
        {
            TestObj.StartSession();
            _dataPumpProvider.DidNotReceiveWithAnyArgs().ExecuteSQL(Arg.Compat.Any<string>());
            _dataPumpProvider.DidNotReceiveWithAnyArgs().GetTableName(Arg.Compat.Any<StockType>(), Arg.Compat.Any<StockDataRange>(), Arg.Compat.Any<int>());
        }

        [Test]
        public void EndSession__DoesNothing()
        {
            TestObj.EndSession();
            _dataPumpProvider.DidNotReceiveWithAnyArgs().ExecuteSQL(Arg.Compat.Any<string>());
            _dataPumpProvider.DidNotReceiveWithAnyArgs().GetTableName(Arg.Compat.Any<StockType>(), Arg.Compat.Any<StockDataRange>(), Arg.Compat.Any<int>());
        }

        [Test]
        public void WriteDaily__ExecutesSql()
        {
            TestObj.WriteDaily(stockData, stockDefinition);
            _dataPumpProvider.Received().GetTableName(stockDefinition.Type, StockDataRange.Daily, 0);
            _dataPumpProvider.Received().ExecuteSQL(Arg.Compat.Any<string>());
        }
    }
}
