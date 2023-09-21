using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemDefs.NTopFunds
{
    /// <summary>
    /// N top funds strategy, positions equally risk balanced.
    /// </summary>
    public class NTopFunds : SystemDefinition
    {
        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _dataLoader;

        public NTopFunds(IStockDataProvider dataProvider, ISystemDataLoader dataLoader,
            ISlippage slippage, ICommission commission, ISystemExecutionLogger systemExecutionLogger)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
            _slippage = slippage;
            _commission = commission;
            _systemExecutionLogger = systemExecutionLogger;

            SystemParams.Set(NTopFundsParams.N, 3);
            SystemParams.Set(NTopFundsParams.AvgProfitRange, 3);
            SystemParams.Set(NTopFundsParams.AvgChangeRange, 3);
        }

        public override void Prepare()
        {
            SignalsNTopFunds signals = new SignalsNTopFunds(_dataLoader, _dataProvider, _systemExecutionLogger, SystemParams);

            _dataDefinitionProvider = signals;
            _signalGeneratorOnOpen = null;
            _signalGeneratorOnClose = signals;
            //_commission = null;
            //_slippage = null;
        }
    }
}
