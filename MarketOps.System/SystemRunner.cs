using MarketOps.StockData.Interfaces;
using System;
using MarketOps.System.Processor;
using MarketOps.System.Interfaces;

namespace MarketOps.System
{
    /// <summary>
    /// Defined system runner.
    /// </summary>
    public class SystemRunner
    {
        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _dataLoader;

        public SystemRunner(IStockDataProvider dataProvider, ISystemDataLoader dataLoader)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
        }

        public void Run(SystemDefinition systemDefinition, SystemState systemState, DateTime tsFrom, DateTime tsTo)
        {
            SystemProcessor processor = new SystemProcessor(
                _dataProvider,
                _dataLoader,
                systemDefinition.DataDefinitionProvider,
                systemDefinition.SignalGeneratorOnOpen,
                systemDefinition.SignalGeneratorOnClose,
                systemDefinition.Commission,
                systemDefinition.Slippage,
                systemDefinition.MMPositionCloseCalculator);
            processor.Process(systemState, tsFrom, tsTo);
        }
    }
}
