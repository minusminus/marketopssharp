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
            Func<Signal, StockPricesData, int, bool> signalSelector,
            Func<Signal, StockPricesData, int, float> openPriceSelector)
        {
            if (signals.Count == 0) return;

            HashSet<Signal> processedSignals = new HashSet<Signal>();
            ProcessSignals(signals, ts, equity, signalSelector, openPriceSelector, processedSignals);
            RemoveProcessedSignals(signals, processedSignals);
        }

        private static void RemoveProcessedSignals(List<Signal> signals, HashSet<Signal> signalsToProcess)
        {
            signals.RemoveAll(s => signalsToProcess.Contains(s));
        }

        private void ProcessSignals(List<Signal> signals, DateTime ts, SystemEquity equity, Func<Signal, StockPricesData, int, bool> signalSelector, Func<Signal, StockPricesData, int, float> openPriceSelector, HashSet<Signal> processedSignals)
        {
            foreach (Signal signal in signals)
            {
                StockPricesData pricesData = GetPricesData(signal, ts);
                int pricesDataIndex = pricesData.FindByTS(ts);
                if (signalSelector(signal, pricesData, pricesDataIndex))
                {
                    processedSignals.Add(signal);
                    ProcessSignal(signal, ts, equity, openPriceSelector, pricesData, pricesDataIndex);
                }
            }
        }

        private void ProcessSignal(Signal signal, DateTime ts, SystemEquity equity, Func<Signal, StockPricesData, int, float> openPriceSelector, StockPricesData pricesData, int pricesDataIndex)
        {
            float openPrice = openPriceSelector(signal, pricesData, pricesDataIndex);

            if (signal.ReversePosition)
                ReversePosition(signal, ts, equity, openPrice);
            else
                OpenPosition(signal, ts, equity, openPrice);
        }

        private StockPricesData GetPricesData(Signal signal, DateTime ts)
        {
            return _dataLoader.Get(signal.Stock.Name, signal.DataRange, signal.IntradayInterval, ts, ts);
        }

        private static void OpenPosition(Signal signal, DateTime ts, SystemEquity equity, float openPrice)
        {
            equity.Open(signal.Stock, signal.Direction, ts, openPrice, signal.Volume, signal.DataRange, signal.IntradayInterval, signal);
        }

        private static void ReversePosition(Signal signal, DateTime ts, SystemEquity equity, float openPrice)
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
