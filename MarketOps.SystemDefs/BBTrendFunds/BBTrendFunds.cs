using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;

namespace MarketOps.SystemDefs.BBTrendFunds
{
    /// <summary>
    /// Multi funds strategy with BB trends.
    /// </summary>
    public class BBTrendFunds : SystemDefinition
    {
        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _dataLoader;

        public BBTrendFunds(IStockDataProvider dataProvider, ISystemDataLoader dataLoader,
            ISlippage slippage, ICommission commission)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
            _slippage = slippage;
            _commission = commission;

            //SystemParams.Set(BBTrendParams.StockName, "");
            //SystemParams.Set(BBTrendParams.BBPeriod, 20);
            //SystemParams.Set(BBTrendParams.BBSigmaWidth, 2f);
        }

        public override void Prepare()
        {
            SignalsBBTrendFunds signals = new SignalsBBTrendFunds(_dataLoader, _dataProvider);

            _dataDefinitionProvider = signals;
            _signalGeneratorOnOpen = null;
            _signalGeneratorOnClose = signals;
            //_commission = null;
            //_slippage = null;
            _mmPositionCloseCalculator = null;
        }
    }
}
