using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemDefs.NTopFundsAll
{
    /// <summary>
    /// N top funds strategy for all provided stocks, positions equally risk balanced.
    /// </summary>
    public class NTopFundsAll : SystemDefinition
    {
        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _dataLoader;

        public NTopFundsAll(IStockDataProvider dataProvider, ISystemDataLoader dataLoader,
            ISlippage slippage, ICommission commission, ISystemExecutionLogger systemExecutionLogger)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
            _slippage = slippage;
            _commission = commission;
            _systemExecutionLogger = systemExecutionLogger;

            SystemParams.Set(NTopFundsAllParams.N, 8);
            SystemParams.Set(NTopFundsAllParams.AvgProfitRange, 2);
            SystemParams.Set(NTopFundsAllParams.AvgChangeRange, 2);
        }

        public override void Prepare()
        {
            SignalsNTopFundsAll signals = new SignalsNTopFundsAll(_dataLoader, _dataProvider, _systemExecutionLogger, SystemParams);

            _dataDefinitionProvider = signals;
            _signalGeneratorOnOpen = null;
            _signalGeneratorOnClose = signals;
            //_commission = null;
            //_slippage = null;
        }
    }
}
