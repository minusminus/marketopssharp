using NUnit.Framework;
using Shouldly;
using MarketOps.SystemExecutor.Processor;
using MarketOps.StockData.Types;
using System;
using System.Collections.Generic;
using MarketOps.SystemData.Interfaces;
using System.Linq;
using MarketOps.SystemExecutor.Tests.Mocks;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemExecutor.Tests.Processor
{
    [TestFixture]
    public class PositionsRebalancerTests
    {
        private readonly DateTime LastDate = DateTime.Now.Date;
        private const int PricesCount = 10;

        private PositionsRebalancer TestObj;
        private ISystemDataLoader _dataLoader;
        private ICommission _commission;
        private ISlippage _slippage;

        [SetUp]
        public void SetUp()
        {
            _dataLoader = SystemDataLoaderUtils.CreateSubstitute(PricesCount, LastDate);
            _commission = CommissionUtils.CreateSubstitute();
            _slippage = SlippageUtils.CreateSusbstitute();
            TestObj = new PositionsRebalancer(_dataLoader, _commission, _slippage);
        }
    }
}
