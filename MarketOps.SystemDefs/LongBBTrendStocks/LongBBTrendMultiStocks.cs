using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using MarketOps.SystemExecutor.GPW;
using MarketOps.SystemExecutor.MM;

namespace MarketOps.SystemDefs.LongBBTrendStocks
{
    /// <summary>
    /// Catching long trends on stocks (on monthly data) with BB(5, 2).
    /// Multi stocks strategy.
    /// </summary>
    public class LongBBTrendMultiStocks : SystemDefinition
    {
        private const int TrailingStopTicksBelow = 2;
        private const int TrailingStopMinOfL = 5;

        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _dataLoader;
        private readonly GPWTickOps _gpwTickOps = new GPWTickOps();

        public LongBBTrendMultiStocks(IStockDataProvider dataProvider, ISystemDataLoader dataLoader,
            ISlippage slippage, ICommission commission, ISystemExecutionLogger systemExecutionLogger)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
            _slippage = slippage;
            _commission = commission;
            _systemExecutionLogger = systemExecutionLogger;

            //SystemParams.Set(LongBBTrendStocksParams.StockName, "");
            SystemParams.Set(LongBBTrendStocksParams.BBPeriod, 5);
            SystemParams.Set(LongBBTrendStocksParams.BBSigmaWidth, 2f);
            SystemParams.Set(LongBBTrendStocksParams.ATRWidth, 10);
        }

        public override void Prepare()
        {
            SignalsLongBBTrendMultiStocks signals = new SignalsLongBBTrendMultiStocks(
                StockData.Types.StockDataRange.Monthly,
                SystemParams.Get(LongBBTrendStocksParams.BBPeriod).As<int>(),
                SystemParams.Get(LongBBTrendStocksParams.BBSigmaWidth).As<float>(),
                SystemParams.Get(LongBBTrendStocksParams.ATRWidth).As<int>(),
                _dataLoader, _dataProvider,
                new MMSignalVolumeForSystemValuePercent(0.05f, _commission, _dataLoader),// new MMSignalVolumeOneItem(),
                _gpwTickOps
                );
            MMTrailingStopMinMaxOfN trailingStopCalculator = new MMTrailingStopMinMaxOfN(TrailingStopMinOfL, 0, TrailingStopTicksBelow, _dataLoader, _gpwTickOps);

            _dataDefinitionProvider = signals;
            _signalGeneratorOnOpen = null;
            _signalGeneratorOnClose = signals;
            //_commission = null;
            //_slippage = null;
            _mmPositionCloseCalculator = trailingStopCalculator;// null;
        }
    }
}
