using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.SystemExecutor;
using MarketOps.SystemExecutor.Interfaces;

namespace MarketOps.SystemDefs.PriceCrossingSMA
{
    /// <summary>
    /// Price crossing sma up or down.
    /// </summary>
    public class PriceCrossingSMA : SystemDefinition
    {
        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _dataLoader;

        public PriceCrossingSMA(IStockDataProvider dataProvider, ISystemDataLoader dataLoader,
            ISlippage slippage, ICommission commission)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
            _slippage = slippage;
            _commission = commission;

            SystemParams.Set(PriceCrossingSMAParams.StockName, "");
            SystemParams.Set(PriceCrossingSMAParams.SMAPeriod, 20);
        }

        public override void Prepare()
        {
            SignalsPriceCrossingSMA signals = new SignalsPriceCrossingSMA(
                SystemParams.Get(PriceCrossingSMAParams.StockName).As<string>(),
                StockData.Types.StockDataRange.Daily,
                SystemParams.Get(PriceCrossingSMAParams.SMAPeriod).As<int>(),
                _dataLoader, _dataProvider);

            _dataDefinitionProvider = signals;
            _signalGeneratorOnOpen = null;
            _signalGeneratorOnClose = signals;
            //_commission = null;
            //_slippage = null;
            _mmPositionCloseCalculator = null;
        }
    }
}
