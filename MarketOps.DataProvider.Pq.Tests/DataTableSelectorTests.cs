using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.DataProvider.Pg;
using MarketOps.StockData.Types;

namespace MarketOps.DataProvider.Pq.Tests
{
    [TestFixture]
    public class DataTableSelectorTests
    {
        private readonly DataTableSelector TestObj = new DataTableSelector();

        [Test]
        public void UndefinedStockType_Throws()
        {
            Should.Throw<Exception>(() => { TestObj.GetTableName(StockType.Undefined, StockDataRange.Daily, 0); });
        }

        [Test]
        public void UndefinedDataRange_Throws()
        {
            Should.Throw<Exception>(() => { TestObj.GetTableName(StockType.Stock, StockDataRange.Undefined, 0); });
        }

        [Test]
        public void UndefinedIntradayInterval_Throws()
        {
            Should.Throw<Exception>(() => { TestObj.GetTableName(StockType.Stock, StockDataRange.Intraday, 0); });
        }

        private StockType[] stdTestStockTypes = new StockType[6] { StockType.Stock, StockType.Index, StockType.IndexFuture, StockType.InvestmentFund, StockType.NBPCurrency, StockType.Forex };

        [Test]
        public void DataDaily()
        {
            string[] expected = new string[6] { "at_dzienne0", "at_dzienne1", "at_dzienne2", "at_dzienne4", "at_dzienne5", "at_dzienne6" };
            for (int i = 0; i < stdTestStockTypes.Length; i++)
                TestObj.GetTableName(stdTestStockTypes[i], StockDataRange.Daily, 0).ShouldBe(expected[i]);
        }

        [Test]
        public void DataWeekly()
        {
            string[] expected = new string[6] { "at_tyg0", "at_tyg1", "at_tyg2", "at_tyg4", "at_tyg5", "at_tyg6" };
            for (int i = 0; i < stdTestStockTypes.Length; i++)
                TestObj.GetTableName(stdTestStockTypes[i], StockDataRange.Weekly, 0).ShouldBe(expected[i]);
        }

        [Test]
        public void DataMonthly()
        {
            string[] expected = new string[6] { "at_mies0", "at_mies1", "at_mies2", "at_mies4", "at_mies5", "at_mies6" };
            for (int i = 0; i < stdTestStockTypes.Length; i++)
                TestObj.GetTableName(stdTestStockTypes[i], StockDataRange.Monthly, 0).ShouldBe(expected[i]);
        }

        [Test]
        public void DataTicks()
        {
            string[] expected = new string[6] { "at_ciagle0", "at_ciagle1", "at_ciagle2", "at_ciagle4", "at_ciagle5", "at_ciagle6" };
            for (int i = 0; i < stdTestStockTypes.Length; i++)
                TestObj.GetTableName(stdTestStockTypes[i], StockDataRange.Tick, 0).ShouldBe(expected[i]);
        }

        [Test]
        public void DataIntraday()
        {
            int[] intradayRanges = new int[] { 1, 2, 5, 10, 15, 30, 60 };
            for (int i = 0; i < stdTestStockTypes.Length; i++)
                for (int j = 0; j < intradayRanges.Length; j++)
                    TestObj.GetTableName(stdTestStockTypes[i], StockDataRange.Intraday, intradayRanges[j])
                        .ShouldBe($"at_intra{intradayRanges[j]}m{(int)stdTestStockTypes[i]}");
        }
    }
}
