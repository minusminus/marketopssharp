using MarketOps.StockData.Interfaces;
using System;
using MarketOps.SystemExecutor.Processor;
using MarketOps.SystemExecutor.Interfaces;

namespace MarketOps.SystemExecutor
{
    /// <summary>
    /// Runner of defined system.
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
            systemDefinition.Prepare();

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
