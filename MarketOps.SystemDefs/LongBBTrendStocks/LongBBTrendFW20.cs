using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using MarketOps.SystemExecutor.GPW;
using MarketOps.SystemExecutor.MM;

namespace MarketOps.SystemDefs.LongBBTrendStocks
{
    /// <summary>
    /// Catching long trends on fw20 with BB
    /// </summary>
    public class LongBBTrendFW20 : SystemDefinition
    {
        private const int TrailingStopTicksBelow = 2;
        private const int TrailingStopMinOfL = 5;

        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _dataLoader;
        private readonly GPWTickOps _gpwTickOps = new GPWTickOps();

        public LongBBTrendFW20(IStockDataProvider dataProvider, ISystemDataLoader dataLoader,
            ISlippage slippage, ICommission commission, ISystemExecutionLogger systemExecutionLogger)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
            _slippage = slippage;
            _commission = commission;
            _systemExecutionLogger = systemExecutionLogger;

            SystemParams.Set(LongBBTrendStocksParams.StockName, "");
            SystemParams.Set(LongBBTrendStocksParams.BBPeriod, 5);
            SystemParams.Set(LongBBTrendStocksParams.BBSigmaWidth, 2f);
            SystemParams.Set(LongBBTrendStocksParams.ATRWidth, 10);
        }

        public override void Prepare()
        {
            SignalsLongBBTrendStocks signals = new SignalsLongBBTrendStocks(
                //SystemParams.Get(LongBBTrendStocksParams.StockName).As<string>(),
                //StockData.Types.StockDataRange.Monthly,
                "FW20",
                StockData.Types.StockDataRange.Daily,
                SystemParams.Get(LongBBTrendStocksParams.BBPeriod).As<int>(),
                SystemParams.Get(LongBBTrendStocksParams.BBSigmaWidth).As<float>(),
                SystemParams.Get(LongBBTrendStocksParams.ATRWidth).As<int>(),
                _dataLoader, _dataProvider,
                new MMSignalVolumeForAllCash(_commission),
                new MMTrailingStopMinMaxOfN(TrailingStopMinOfL, 0, TrailingStopTicksBelow, _dataLoader, _gpwTickOps),
                _gpwTickOps,
                _systemExecutionLogger
                );

            _dataDefinitionProvider = signals;
            _signalGeneratorOnOpen = null;
            _signalGeneratorOnClose = signals;
            //_commission = null;
            //_slippage = null;
        }
    }
}
