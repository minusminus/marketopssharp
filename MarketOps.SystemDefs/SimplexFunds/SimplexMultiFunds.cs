using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;

namespace MarketOps.SystemDefs.SimplexFunds
{
    /// <summary>
    /// Multi funds strategy with position balance based on risk optimization with simplex.
    /// </summary>
    public class SimplexMultiFunds : SystemDefinition
    {
        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _dataLoader;

        public SimplexMultiFunds(IStockDataProvider dataProvider, ISystemDataLoader dataLoader,
            ISlippage slippage, ICommission commission, ISystemExecutionLogger systemExecutionLogger)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
            _slippage = slippage;
            _commission = commission;
            _systemExecutionLogger = systemExecutionLogger;

            SystemParams.Set(SimplexMultiFundsParams.AvgProfitRange, 3);
            SystemParams.Set(SimplexMultiFundsParams.AvgChangeRange, 6);
            SystemParams.Set(SimplexMultiFundsParams.AcceptableSingleDD, 0.1);
            SystemParams.Set(SimplexMultiFundsParams.RiskSigmaMultiplier, 2.0);
            SystemParams.Set(SimplexMultiFundsParams.MaxSinglePositionSize, 0.8);
            SystemParams.Set(SimplexMultiFundsParams.MaxPortfolioRisk, 0.8);
            SystemParams.Set(SimplexMultiFundsParams.TruncateBalanceToNthPlace, 3);
        }

        public override void Prepare()
        {
            SignalsSimplexMultiFunds signals = new SignalsSimplexMultiFunds(_dataLoader, _dataProvider, _systemExecutionLogger, SystemParams);

            _dataDefinitionProvider = signals;
            _signalGeneratorOnOpen = null;
            _signalGeneratorOnClose = signals;
            //_commission = null;
            //_slippage = null;
            _mmPositionCloseCalculator = null;
        }
    }
}
