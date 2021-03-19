using NUnit.Framework;
using Shouldly;
using MarketOps.SystemExecutor.Processor;
using MarketOps.StockData.Types;
using System;
using System.Collections.Generic;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemExecutor.Tests.Mocks;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemExecutor.Tests.Processor
{
    [TestFixture]
    public class PositionsRebalancerTests
    {
        private readonly DateTime LastDate = DateTime.Now.Date;
        private const int PricesCount = 10;
        private const float InitialCash = 10000;
        private const float Price = 10;
        private const float PositionVolume = 10;
        private readonly StockDefinition _stock = new StockDefinition() { ID = 1, Type = StockType.InvestmentFund };
        private readonly StockDefinition _stock2 = new StockDefinition() { ID = 2, Type = StockType.InvestmentFund };

        private PositionsRebalancer TestObj;
        private ISystemDataLoader _dataLoader;
        private ICommission _commission;
        private ISlippage _slippage;

        private bool _openPriceLevelCalled;

        [SetUp]
        public void SetUp()
        {
            _dataLoader = SystemDataLoaderUtils.CreateSubstitute(PricesCount, LastDate);
            _commission = CommissionUtils.CreateSubstitute();
            _slippage = SlippageUtils.CreateSusbstitute();
            TestObj = new PositionsRebalancer(_dataLoader, _commission, _slippage);
            _openPriceLevelCalled = false;
        }

        private SystemState CreateSystemState(int activePositions)
        {
            SystemState res = new SystemState() { Cash = InitialCash };
            for (int i = 0; i < activePositions; i++)
                res.PositionsActive.Add(new Position() { Stock = _stock, Direction = PositionDir.Long, Volume = PositionVolume });
            return res;
        }

        [Test]
        public void Rebalance_EmptyNewBalance__AllClosed_NoNewActive([Range(0, 2)] int activePositions)
        {
            SystemState systemState = CreateSystemState(activePositions);

            TestObj.Rebalance(
                new Signal()
                {
                    Rebalance = true,
                    NewBalance = new List<(StockDefinition stockDef, float balance)>()
                },
                LastDate, systemState,
                (_, __, ___) => { _openPriceLevelCalled = true; return Price; });

            _openPriceLevelCalled.ShouldBe(activePositions > 0);
            systemState.PositionsClosed.Count.ShouldBe(activePositions);
            systemState.PositionsActive.Count.ShouldBe(0);
            systemState.Cash.ShouldBe(InitialCash + Price * PositionVolume * activePositions);
        }

        [Test]
        public void Rebalance_ZeroValueNewBalance__AllClosed_NoNewActive([Range(0, 2)] int activePositions)
        {
            SystemState systemState = CreateSystemState(activePositions);

            TestObj.Rebalance(
                new Signal()
                {
                    Rebalance = true,
                    NewBalance = new List<(StockDefinition stockDef, float balance)>()
                    {
                        (_stock, 0)
                    }
                },
                LastDate, systemState,
                (_, __, ___) => { _openPriceLevelCalled = true; return Price; });

            _openPriceLevelCalled.ShouldBe(activePositions > 0);
            systemState.PositionsClosed.Count.ShouldBe(activePositions);
            systemState.PositionsActive.Count.ShouldBe(0);
            systemState.Cash.ShouldBe(InitialCash + Price * PositionVolume * activePositions);
        }

        [Test]
        public void Rebalance_OneStockInNewBalance__ClosedAllPrevPositions_OneNewActive([Range(0, 2)] int activePositions)
        {
            const float newBalance = 0.4f;
            SystemState systemState = CreateSystemState(activePositions);

            TestObj.Rebalance(
                new Signal()
                {
                    Rebalance = true,
                    NewBalance = new List<(StockDefinition stockDef, float balance)>()
                    {
                        (_stock, newBalance)
                    }
                },
                LastDate, systemState,
                (_, __, ___) => { _openPriceLevelCalled = true; return Price; });

            _openPriceLevelCalled.ShouldBeTrue();
            systemState.PositionsClosed.Count.ShouldBe(activePositions);
            systemState.PositionsActive.Count.ShouldBe(1);
            float totalValue = InitialCash + Price * PositionVolume * activePositions;
            systemState.Cash.ShouldBe(totalValue * (1 - newBalance));
            systemState.PositionsActive[0].Volume.ShouldBe(totalValue * newBalance / Price);
        }

        [Test]
        public void Rebalance_TwoStocksInNewBalance__ClosedAllPrevPositions_TwoNewActive([Range(0, 2)] int activePositions)
        {
            const float newBalance = 0.4f;
            const float newBalance2 = 0.2f;
            SystemState systemState = CreateSystemState(activePositions);

            TestObj.Rebalance(
                new Signal()
                {
                    Rebalance = true,
                    NewBalance = new List<(StockDefinition stockDef, float balance)>()
                    {
                        (_stock, newBalance),
                        (_stock2, newBalance2)
                    }
                },
                LastDate, systemState,
                (_, __, ___) => { _openPriceLevelCalled = true; return Price; });

            _openPriceLevelCalled.ShouldBeTrue();
            systemState.PositionsClosed.Count.ShouldBe(activePositions);
            systemState.PositionsActive.Count.ShouldBe(2);
            float totalValue = InitialCash + Price * PositionVolume * activePositions;
            systemState.Cash.ToString().ShouldBe((totalValue * (1 - (newBalance + newBalance2))).ToString());
            systemState.PositionsActive[0].Volume.ShouldBe(totalValue * newBalance / Price);
            systemState.PositionsActive[1].Volume.ShouldBe(totalValue * newBalance2 / Price);
        }
    }
}
