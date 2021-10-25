using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using System;
using MarketOps.SystemData.Types;
using MarketOps.SystemData.Extensions;

namespace MarketOps.SystemExecutor.Processor
{
    /// <summary>
    /// System active positions closer.
    /// </summary>
    internal class PositionsCloser
    {
        private readonly ISystemDataLoader _dataLoader;
        private readonly ICommission _commission;
        private readonly ISlippage _slippage;

        public PositionsCloser(ISystemDataLoader dataLoader, ICommission commission, ISlippage slippage)
        {
            _dataLoader = dataLoader;
            _commission = commission;
            _slippage = slippage;
        }

        public void Process(DateTime ts, SystemState systemState,
            Func<Position, StockPricesData, int, bool> positionSelector,
            Func<Position, StockPricesData, int, float> closePriceSelector)
        {
            if (systemState.PositionsActive.Count == 0) return;
            ProcessActivePrositions(ts, systemState, positionSelector, closePriceSelector);
        }

        private void ProcessActivePrositions(DateTime ts, SystemState systemState,
            Func<Position, StockPricesData, int, bool> positionSelector,
            Func<Position, StockPricesData, int, float> closePriceSelector)
        {
            int i = 0;
            while (i < systemState.PositionsActive.Count)
            {
                StockPricesData pricesData = GetPricesData(systemState.PositionsActive[i], ts);
                int pricesDataIndex = pricesData.FindByTS(ts);
                if (positionSelector(systemState.PositionsActive[i], pricesData, pricesDataIndex))
                    ProcessPosition(ts, systemState, i, closePriceSelector, pricesData, pricesDataIndex);
                else
                    i++;
            }
        }

        private void ProcessPosition(DateTime ts, SystemState systemState, int positionIndex, Func<Position, StockPricesData, int, float> closePriceSelector, StockPricesData pricesData, int pricesDataIndex)
        {
            systemState.Close(positionIndex, ts, closePriceSelector(systemState.PositionsActive[positionIndex], pricesData, pricesDataIndex), _slippage, _commission);
        }

        private StockPricesData GetPricesData(Position position, DateTime ts)
        {
            return _dataLoader.Get(position.Stock.FullName, position.DataRange, position.IntradayInterval, ts, ts);
        }
    }
}
