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
        private readonly ICommission _commission;
        private readonly ISlippage _slippage;

        public SignalsProcessor(IDataLoader dataLoader, ICommission commission, ISlippage slippage)
        {
            _dataLoader = dataLoader;
            _commission = commission;
            _slippage = slippage;
        }

        public void Process(List<Signal> signals, DateTime ts, SystemEquity equity,
            Func<Signal, StockPricesData, int, bool> signalSelector,
            Func<Signal, StockPricesData, int, float> openPriceSelector)
        {
            if (signals.Count == 0) return;
            ProcessSignals(signals, ts, equity, signalSelector, openPriceSelector);
        }

        private void ProcessSignals(List<Signal> signals, DateTime ts, SystemEquity equity, Func<Signal, StockPricesData, int, bool> signalSelector, Func<Signal, StockPricesData, int, float> openPriceSelector)
        {
            int i = 0;
            while (i < signals.Count)
            {
                StockPricesData pricesData = GetPricesData(signals[i], ts);
                int pricesDataIndex = pricesData.FindByTS(ts);
                if (signalSelector(signals[i], pricesData, pricesDataIndex))
                {
                    ProcessSignal(signals[i], ts, equity, openPriceSelector, pricesData, pricesDataIndex);
                    RemoveProcessedSignal(signals, i);
                }
                else
                    i++;
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

        private void RemoveProcessedSignal(List<Signal> signals, int i)
        {
            signals.RemoveAt(i);
        }

        private StockPricesData GetPricesData(Signal signal, DateTime ts)
        {
            return _dataLoader.Get(signal.Stock.Name, signal.DataRange, signal.IntradayInterval, ts, ts);
        }

        private void OpenPosition(Signal signal, DateTime ts, SystemEquity equity, float openPrice)
        {
            equity.Open(ts, signal.Direction, openPrice, signal, _slippage, _commission);
        }

        private void ReversePosition(Signal signal, DateTime ts, SystemEquity equity, float openPrice)
        {
            PositionDir newPosDir = signal.Direction;
            int currPos = equity.PositionsActive.FindIndex(p => p.Stock.ID == signal.Stock.ID);
            if (currPos > -1)
            {
                newPosDir = equity.PositionsActive[currPos].Direction == PositionDir.Long ? PositionDir.Short : PositionDir.Long;
                equity.Close(currPos, ts, openPrice, _slippage, _commission);
            }
            equity.Open(ts, newPosDir, openPrice, signal, _slippage, _commission);
        }
    }
}
