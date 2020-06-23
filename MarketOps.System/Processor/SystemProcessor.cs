using MarketOps.StockData.Interfaces;
using MarketOps.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketOps.System.Processor
{
    /// <summary>
    /// System processor.
    /// </summary>
    internal class SystemProcessor
    {
        private readonly IStockDataProvider _dataProvider;
        private readonly IDataLoader _dataLoader;
        private readonly ITickAligner _tickAligner;
        private readonly ITickAdder _tickAdder;
        private readonly ISystemDataDefinitionProvider _dataDefinitionProvider;
        private readonly ISignalGeneratorOnOpen _signalGeneratorOnOpen;
        private readonly ISignalGeneratorOnClose _signalGeneratorOnClose;
        private readonly ICommission _commission;
        private readonly ISlippage _slippage;

        public SystemProcessor(
            IStockDataProvider dataProvider,
            IDataLoader dataLoader,
            ITickAligner tickAligner,
            ITickAdder tickAdder,
            ISystemDataDefinitionProvider dataDefinitionProvider,
            ISignalGeneratorOnOpen signalGeneratorOnOpen,
            ISignalGeneratorOnClose signalGeneratorOnClose,
            ICommission commission,
            ISlippage slippage)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
            _tickAligner = tickAligner;
            _tickAdder = tickAdder;
            _dataDefinitionProvider = dataDefinitionProvider;
            _signalGeneratorOnOpen = signalGeneratorOnOpen;
            _signalGeneratorOnClose = signalGeneratorOnClose;
            _commission = commission;
            _slippage = slippage;
        }

        public void Process(DateTime tsFrom, DateTime tsTo)
        {
            SystemConfiguration systemConfiguration = GetSystemConfiguration(tsFrom, tsTo);
            //Dictionary<SystemStockDataDefinition, int> backBufferInfo = StocksBackBufferAggregator.Calculate(systemConfiguration.dataDefinition.statsForStocks);
            //PreloadStocksData(backBufferInfo);
            //PrecalcStockStats(backBufferInfo);
        }

        private SystemConfiguration GetSystemConfiguration(DateTime tsFrom, DateTime tsTo)
        {
            return new SystemConfiguration()
            {
                tsFrom = tsFrom,
                tsTo = tsTo,
                dataDefinition = _dataDefinitionProvider.GetDataDefinition()
            };
        }

        private void PreloadStocksData(Dictionary<SystemStockDataDefinition, int> backBufferInfo)
        {
            //_dataLoader.
        }

        private void PrecalcStockStats(Dictionary<SystemStockDataDefinition, int> backBufferInfo)
        {
            throw new NotImplementedException();
        }
    }
}
