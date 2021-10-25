using NUnit.Framework;
using NSubstitute;
using MarketOps.DataPump.DBWriters;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;

namespace MarketOps.Tests.DataPump.DBWriter
{
    [TestFixture]
    public class StartTSUpdaterTests
    {
        private StartTSUpdater TestObj;

        private IDataPumpProvider _dataPumpProvider;

        [SetUp]
        public void SetUp()
        {
            _dataPumpProvider = Substitute.For<IDataPumpProvider>();
            _dataPumpProvider.GetTableName(StockType.Stock, StockDataRange.Daily, 0).Returns("test_dzienne");
            TestObj = new StartTSUpdater(_dataPumpProvider);
        }

        [Test]
        public void Update__ExecutesSql()
        {
            TestObj.Update(StockType.Stock);
            _dataPumpProvider.Received().GetTableName(StockType.Stock, StockDataRange.Daily, 0);
            _dataPumpProvider.Received().ExecuteSQL(Arg.Compat.Any<string>());
        }
    }
}
