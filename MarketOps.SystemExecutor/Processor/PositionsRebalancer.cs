using MarketOps.StockData.Types;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using MarketOps.SystemExecutor.Extensions;
using System;

namespace MarketOps.SystemExecutor.Processor
{
    /// <summary>
    /// Rebalances all positions based on new balance provided in signal.
    /// </summary>
    internal class PositionsRebalancer
    {
        private readonly ISystemDataLoader _dataLoader;
        private readonly ICommission _commission;
        private readonly ISlippage _slippage;

        public PositionsRebalancer(ISystemDataLoader dataLoader, ICommission commission, ISlippage slippage)
        {
            _dataLoader = dataLoader;
            _commission = commission;
            _slippage = slippage;
        }

        public void Rebalance(Signal signal, DateTime ts, SystemState systemState, Func<Signal, StockPricesData, int, float> openPriceSelector)
        {
            CloseAllPositions(signal, ts, systemState, openPriceSelector);
            OpenNewPositions(GetTotalValue(systemState), signal, ts, systemState, openPriceSelector);
        }

        private void CloseAllPositions(Signal signal, DateTime ts, SystemState systemState, Func<Signal, StockPricesData, int, float> openPriceSelector)
        {
            while (systemState.PositionsActive.Count > 0)
            {
                float closePrice = CalculatePrice(systemState.PositionsActive[0].Stock.Name, systemState.PositionsActive[0].DataRange, systemState.PositionsActive[0].IntradayInterval, signal, ts, openPriceSelector);
                systemState.Close(0, ts, closePrice, _commission.Calculate(systemState.PositionsActive[0].Stock.Type, systemState.PositionsActive[0].Volume, closePrice));
            }
        }

        private float GetTotalValue(SystemState systemState) => systemState.Cash;

        private void OpenNewPositions(float totalValue, Signal signal, DateTime ts, SystemState systemState, Func<Signal, StockPricesData, int, float> openPriceSelector)
        {
            foreach ((StockDefinition stockDef, float balance) in signal.NewBalance)
            {
                float openPrice = CalculatePrice(stockDef.Name, signal.DataRange, signal.IntradayInterval, signal, ts, openPriceSelector);
                float balancedVolume = (totalValue * balance / openPrice).TruncateToAllowedVolume(signal.Stock.Type);
                systemState.Open(ts, signal.Direction, openPrice, balancedVolume, signal, _slippage, _commission);
            }
        }

        private float CalculatePrice(string stockName, StockDataRange dataRange, int intradayInterval, Signal signal, DateTime ts, Func<Signal, StockPricesData, int, float> openPriceSelector)
        {
            (StockPricesData pricesData, int pricesDataIndex) = _dataLoader.GetPricesDataAndIndex(stockName, dataRange, intradayInterval, ts);
            return openPriceSelector(signal, pricesData, pricesDataIndex);
        }
    }
}
