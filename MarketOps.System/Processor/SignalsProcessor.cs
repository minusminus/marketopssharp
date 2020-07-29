using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using MarketOps.System.Extensions;
using MarketOps.System.Interfaces;
using System;
using System.Collections.Generic;

namespace MarketOps.System.Processor
{
    /// <summary>
    /// System signals processor.
    /// </summary>
    internal class SignalsProcessor
    {
        private readonly IDataLoader _dataLoader;

        public SignalsProcessor(IDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
        }

        public void Process(List<Signal> signals, DateTime ts, SystemEquity equity,
            Func<IEnumerable<Signal>, IEnumerable<Signal>> signalsSelector,
            Func<StockPricesData, int, Signal, float> openPriceLevel)
        {
            if (signals.Count == 0) return;

            HashSet<Signal> signalsToProcess = SelectSignals(signals, signalsSelector);
            ProcessSignals(ts, equity, openPriceLevel, signalsToProcess);
            RemoveProcessedSignals(signals, signalsToProcess);
        }

        private static HashSet<Signal> SelectSignals(List<Signal> signals, Func<IEnumerable<Signal>, IEnumerable<Signal>> signalsSelector)
        {
            return new HashSet<Signal>(signalsSelector(signals));
        }

        private static void RemoveProcessedSignals(List<Signal> signals, HashSet<Signal> signalsToProcess)
        {
            signals.RemoveAll(s => signalsToProcess.Contains(s));
        }

        private void ProcessSignals(DateTime ts, SystemEquity equity, Func<StockPricesData, int, Signal, float> openPriceLevel, HashSet<Signal> signalsToProcess)
        {
            foreach (Signal signal in signalsToProcess)
                ProcessSignal(ts, equity, openPriceLevel, signal);
        }

        private void ProcessSignal(DateTime ts, SystemEquity equity, Func<StockPricesData, int, Signal, float> openPriceLevel, Signal signal)
        {
            StockPricesData pricesData = GetPricesData(ts, signal);
            int pricesDataIndex = pricesData.FindByTS(ts);
            float openPrice = openPriceLevel(pricesData, pricesDataIndex, signal);

            if (signal.ReversePosition)
                ReversePosition(ts, equity, signal, openPrice);
            else
                OpenPosition(ts, equity, signal, openPrice);
        }

        private StockPricesData GetPricesData(DateTime ts, Signal signal)
        {
            return _dataLoader.Get(signal.Stock.Name, signal.DataRange, signal.IntradayInterval, ts, ts);
        }

        private static void OpenPosition(DateTime ts, SystemEquity equity, Signal signal, float openPrice)
        {
            equity.Open(signal.Stock, signal.Direction, ts, openPrice, signal.Volume, signal.DataRange, signal.IntradayInterval, signal);
        }

        private static void ReversePosition(DateTime ts, SystemEquity equity, Signal signal, float openPrice)
        {
            PositionDir newPosDir = signal.Direction;
            int currPos = equity.PositionsActive.FindIndex(p => p.Stock.ID == signal.Stock.ID);
            if (currPos > -1)
            {
                newPosDir = equity.PositionsActive[currPos].Direction == PositionDir.Long ? PositionDir.Short : PositionDir.Long;
                equity.Close(currPos, ts, openPrice);
            }
            equity.Open(signal.Stock, newPosDir, ts, openPrice, signal.Volume, signal.DataRange, signal.IntradayInterval, signal);
        }
    }
}
