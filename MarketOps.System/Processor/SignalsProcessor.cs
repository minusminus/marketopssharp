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

            var signalsToProcess = new HashSet<Signal>(signalsSelector(signals));

            foreach (Signal signal in signalsToProcess)
            {
                StockPricesData pricesData = _dataLoader.Get(signal.Stock.Name, signal.DataRange, signal.IntradayInterval, ts, ts);
                int pricesDataIndex = pricesData.FindByTS(ts);
                float openPrice = openPriceLevel(pricesData, pricesDataIndex, signal);

                if (signal.ReversePosition)
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
                else
                    equity.Open(signal.Stock, signal.Direction, ts, openPrice, signal.Volume, signal.DataRange, signal.IntradayInterval, signal);
            }

            signals.RemoveAll(s => signalsToProcess.Contains(s));
        }
    }
}
