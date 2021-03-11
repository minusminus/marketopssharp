﻿using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Interfaces;
using System;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemExecutor.Processor
{
    /// <summary>
    /// System signals processor.
    /// </summary>
    internal class SignalsProcessor
    {
        private readonly ISystemDataLoader _dataLoader;
        private readonly ICommission _commission;
        private readonly ISlippage _slippage;

        public SignalsProcessor(ISystemDataLoader dataLoader, ICommission commission, ISlippage slippage)
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
                (StockPricesData pricesData, int pricesDataIndex) = GetPricesDataAndIndex(systemState.Signals[i].Stock.Name, systemState.Signals[i].DataRange, systemState.Signals[i].IntradayInterval, ts);
                if (signalSelector(systemState.Signals[i], pricesData, pricesDataIndex))
                    ProcessSignal(systemState.Signals[i], ts, systemState, openPriceSelector, pricesData, pricesDataIndex);
            }
        }

        private void ProcessSignal(Signal signal, DateTime ts, SystemState systemState, Func<Signal, StockPricesData, int, float> openPriceSelector, StockPricesData pricesData, int pricesDataIndex)
        {
            VerifySignal(systemState, signal);
            float openPrice = openPriceSelector(signal, pricesData, pricesDataIndex);

            if (signal.ConvertPosition)
            {
                (StockPricesData srcPricesData, int srcPricesDataIndex) = GetPricesDataAndIndex(signal.SrcStock.Name, signal.DataRange, signal.IntradayInterval, ts);
                float srcPrice = openPriceSelector(signal, srcPricesData, srcPricesDataIndex);
                ConvertPosition(signal, ts, systemState, openPrice, srcPrice);
            }
            else if (signal.ReversePosition)
                ReversePosition(signal, ts, systemState, openPrice);
            else
                OpenPosition(signal, ts, systemState, openPrice);
        }

        private void VerifySignal(SystemState systemState, Signal signal)
        {
            void ThrowException(string header) => throw new Exception($"{header} for: {signal.Stock.Name} (ID = {signal.Stock.ID})");

            if (signal.ConvertPosition)
            {
                if (signal.SrcStock == null)
                    ThrowException("Signal convert source stock undefined");
                if ((signal.ConvertValue == 0) && (!signal.ConvertAll))
                    ThrowException("Signal convert value undefined");
                if (FindActivePositionIndex(systemState, signal.SrcStock) < 0)
                    ThrowException("No active source stock position");
            }
            else
            {
                if (signal.Volume == 0)
                    ThrowException("Signal volume 0");
            }
        }

        private (StockPricesData pricesData, int pricesDataIndex) GetPricesDataAndIndex(string stockName, StockDataRange dataRange, int intradayInterval, DateTime ts)
        {
            StockPricesData data = _dataLoader.Get(stockName, dataRange, intradayInterval, ts, ts);
            return (data, data.FindByTS(ts));
        }

        private void OpenPosition(Signal signal, DateTime ts, SystemState systemState, float openPrice)
        {
            systemState.Open(ts, signal.Direction, openPrice, signal, _slippage, _commission);
        }

        private void ReversePosition(Signal signal, DateTime ts, SystemState systemState, float openPrice)
        {
            PositionDir newPosDir = signal.Direction;
            int currPos = FindActivePositionIndex(systemState, signal.Stock);
            if (currPos > -1)
            {
                newPosDir = systemState.PositionsActive[currPos].ReversedDirection();
                systemState.Close(currPos, ts, openPrice, _slippage, _commission);
            }
            systemState.Open(ts, newPosDir, openPrice, signal, _slippage, _commission);
        }

        private void ConvertPosition(Signal signal, DateTime ts, SystemState systemState, float openPrice, float srcPrice)
        {
            int srcPos = FindActivePositionIndex(systemState, signal.SrcStock);
            float reduceVolume = Math.Min(systemState.PositionsActive[srcPos].Volume,
                signal.ConvertAll ? systemState.PositionsActive[srcPos].Volume : (signal.ConvertValue / srcPrice));
            systemState.ReducePosition(srcPos, ts, srcPrice, reduceVolume, signal, _slippage, _commission);

            int destPos = FindActivePositionIndex(systemState, signal.Stock);
            float addVolume = (srcPrice * reduceVolume) / openPrice;
            if (destPos > -1)
                systemState.AddToPosition(destPos, ts, openPrice, addVolume, signal, _slippage, _commission);
            else
                systemState.Open(ts, signal.Direction, openPrice, addVolume, signal, _slippage, _commission);
        }

        private int FindActivePositionIndex(SystemState systemState, StockDefinition stock) =>
            systemState.PositionsActive.FindIndex(p => p.Stock.ID == stock.ID);
    }
}
