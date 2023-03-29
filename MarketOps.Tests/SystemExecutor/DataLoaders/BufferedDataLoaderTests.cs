using System;
using MarketOps.StockData.Interfaces;
using NUnit.Framework;
using Shouldly;
using NSubstitute;
using MarketOps.StockData.Types;
using MarketOps.SystemExecutor.DataLoaders;

namespace MarketOps.Tests.SystemExecutor.DataLoaders
{
    [TestFixture]
    public class BufferedDataLoaderTests
    {
        private BufferedDataLoader _testObj;
        private IStockDataProvider _dataProvider;
        private int _getStockDefinitionCalls;
        private int _getPricesDataCalls;

        private const string Stock1 = "KGHM";
        private const string Stock2 = "PKOBP";
        private readonly DateTime TSFrom1 = new DateTime(2019, 02, 01);
        private readonly DateTime TSTo1 = new DateTime(2019, 03, 01);
        private readonly DateTime TSFrom2 = new DateTime(2019, 01, 01);
        private readonly DateTime TSTo2 = new DateTime(2019, 04, 01);

        [SetUp]
        public void SetUp()
        {
            _dataProvider = Substitute.For<IStockDataProvider>();
            _dataProvider.GetStockDefinition(Arg.Compat.Any<string>())
                .Returns((x) =>
                {
                    _getStockDefinitionCalls++;
                    return new StockDefinition();
                });
            _testObj = new BufferedDataLoader(_dataProvider);
            _getStockDefinitionCalls = 0;
            _getPricesDataCalls = 0;
        }

        private StockPricesData CreatePricesData(StockDataRange dataRange, int intradayInterval, DateTime tsFrom, DateTime tsTo)
        {
            return new StockPricesData(2)
            {
                Range = dataRange,
                IntradayInterval = intradayInterval,
                TS =
                {
                    [0] = tsFrom,
                    [1] = tsTo
                }
            };
        }

        private void SubstituteGetPricesData(DateTime tsFrom, DateTime tsTo)
        {
            _dataProvider.GetPricesData(Arg.Compat.Any<StockDefinition>(), StockDataRange.Daily, 0,
                Arg.Compat.Any<DateTime>(), Arg.Compat.Any<DateTime>())
                .Returns((x) =>
                {
                    _getPricesDataCalls++;
                    return CreatePricesData(StockDataRange.Daily, 0, tsFrom, tsTo);
                });
        }

        private void CheckPricesData(StockPricesData data, DateTime tsFrom, DateTime tsTo)
        {
            data.TS[0].ShouldBe(tsFrom);
            data.TS[1].ShouldBe(tsTo);
        }

        private void SubstituteEmptyGetPricesData()
        {
            _dataProvider.GetPricesData(Arg.Compat.Any<StockDefinition>(), StockDataRange.Daily, 0,
                Arg.Compat.Any<DateTime>(), Arg.Compat.Any<DateTime>())
                .Returns((x) =>
                {
                    _getPricesDataCalls++;
                    return new StockPricesData(0)
                    {
                        Range = StockDataRange.Daily,
                        IntradayInterval = 0
                    };
                });
        }

        private void CheckEmptyPricesData(StockPricesData data)
        {
            data.TS.Length.ShouldBe(0);
        }

        private void CheckDBAccess(int expectedStockDefinitionCalls, int expectedPricesDataCalls)
        {
            _getStockDefinitionCalls.ShouldBe(expectedStockDefinitionCalls);
            _getPricesDataCalls.ShouldBe(expectedPricesDataCalls);
        }

        [Test]
        public void Get_FirstTime__ReturnsData()
        {
            SubstituteGetPricesData(TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom1, TSTo1);
            CheckDBAccess(1, 1);
        }

        [Test]
        public void Get_SameDataTwice__ReturnsData_OnlyOneDBAccess()
        {
            SubstituteGetPricesData(TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom1, TSTo1);
            CheckDBAccess(1, 1);
        }

        [Test]
        public void Get_TwoStocks__ReturnsData_TwoDBAccesses()
        {
            SubstituteGetPricesData(TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock2, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom1, TSTo1);
            CheckDBAccess(2, 2);
        }

        [Test]
        public void Get_TwoStocks_GetTwice__ReturnsData_TwoDBAccesses()
        {
            SubstituteGetPricesData(TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock2, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock2, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom1, TSTo1);
            CheckDBAccess(2, 2);
        }

        [Test]
        public void Get_SecondGetBelowCurrent__ReturnsData()
        {
            SubstituteGetPricesData(TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom1, TSTo1);
            SubstituteGetPricesData(TSFrom2, TSTo1);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom2, TSTo1), TSFrom2, TSTo1);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom2, TSTo1), TSFrom2, TSTo1);
            CheckDBAccess(2, 2);
        }

        [Test]
        public void Get_SecondGetAboveCurrent__ReturnsData()
        {
            SubstituteGetPricesData(TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom1, TSTo1);
            SubstituteGetPricesData(TSFrom1, TSTo2);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo2), TSFrom1, TSTo2);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo2), TSFrom1, TSTo2);
            CheckDBAccess(2, 2);
        }

        [Test]
        public void Get_SecondGetOutOfCurrentRange__ReturnsData()
        {
            SubstituteGetPricesData(TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom1, TSTo1);
            SubstituteGetPricesData(TSFrom2, TSTo2);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom2, TSTo2), TSFrom2, TSTo2);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom2, TSTo2), TSFrom2, TSTo2);
            CheckDBAccess(2, 2);
        }

        [Test]
        public void Get_TwoStocks_OutOfCurrentRange__ReturnsData()
        {
            SubstituteGetPricesData(TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock2, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom1, TSTo1);
            SubstituteGetPricesData(TSFrom2, TSTo2);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom2, TSTo2), TSFrom2, TSTo2);
            CheckPricesData(_testObj.Get(Stock2, StockDataRange.Daily, 0, TSFrom2, TSTo2), TSFrom2, TSTo2);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom2, TSTo2), TSFrom2, TSTo2);
            CheckPricesData(_testObj.Get(Stock2, StockDataRange.Daily, 0, TSFrom2, TSTo2), TSFrom2, TSTo2);
            CheckDBAccess(4, 4);
        }

        [Test]
        public void Get_SecondGetInCurrentRange__ReturnsWiderDate()
        {
            SubstituteGetPricesData(TSFrom2, TSTo2);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom2, TSTo2), TSFrom2, TSTo2);
            SubstituteGetPricesData(TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom2, TSTo2);
            CheckDBAccess(1, 1);
        }

        [Test]
        public void Get_ExpandRangeTwice__ReturnsWiderData()
        {
            SubstituteGetPricesData(TSFrom1, TSTo1);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo1), TSFrom1, TSTo1);
            SubstituteGetPricesData(TSFrom1, TSTo2);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo2), TSFrom1, TSTo2);
            SubstituteGetPricesData(TSFrom2, TSTo2);
            CheckPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom2, TSTo2), TSFrom2, TSTo2);
            CheckDBAccess(3, 3);
        }

        [Test]
        public void Get_EmptyData__ReturnsEmptyData()
        {
            SubstituteEmptyGetPricesData();
            CheckEmptyPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo1));
        }

        [Test]
        public void Get_EmptyDataTwice__ReturnsEmptyDataTwice()
        {
            SubstituteEmptyGetPricesData();
            CheckEmptyPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo1));
            CheckEmptyPricesData(_testObj.Get(Stock1, StockDataRange.Daily, 0, TSFrom1, TSTo1));
        }
    }
}
