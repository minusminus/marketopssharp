using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using NSubstitute;
using MarketOps.DataGen.DataGenerators;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;

namespace MarketOps.DataGen.Tests.DataGenerators
{
    [TestFixture]
    public class DataAggregatorTests
    {
        private DataAggregator TestObj;

        private IDataGenProvider _dataGenProvider;
        private readonly StockDefinition _stockDefinition = new StockDefinition() { ID = 1, Type = StockType.Stock };
        private readonly List<string> _executedQueries = new List<string>();

        private const string TblDaily = "test_dzienne";
        private const string TblWeekly = "test_tyg";
        private const string TblMonthly = "test_mies";

        [SetUp]
        public void SetUp()
        {
            _executedQueries.Clear();
            _dataGenProvider = Substitute.For<IDataGenProvider>();
            _dataGenProvider.GetTableName(StockType.Stock, StockDataRange.Daily, 0).Returns(TblDaily);
            _dataGenProvider.GetTableName(StockType.Stock, StockDataRange.Weekly, 0).Returns(TblWeekly);
            _dataGenProvider.GetTableName(StockType.Stock, StockDataRange.Monthly, 0).Returns(TblMonthly);
            _dataGenProvider.ExecuteSQL(Arg.Compat.Do<string>(s => _executedQueries.Add(s)));

            TestObj = new DataAggregator(_dataGenProvider);
        }

        private bool CheckQueryPart(int queryNo, string queryPart)
        {
            return _executedQueries[queryNo].Contains(queryPart);
        }

        [Test]
        public void GenerateWeekly_NoDataExists__ExecutesQuery()
        {
            _dataGenProvider.GetMaxTS(Arg.Compat.Any<StockDefinition>(), StockDataRange.Weekly, 0)
                .Returns(DateTime.MinValue);
            TestObj.GenerateWeekly(_stockDefinition);
            _executedQueries.Count.ShouldBe(1);
            CheckQueryPart(0, $"insert into {TblWeekly}").ShouldBeTrue();
            CheckQueryPart(0, $"from {TblDaily}").ShouldBeTrue();
        }

        [Test]
        public void GenerateWeekly_DataExists__ExecutesQuery()
        {
            _dataGenProvider.GetMaxTS(Arg.Compat.Any<StockDefinition>(), StockDataRange.Weekly, 0)
                .Returns(new DateTime(2019, 1, 1));
            TestObj.GenerateWeekly(_stockDefinition);
            _executedQueries.Count.ShouldBe(2);
            CheckQueryPart(0, $"delete {TblWeekly}").ShouldBeTrue();
            CheckQueryPart(1, $"insert into {TblWeekly}").ShouldBeTrue();
            CheckQueryPart(1, $"from {TblDaily}").ShouldBeTrue();
        }

        [Test]
        public void GenerateMonthly_NoDataExists__ExecutesQuery()
        {
            _dataGenProvider.GetMaxTS(Arg.Compat.Any<StockDefinition>(), StockDataRange.Monthly, 0)
                .Returns(DateTime.MinValue);
            TestObj.GenerateMonthly(_stockDefinition);
            _executedQueries.Count.ShouldBe(1);
            CheckQueryPart(0, $"insert into {TblMonthly}").ShouldBeTrue();
            CheckQueryPart(0, $"from {TblDaily}").ShouldBeTrue();
        }

        [Test]
        public void GenerateMonthly_DataExists__ExecutesQuery()
        {
            _dataGenProvider.GetMaxTS(Arg.Compat.Any<StockDefinition>(), StockDataRange.Monthly, 0)
                .Returns(new DateTime(2019, 1, 1));
            TestObj.GenerateMonthly(_stockDefinition);
            _executedQueries.Count.ShouldBe(2);
            CheckQueryPart(0, $"delete {TblMonthly}").ShouldBeTrue();
            CheckQueryPart(1, $"insert into {TblMonthly}").ShouldBeTrue();
            CheckQueryPart(1, $"from {TblDaily}").ShouldBeTrue();
        }
    }
}
