using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;
using System.Collections.Generic;

namespace MarketOps.SystemExecutor.Processor
{
    /// <summary>
    /// System processor.
    /// </summary>
    internal class SystemProcessor
    {
        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _dataLoader;
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
            ISystemDataLoader dataLoader,
            ISystemDataDefinitionProvider dataDefinitionProvider,
            ISignalGeneratorOnOpen signalGeneratorOnOpen,
            ISignalGeneratorOnClose signalGeneratorOnClose,
            ICommission commission,
            ISlippage slippage,
            IMMPositionCloseCalculator mmPositionCloseCalculator)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
            _dataDefinitionProvider = dataDefinitionProvider;
            _signalGeneratorOnOpen = signalGeneratorOnOpen;
            _signalGeneratorOnClose = signalGeneratorOnClose;
            _commission = commission;
            _slippage = slippage;
            _mmPositionCloseCalculator = mmPositionCloseCalculator;
            _signalsProcessor = new SignalsProcessor(_dataLoader, _commission, _slippage);
            _positionCloser = new PositionsCloser(_dataLoader, _commission, _slippage);
        }

        public void Process(SystemState systemState, DateTime tsFrom, DateTime tsTo)
        {
            SystemConfiguration systemConfiguration = GetSystemConfiguration(tsFrom, tsTo);
            CheckSystemConfiguration(systemConfiguration);
            PreloadAndCalcStockData(systemConfiguration);
            ProcessConfiguredSystem(systemConfiguration, systemState);
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

        private void ProcessConfiguredSystem(SystemConfiguration systemConfiguration, SystemState systemState)
        {
            StockPricesData leadingPricesData = GetLeadingData(systemConfiguration);
            var (from, to) = PricesDataRangeFinder.Find(leadingPricesData, systemConfiguration.tsFrom, systemConfiguration.tsTo);
            for (int i = from; i <= to; i++)
                ProcessSingleTick(leadingPricesData, i, systemState);
        }

        private StockPricesData GetLeadingData(SystemConfiguration systemConfiguration)
        {
            StockPricesData res = _dataLoader.Get(systemConfiguration.dataDefinition.stocks[0].stock.Name, systemConfiguration.dataDefinition.stocks[0].dataRange, 0, systemConfiguration.tsFrom, systemConfiguration.tsTo);
            if (res.Length == 0) throw new Exception("Leading prices data is empty.");
            return res;
        }

        private void ProcessSingleTick(StockPricesData leadingPricesData, int leadingIndex, SystemState systemState)
        {
            UpdateActivePositions(systemState);
            ProcessStopsOnOpen(leadingPricesData.TS[leadingIndex], systemState);
            ProcessSignalsOnOpen(leadingPricesData.TS[leadingIndex], systemState);
            GenerateSignalsOnOpen(leadingPricesData.TS[leadingIndex], leadingIndex, systemState.Signals);

            ProcessStopsOnPrice(leadingPricesData.TS[leadingIndex], systemState);
            ProcessSignalsOnPrice(leadingPricesData.TS[leadingIndex], systemState);

            ProcessStopsOnClose(leadingPricesData.TS[leadingIndex], systemState);
            ProcessSignalsOnClose(leadingPricesData.TS[leadingIndex], systemState);
            GenerateSignalsOnClose(leadingPricesData.TS[leadingIndex], leadingIndex, systemState.Signals);

            RecalculateStops(leadingPricesData.TS[leadingIndex], systemState);
            CalculateCurrentSystemValue(leadingPricesData.TS[leadingIndex], systemState);
            UpdateLastProcessedTS(leadingPricesData.TS[leadingIndex], systemState);
        }

        private void ProcessSignalsOnOpen(DateTime ts, SystemState systemState)
        {
            _signalsProcessor.Process(ts, systemState,
                SignalSelector.OnOpen,
                OpenPriceSelector.OnOpen);
            RemoveSignalsOfType(systemState.Signals, SignalType.EnterOnOpen);
        }

        private void ProcessSignalsOnClose(DateTime ts, SystemState systemState)
        {
            _signalsProcessor.Process(ts, systemState,
                SignalSelector.OnClose,
                OpenPriceSelector.OnClose);
            RemoveSignalsOfType(systemState.Signals, SignalType.EnterOnClose);
        }

        private void ProcessSignalsOnPrice(DateTime ts, SystemState systemState)
        {
            _signalsProcessor.Process(ts, systemState,
                SignalSelector.OnPrice,
                OpenPriceSelector.OnPrice);
            RemoveSignalsOfType(systemState.Signals, SignalType.EnterOnPrice);
        }

        private void RemoveSignalsOfType(List<Signal> signals, SignalType toRemove)
        {
            signals.RemoveAll(s => s.Type == toRemove);
        }

        private void GenerateSignalsOnOpen(DateTime ts, int leadingIndex, List<Signal> signals)
        {
            if (_signalGeneratorOnOpen == null) return;
            signals.AddRange(_signalGeneratorOnOpen.GenerateOnOpen(ts, leadingIndex));
        }

        private void GenerateSignalsOnClose(DateTime ts, int leadingIndex, List<Signal> signals)
        {
            if (_signalGeneratorOnClose == null) return;
            signals.AddRange(_signalGeneratorOnClose.GenerateOnClose(ts, leadingIndex));
        }

        private void ProcessStopsOnOpen(DateTime ts, SystemState systemState)
        {
            _positionCloser.Process(ts, systemState,
                ClosingPositionSelector.OnOpen,
                ClosePriceSelector.OnOpen);
        }

        private void ProcessStopsOnClose(DateTime ts, SystemState systemState)
        {
            _positionCloser.Process(ts, systemState,
                ClosingPositionSelector.OnClose,
                ClosePriceSelector.OnClose);
        }

        private void ProcessStopsOnPrice(DateTime ts, SystemState systemState)
        {
            _positionCloser.Process(ts, systemState,
                ClosingPositionSelector.OnStopHit,
                ClosePriceSelector.OnStopHit);
        }

        private void UpdateActivePositions(SystemState systemState)
        {
            systemState.PositionsActive.ForEach(pos => pos.TicksActive++);
        }

        private void RecalculateStops(DateTime ts, SystemState systemState)
        {
            if (_mmPositionCloseCalculator == null) return;
            systemState.PositionsActive.ForEach(pos => _mmPositionCloseCalculator.CalculateCloseMode(pos, ts));
        }

        private void CalculateCurrentSystemValue(DateTime ts, SystemState systemState)
        {
            systemState.CalcCurrentValue(ts, _dataLoader);
        }

        private void UpdateLastProcessedTS(DateTime ts, SystemState systemState)
        {
            systemState.LastProcessedTS = ts;
        }
    }
}
