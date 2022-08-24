using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemDefs.SimplexStocks
{
    /// <summary>
    /// Multi stocks strategy with position balance based on risk optimization with simplex.
    /// </summary>
    public class SimplexMultiStocks : SystemDefinition
    {
        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _dataLoader;

        public SimplexMultiStocks(IStockDataProvider dataProvider, ISystemDataLoader dataLoader,
            ISlippage slippage, ICommission commission, ISystemExecutionLogger systemExecutionLogger)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
            _slippage = slippage;
            _commission = commission;
            _systemExecutionLogger = systemExecutionLogger;

            SystemParams.Set(SimplexMultiStocksParams.AvgProfitRange, 3);
            SystemParams.Set(SimplexMultiStocksParams.AvgChangeRange, 6);
            SystemParams.Set(SimplexMultiStocksParams.AcceptableSingleDD, 0.1);
            SystemParams.Set(SimplexMultiStocksParams.RiskSigmaMultiplier, 2.0);
            SystemParams.Set(SimplexMultiStocksParams.MaxSinglePositionSize, 0.8);
            SystemParams.Set(SimplexMultiStocksParams.MaxPortfolioRisk, 0.8);
            SystemParams.Set(SimplexMultiStocksParams.TruncateBalanceToNthPlace, 3);
        }

        public override void Prepare()
        {
            SignalsSimplexMultiStocks signals = new SignalsSimplexMultiStocks(_dataLoader, _dataProvider, _systemExecutionLogger, SystemParams);

            _dataDefinitionProvider = signals;
            _signalGeneratorOnOpen = null;
            _signalGeneratorOnClose = signals;
            //_commission = null;
            //_slippage = null;
        }
    }
}
