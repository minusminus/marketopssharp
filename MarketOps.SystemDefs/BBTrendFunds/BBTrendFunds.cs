using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemDefs.BBTrendFunds
{
    /// <summary>
    /// Two funds strategy with BB trends.
    /// </summary>
    public class BBTrendFunds : SystemDefinition
    {
        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _dataLoader;

        public BBTrendFunds(IStockDataProvider dataProvider, ISystemDataLoader dataLoader,
            ISlippage slippage, ICommission commission, ISystemExecutionLogger systemExecutionLogger)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
            _slippage = slippage;
            _commission = commission;
            _systemExecutionLogger = systemExecutionLogger;

            //SystemParams.Set(BBTrendParams.StockName, "");
            //SystemParams.Set(BBTrendParams.BBPeriod, 20);
            //SystemParams.Set(BBTrendParams.BBSigmaWidth, 2f);
        }

        public override void Prepare()
        {
            SignalsBBTrendFunds signals = new SignalsBBTrendFunds(_dataLoader, _dataProvider, _systemExecutionLogger);

            _dataDefinitionProvider = signals;
            _signalGeneratorOnOpen = null;
            _signalGeneratorOnClose = signals;
            //_commission = null;
            //_slippage = null;
        }
    }
}
