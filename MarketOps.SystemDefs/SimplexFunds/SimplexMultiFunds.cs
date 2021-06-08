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
        }

        public override void Prepare()
        {
            SignalsSimplexMultiFunds signals = new SignalsSimplexMultiFunds(_dataLoader, _dataProvider, _systemExecutionLogger);

            _dataDefinitionProvider = signals;
            _signalGeneratorOnOpen = null;
            _signalGeneratorOnClose = signals;
            //_commission = null;
            //_slippage = null;
            _mmPositionCloseCalculator = null;
        }
    }
}
