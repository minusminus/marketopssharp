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

        public void Process(DateTime ts, SystemState systemState,
            Func<Signal, StockPricesData, int, bool> signalSelector,
            Func<Signal, StockPricesData, int, float> openPriceSelector)
        {
            if (systemState.Signals.Count == 0) return;
            ProcessSignals(ts, systemState, signalSelector, openPriceSelector);
        }

        private void ProcessSignals(DateTime ts, SystemState systemState, Func<Signal, StockPricesData, int, bool> signalSelector, Func<Signal, StockPricesData, int, float> openPriceSelector)
        {
            for (int i = 0; i < systemState.Signals.Count; i++)
            {
                StockPricesData pricesData = GetPricesData(systemState.Signals[i], ts);
                int pricesDataIndex = pricesData.FindByTS(ts);
                if (signalSelector(systemState.Signals[i], pricesData, pricesDataIndex))
                    ProcessSignal(systemState.Signals[i], ts, systemState, openPriceSelector, pricesData, pricesDataIndex);
            }
        }

        private void ProcessSignal(Signal signal, DateTime ts, SystemState systemState, Func<Signal, StockPricesData, int, float> openPriceSelector, StockPricesData pricesData, int pricesDataIndex)
        {
            float openPrice = openPriceSelector(signal, pricesData, pricesDataIndex);

            if (signal.ReversePosition)
                ReversePosition(signal, ts, systemState, openPrice);
            else
                OpenPosition(signal, ts, systemState, openPrice);
        }

        private StockPricesData GetPricesData(Signal signal, DateTime ts)
        {
            return _dataLoader.Get(signal.Stock.Name, signal.DataRange, signal.IntradayInterval, ts, ts);
        }

        private void OpenPosition(Signal signal, DateTime ts, SystemState systemState, float openPrice)
        {
            systemState.Open(ts, signal.Direction, openPrice, signal, _slippage, _commission);
        }

        private void ReversePosition(Signal signal, DateTime ts, SystemState systemState, float openPrice)
        {
            PositionDir newPosDir = signal.Direction;
            int currPos = systemState.PositionsActive.FindIndex(p => p.Stock.ID == signal.Stock.ID);
            if (currPos > -1)
            {
                newPosDir = systemState.PositionsActive[currPos].ReverseDirection();
                systemState.Close(currPos, ts, openPrice, _slippage, _commission);
            }
            systemState.Open(ts, newPosDir, openPrice, signal, _slippage, _commission);
        }
    }
}
