using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;

namespace MarketOps.SystemData.Types
{
    /// <summary>
    /// Base class for system definition.
    /// Provides interfaces and other reqired data for system runner.
    /// </summary>
    public abstract class SystemDefinition
    {
        protected ISystemDataDefinitionProvider _dataDefinitionProvider;
        protected ISignalGeneratorOnOpen _signalGeneratorOnOpen;
        protected ISignalGeneratorOnClose _signalGeneratorOnClose;
        protected ICommission _commission;
        protected ISlippage _slippage;
        protected IMMPositionCloseCalculator _mmPositionCloseCalculator;

        public ISystemDataDefinitionProvider DataDefinitionProvider { get => _dataDefinitionProvider; }
        public ISignalGeneratorOnOpen SignalGeneratorOnOpen { get => _signalGeneratorOnOpen; }
        public ISignalGeneratorOnClose SignalGeneratorOnClose { get => _signalGeneratorOnClose; }
        public ICommission Commission { get => _commission; }
        public ISlippage Slippage { get => _slippage; }
        public IMMPositionCloseCalculator MMPositionCloseCalculator { get => _mmPositionCloseCalculator; }
        public readonly MOParams SystemParams = new MOParams();

        public abstract void Prepare();
    }
}
