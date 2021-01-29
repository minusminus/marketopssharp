using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using MarketOps.SystemExecutor.MM;

namespace MarketOps.SystemDefs.BBTrend
{
    /// <summary>
    /// Play in trend specified by price in BB.
    /// </summary>
    public class BBTrend : SystemDefinition
    {
        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _dataLoader;

        public BBTrend(IStockDataProvider dataProvider, ISystemDataLoader dataLoader,
            ISlippage slippage, ICommission commission)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
            _slippage = slippage;
            _commission = commission;

            SystemParams.Set(BBTrendParams.StockName, "");
            SystemParams.Set(BBTrendParams.BBPeriod, 20);
            SystemParams.Set(BBTrendParams.BBSigmaWidth, 2f);
        }

        public override void Prepare()
        {
            SignalsBBTrend signals = new SignalsBBTrend(
                SystemParams.Get(BBTrendParams.StockName).As<string>(),
                StockData.Types.StockDataRange.Daily,
                SystemParams.Get(BBTrendParams.BBPeriod).As<int>(),
                SystemParams.Get(BBTrendParams.BBSigmaWidth).As<float>(),
                _dataLoader, _dataProvider,
                new MMSignalVolumeOneItem());

            _dataDefinitionProvider = signals;
            _signalGeneratorOnOpen = null;
            _signalGeneratorOnClose = signals;
            //_commission = null;
            //_slippage = null;
            _mmPositionCloseCalculator = null;
        }
    }
}
