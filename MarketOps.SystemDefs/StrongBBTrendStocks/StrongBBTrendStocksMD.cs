using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using MarketOps.SystemExecutor.GPW;
using MarketOps.SystemExecutor.MM;

namespace MarketOps.SystemDefs.StrongBBTrendStocks
{
    /// <summary>
    /// Strong trends on stocks by BB breakout on monthly data, running trailing stop on daily data.
    /// (next step after LongBBTrendMultiStocks)
    /// </summary>
    public class StrongBBTrendStocksMD : SystemDefinition
    {
        private const int TrailingStopTicksBelow = 2;
        private const int TrailingStopMinOfL = 10;

        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _dataLoader;
        private readonly GPWTickOps _gpwTickOps = new GPWTickOps();

        public StrongBBTrendStocksMD(IStockDataProvider dataProvider, ISystemDataLoader dataLoader,
            ISlippage slippage, ICommission commission, ISystemExecutionLogger systemExecutionLogger)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
            _slippage = slippage;
            _commission = commission;
            _systemExecutionLogger = systemExecutionLogger;

            SystemParams.Set(StrongBBTrendStocksParams.StockName, "");
            SystemParams.Set(StrongBBTrendStocksParams.BBPeriod, 5);
            SystemParams.Set(StrongBBTrendStocksParams.BBSigmaWidth, 2f);
            SystemParams.Set(StrongBBTrendStocksParams.ATRWidth, 10);
        }

        public override void Prepare()
        {
            SignalsStrongBBTrendStocksMD signals = new SignalsStrongBBTrendStocksMD(
                SystemParams.Get(StrongBBTrendStocksParams.StockName).As<string>(),
                SystemParams.Get(StrongBBTrendStocksParams.BBPeriod).As<int>(),
                SystemParams.Get(StrongBBTrendStocksParams.BBSigmaWidth).As<float>(),
                SystemParams.Get(StrongBBTrendStocksParams.ATRWidth).As<int>(),
                _dataLoader, _dataProvider, _systemExecutionLogger,
                //new MMSignalVolumeForSystemValuePercent(0.05f, _commission, _dataLoader),
                new MMSignalVolumeByTakenRiskPercent(0.01f, _commission, _dataLoader),
                new MMTrailingStopMinMaxOfN(TrailingStopMinOfL, 0, TrailingStopTicksBelow, _dataLoader, _gpwTickOps),
                _gpwTickOps,
                _gpwTickOps
                );

            _dataDefinitionProvider = signals;
            _signalGeneratorOnOpen = null;
            _signalGeneratorOnClose = signals;
            //_commission = null;
            //_slippage = null;
        }
    }
}
