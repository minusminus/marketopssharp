using MarketOps.System.Processor;
using MarketOps.System.Tests.Mocks;
using NUnit.Framework;
using Shouldly;
using NSubstitute;
using MarketOps.StockData.Interfaces;
using MarketOps.System.Interfaces;
using System;
using MarketOps.StockData.Types;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.System.Tests.Processor
{
    [TestFixture]
    public class SystemProcessorTests
    {
        private readonly DateTime LastDate = DateTime.Now.Date;
        private readonly int PricesCount = 10;
        private readonly float InitialCash = 10000;

        private static readonly StockDefinition _stock = new StockDefinition() { ID = 1 };
        private static readonly StockStatMock _stockStatMock = new StockStatMock("", 0);

        private IStockDataProvider _dataProvider;
        private IDataLoader _dataLoader;
        private ISystemDataDefinitionProvider _dataDefinitionProvider;
        private ISignalGeneratorOnOpen _signalGeneratorOnOpen;
        private ISignalGeneratorOnClose _signalGeneratorOnClose;
        private ICommission _commission;
        private ISlippage _slippage;
        private IMMPositionCloseCalculator _mmPositionCloseCalculator;

        private SystemProcessor TestObj;

        [SetUp]
        public void SetUp()
        {
            _dataProvider = StockDataProviderUtils.CreateSubstitute(DateTime.MinValue);
            _dataLoader = DataLoaderUtils.CreateSubstitute(PricesCount, LastDate);
            _dataDefinitionProvider = Substitute.For<ISystemDataDefinitionProvider>();
            _signalGeneratorOnOpen = Substitute.For<ISignalGeneratorOnOpen>();
            _signalGeneratorOnClose = Substitute.For<ISignalGeneratorOnClose>();
            _commission = CommissionUtils.CreateSubstitute();
            _slippage = SlippageUtils.CreateSusbstitute();
            _mmPositionCloseCalculator = Substitute.For<IMMPositionCloseCalculator>();

            TestObj = new SystemProcessor(_dataProvider, _dataLoader, _dataDefinitionProvider, _signalGeneratorOnOpen, _signalGeneratorOnClose, _commission, _slippage, _mmPositionCloseCalculator);

            _dataDefinitionProvider.GetDataDefinition().Returns(
                new SystemDataDefinition()
                {
                    stocks = new List<SystemStockDataDefinition>() {
                        new SystemStockDataDefinition()
                        {
                            stock = _stock,
                            dataRange = StockDataRange.Daily,
                            stats = new List<StockStat>() { _stockStatMock }
                        }
                    }
                }
                );
            _stockStatMock.CalculateCallCount = 0;
        }

        private void CheckEmptyEquity(SystemEquity equity)
        {
            equity.Cash.ShouldBe(InitialCash);
            equity.PositionsActive.Count.ShouldBe(0);
            equity.PositionsClosed.Count.ShouldBe(0);
            equity.ClosedPositionsValue.Count.ShouldBe(0);
            equity.Value.Count.ShouldBe(PricesCount);
            equity.Value.All(x => x.Value == InitialCash).ShouldBeTrue();
            Position pos = new Position();
            _mmPositionCloseCalculator.DidNotReceiveWithAnyArgs().CalculateCloseMode(ref pos, default);
            _stockStatMock.CalculateCallCount.ShouldBe(1);
        }

        [Test, Combinatorial]
        public void Process_NoSignals__EmptyEquity([Values(false, true)] bool withGeneratorOnOpen, [Values(false, true)] bool withGeneratorOnClose)
        {
            _signalGeneratorOnOpen.GenerateOnOpen(default, default).ReturnsForAnyArgs(new List<Signal>());
            _signalGeneratorOnClose.GenerateOnClose(default, default).ReturnsForAnyArgs(new List<Signal>());

            TestObj = new SystemProcessor(_dataProvider, _dataLoader, _dataDefinitionProvider, (withGeneratorOnOpen ? _signalGeneratorOnOpen : null), (withGeneratorOnClose ? _signalGeneratorOnClose : null), _commission, _slippage, _mmPositionCloseCalculator);
            CheckEmptyEquity(TestObj.Process(LastDate.AddDays(-PricesCount), LastDate, InitialCash));
        }
    }
}
