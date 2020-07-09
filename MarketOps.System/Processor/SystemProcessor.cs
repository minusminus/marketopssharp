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
        private readonly SignalProcessor _signalProcessor;

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
            _signalProcessor = new SignalProcessor(_dataLoader);
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
            //ProcessStopsOnOpen;
            //ProcessSignalsOnOpen;
            //GenerateOnOpenSignals;

            //ProcessStopsOnPrice;
            //ProcessSignalsOnPrice;

            //ProcessStopsOnClose;
            //ProcessSignalsOnClose;
            //GenerateOnCloseSignals;

            //RecalculateStops;
            //CalculateCurrentSystemValue;
        }

        private void ProcessSignalsOnOpen(DateTime ts, SystemEquity equity, List<Signal> signals)
        {
            _signalProcessor.Process(signals, ts, equity,
                (sigs) => sigs.Where(s => s.Type == SignalType.EnterOnOpen),
                (pricesData, pricesDataIndex, _) => pricesData.O[pricesDataIndex]);

            //var signalsToProcess = new HashSet<Signal>(signals.Where(s => s.Type == SignalType.EnterOnOpen));

            //foreach (Signal signal in signalsToProcess)
            //{
            //    StockPricesData pricesData = _dataLoader.Get(signal.Stock.Name, signal.DataRange, signal.IntradayInterval, ts, ts);
            //    int pricesDataIndex = pricesData.FindByTS(ts);

            //    if (signal.ReversePosition)
            //    {
            //        PositionDir newPosDir = signal.Direction;
            //        int currPos = equity.PositionsActive.FindIndex(p => p.Stock.ID == signal.Stock.ID);
            //        if (currPos > -1)
            //        {
            //            newPosDir = equity.PositionsActive[currPos].Direction;
            //            equity.Close(currPos, ts, pricesData.O[pricesDataIndex]);
            //        }
            //        equity.Open(signal.Stock, newPosDir, ts, pricesData.O[pricesDataIndex], signal.Volume, signal.DataRange, signal.IntradayInterval);
            //    }
            //    else
            //        equity.Open(signal.Stock, signal.Direction, ts, pricesData.O[pricesDataIndex], signal.Volume, signal.DataRange, signal.IntradayInterval);
            //}

            //signals.RemoveAll(s => signalsToProcess.Contains(s));
        }
    }
}
