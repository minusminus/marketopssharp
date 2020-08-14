using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using MarketOps.System.Extensions;
using MarketOps.System.Interfaces;
using System;

namespace MarketOps.System.Processor
{
    /// <summary>
    /// System active positions closer.
    /// </summary>
    internal class PositionsCloser
    {
        private readonly IDataLoader _dataLoader;
        private readonly ICommission _commission;
        private readonly ISlippage _slippage;

        public PositionsCloser(IDataLoader dataLoader, ICommission commission, ISlippage slippage)
        {
            _dataLoader = dataLoader;
            _commission = commission;
            _slippage = slippage;
        }

        public void Process(DateTime ts, SystemEquity equity,
            Func<Position, StockPricesData, int, bool> positionSelector,
            Func<Position, StockPricesData, int, float> closePriceSelector)
        {
            if (equity.PositionsActive.Count == 0) return;
            ProcessActivePrositions(ts, equity, positionSelector, closePriceSelector);
        }

        private void ProcessActivePrositions(DateTime ts, SystemEquity equity,
            Func<Position, StockPricesData, int, bool> positionSelector,
            Func<Position, StockPricesData, int, float> closePriceSelector)
        {
            int i = 0;
            while (i < equity.PositionsActive.Count)
            {
                StockPricesData pricesData = GetPricesData(equity.PositionsActive[i], ts);
                int pricesDataIndex = pricesData.FindByTS(ts);
                if (positionSelector(equity.PositionsActive[i], pricesData, pricesDataIndex))
                    ProcessPosition(ts, equity, i, closePriceSelector, pricesData, pricesDataIndex);
                else
                    i++;
            }
        }

        private void ProcessPosition(DateTime ts, SystemEquity equity, int positionIndex, Func<Position, StockPricesData, int, float> closePriceSelector, StockPricesData pricesData, int pricesDataIndex)
        {
            equity.Close(positionIndex, ts, closePriceSelector(equity.PositionsActive[positionIndex], pricesData, pricesDataIndex), _slippage, _commission);
        }

        private StockPricesData GetPricesData(Position position, DateTime ts)
        {
            return _dataLoader.Get(position.Stock.Name, position.DataRange, position.IntradayInterval, ts, ts);
        }
    }
}
