using NUnit.Framework;
using Shouldly;
using MarketOps.System.Processor;
using MarketOps.StockData.Types;
using System;
using MarketOps.System.Interfaces;
using MarketOps.System.Tests.Mocks;

namespace MarketOps.System.Tests.Processor
{
    [TestFixture]
    public class PositionsCloserTests
    {
        private readonly DateTime LastDate = DateTime.Now.Date;
        private readonly int PricesCount = 10;
        private readonly float InitialCash = 10000;

        private static readonly StockDefinition _stock = new StockDefinition() { ID = 1 };

        private PositionsCloser TestObj;
        private ISystemDataLoader _dataLoader;
        private ICommission _commission;
        private ISlippage _slippage;

        private bool _positionSelectorCalled;
        private bool _closePriceSelectorCalled;

        [SetUp]
        public void SetUp()
        {
            _dataLoader = SystemDataLoaderUtils.CreateSubstitute(PricesCount, LastDate);
            _commission = CommissionUtils.CreateSubstitute();
            _slippage = SlippageUtils.CreateSusbstitute();
            TestObj = new PositionsCloser(_dataLoader, _commission, _slippage);
            _positionSelectorCalled = false;
            _closePriceSelectorCalled = false;
        }

        private SystemState CreateEquity(PositionDir[] activeDirs, PositionCloseMode[] activeCloseModes)
        {
            activeDirs.Length.ShouldBe(activeCloseModes.Length);

            SystemState equity = new SystemState();
            equity.Cash = InitialCash;
            for (int i = 0; i < activeDirs.Length; i++)
                equity.PositionsActive.Add(new Position() { Stock = _stock, Direction = activeDirs[i], CloseMode = activeCloseModes[i] });
            return equity;
        }

        private void CheckProcessResult(SystemState equity, bool expectedSelectorCalled, bool expectedPriceSelectorCalled,
            int expectedPositionsActiveCount, int expectedPositionsClosedCount)
        {
            _positionSelectorCalled.ShouldBe(expectedSelectorCalled);
            _closePriceSelectorCalled.ShouldBe(expectedPriceSelectorCalled);
            equity.PositionsActive.Count.ShouldBe(expectedPositionsActiveCount);
            equity.PositionsClosed.Count.ShouldBe(expectedPositionsClosedCount);
            equity.ClosedPositionsEquity.Count.ShouldBe(expectedPositionsClosedCount);
        }

        [Test]
        public void Process_EmptyActivePositionsList__DoesNothing()
        {
            SystemState equity = new SystemState();
            TestObj.Process(LastDate, equity,
                (_, __, ___) => { _positionSelectorCalled = true; return true; },
                (_, __, ___) => { _closePriceSelectorCalled = true; return -1; });
            CheckProcessResult(equity, false, false, 0, 0);
        }

        [Test]
        public void Process_PositionSelectorReturnsFalse__DoesNothing()
        {
            SystemState equity = CreateEquity(
                new[] { PositionDir.Long, PositionDir.Long, PositionDir.Long },
                new[] { PositionCloseMode.OnOpen, PositionCloseMode.OnOpen, PositionCloseMode.OnOpen });
            TestObj.Process(LastDate, equity,
                (_, __, ___) => { _positionSelectorCalled = true; return false; },
                (_, __, ___) => { _closePriceSelectorCalled = true; return -1; });
            CheckProcessResult(equity, true, false, 3, 0);
        }

        [Test]
        public void Process_PositionSelectorReturnsTrue__PositionClosed()
        {
            SystemState equity = CreateEquity(
                new[] { PositionDir.Long },
                new[] { PositionCloseMode.OnStopHit });
            TestObj.Process(LastDate, equity,
                (_, __, ___) => { _positionSelectorCalled = true; return true; },
                (_, __, ___) => { _closePriceSelectorCalled = true; return 10; });
            CheckProcessResult(equity, true, true, 0, 1);
        }

        [Test]
        public void Process_PositionSelectorReturnsTrue_TwoPositions__PositionsClosed()
        {
            SystemState equity = CreateEquity(
                new[] { PositionDir.Long, PositionDir.Long },
                new[] { PositionCloseMode.OnStopHit, PositionCloseMode.OnStopHit });
            TestObj.Process(LastDate, equity,
                (_, __, ___) => { _positionSelectorCalled = true; return true; },
                (_, __, ___) => { _closePriceSelectorCalled = true; return 10; });
            CheckProcessResult(equity, true, true, 0, 2);
        }

        [Test]
        public void Process_PositionSelectorReturnsTrue_NotAllPositionsSelected__SelectedPositionsClosed()
        {
            SystemState equity = CreateEquity(
                new[] { PositionDir.Short, PositionDir.Long, PositionDir.Long, PositionDir.Short },
                new[] { PositionCloseMode.OnStopHit, PositionCloseMode.OnStopHit, PositionCloseMode.OnStopHit, PositionCloseMode.OnStopHit });
            TestObj.Process(LastDate, equity,
                (pos, __, ___) => { _positionSelectorCalled = true; return (pos.Direction == PositionDir.Long); },
                (_, __, ___) => { _closePriceSelectorCalled = true; return 10; });
            CheckProcessResult(equity, true, true, 2, 2);
        }
    }
}
