using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.System.Extensions;
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
        private readonly IMMPositionCloseCalculator _mmPositionCloseCalculator;
        private readonly SignalsProcessor _signalsProcessor;
        private readonly PositionsCloser _positionCloser;

        public SystemProcessor(
            IStockDataProvider dataProvider,
            IDataLoader dataLoader,
            ITickAligner tickAligner,
            ITickAdder tickAdder,
            ISystemDataDefinitionProvider dataDefinitionProvider,
            ISignalGeneratorOnOpen signalGeneratorOnOpen,
            ISignalGeneratorOnClose signalGeneratorOnClose,
            ICommission commission,
            ISlippage slippage,
            IMMPositionCloseCalculator mmPositionCloseCalculator)
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
            _mmPositionCloseCalculator = mmPositionCloseCalculator;
            _signalsProcessor = new SignalsProcessor(_dataLoader, _commission, _slippage);
            _positionCloser = new PositionsCloser(_dataLoader, _commission, _slippage);
        }

        public SystemEquity Process(DateTime tsFrom, DateTime tsTo, float cashOnStart)
        {
            SystemEquity equity = new SystemEquity() { Cash = cashOnStart };
            SystemConfiguration systemConfiguration = GetSystemConfiguration(tsFrom, tsTo);
            CheckSystemConfiguration(systemConfiguration);
            PreloadAndCalcStockData(systemConfiguration);
            ProcessConfiguredSystem(systemConfiguration, equity);
            return equity;
        }

        private void CheckSystemConfiguration(SystemConfiguration systemConfiguration)
        {
            if (systemConfiguration.tsFrom > systemConfiguration.tsTo) throw new Exception("Configured from greater then to.");
            if (systemConfiguration.dataDefinition.stocks.Count == 0) throw new Exception("No stocks defined.");
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

        private void PreloadAndCalcStockData(SystemConfiguration systemConfiguration)
        {
            new StocksDataPreloader(_dataProvider, _dataLoader).PreloadDataAndPrecalcStats(
                systemConfiguration.tsFrom,
                systemConfiguration.tsTo,
                StocksBackBufferAggregator.Calculate(systemConfiguration.dataDefinition.stocks)
                );
        }

        private void ProcessConfiguredSystem(SystemConfiguration systemConfiguration, SystemEquity equity)
        {
            List<Signal> signals = new List<Signal>();
            StockPricesData leadingPricesData = GetLeadingData(systemConfiguration);
            var (from, to) = PricesDataRangeFinder.Find(leadingPricesData, systemConfiguration.tsFrom, systemConfiguration.tsTo);
            for (int i = from; i <= to; i++)
                ProcessSingleTick(leadingPricesData, i, equity, signals);
        }

        private StockPricesData GetLeadingData(SystemConfiguration systemConfiguration)
        {
            StockPricesData res = _dataLoader.Get(systemConfiguration.dataDefinition.stocks[0].stock.Name, systemConfiguration.dataDefinition.stocks[0].dataRange, 0, systemConfiguration.tsFrom, systemConfiguration.tsTo);
            if (res.Length == 0) throw new Exception("Leading prices data is empty.");
            return res;
        }

        private void ProcessSingleTick(StockPricesData leadingPricesData, int leadingIndex, SystemEquity equity, List<Signal> signals)
        {
            UpdateActivePositions(equity);
            ProcessStopsOnOpen(leadingPricesData.TS[leadingIndex], equity);
            ProcessSignalsOnOpen(leadingPricesData.TS[leadingIndex], equity, signals);
            GenerateOnOpenSignals(leadingPricesData.TS[leadingIndex], leadingIndex, signals);

            ProcessStopsOnPrice(leadingPricesData.TS[leadingIndex], equity);
            ProcessSignalsOnPrice(leadingPricesData.TS[leadingIndex], equity, signals);

            ProcessStopsOnClose(leadingPricesData.TS[leadingIndex], equity);
            ProcessSignalsOnClose(leadingPricesData.TS[leadingIndex], equity, signals);
            GenerateOnCloseSignals(leadingPricesData.TS[leadingIndex], leadingIndex, signals);

            RecalculateStops(leadingPricesData.TS[leadingIndex], equity);
            CalculateCurrentSystemValue(leadingPricesData.TS[leadingIndex], equity);
        }

        private void ProcessSignalsOnOpen(DateTime ts, SystemEquity equity, List<Signal> signals)
        {
            _signalsProcessor.Process(signals, ts, equity,
                SignalSelector.OnOpen,
                OpenPriceSelector.OnOpen);
        }

        private void ProcessSignalsOnClose(DateTime ts, SystemEquity equity, List<Signal> signals)
        {
            _signalsProcessor.Process(signals, ts, equity,
                SignalSelector.OnClose,
                OpenPriceSelector.OnClose);
        }

        private void ProcessSignalsOnPrice(DateTime ts, SystemEquity equity, List<Signal> signals)
        {
            _signalsProcessor.Process(signals, ts, equity,
                SignalSelector.OnPrice,
                OpenPriceSelector.OnPrice);
        }

        private void GenerateOnOpenSignals(DateTime ts, int leadingIndex, List<Signal> signals)
        {
            signals.AddRange(_signalGeneratorOnOpen?.GenerateOnOpen(ts, leadingIndex));
        }

        private void GenerateOnCloseSignals(DateTime ts, int leadingIndex, List<Signal> signals)
        {
            signals.AddRange(_signalGeneratorOnClose?.GenerateOnClose(ts, leadingIndex));
        }

        private void ProcessStopsOnOpen(DateTime ts, SystemEquity equity)
        {
            _positionCloser.Process(ts, equity,
                ClosingPositionSelector.OnOpen,
                ClosePriceSelector.OnOpen);
        }

        private void ProcessStopsOnClose(DateTime ts, SystemEquity equity)
        {
            _positionCloser.Process(ts, equity,
                ClosingPositionSelector.OnClose,
                ClosePriceSelector.OnClose);
        }

        private void ProcessStopsOnPrice(DateTime ts, SystemEquity equity)
        {
            _positionCloser.Process(ts, equity,
                ClosingPositionSelector.OnPrice,
                ClosePriceSelector.OnPrice);
        }

        private void UpdateActivePositions(SystemEquity equity)
        {
            equity.PositionsActive.ForEach(pos => pos.TicksActive++);
        }

        private void RecalculateStops(DateTime ts, SystemEquity equity)
        {
            equity.PositionsActive.ForEach(pos => _mmPositionCloseCalculator?.CalculateCloseMode(ref pos, ts));
        }

        private void CalculateCurrentSystemValue(DateTime ts, SystemEquity equity)
        {
            equity.CalcCurrentValue(ts, _dataLoader);
        }
    }
}
