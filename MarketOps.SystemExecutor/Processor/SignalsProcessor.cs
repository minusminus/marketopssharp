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
        private readonly PositionsRebalancer _rebalancer;

        public SignalsProcessor(ISystemDataLoader dataLoader, ICommission commission, ISlippage slippage)
        {
            _dataLoader = dataLoader;
            _commission = commission;
            _slippage = slippage;
            _rebalancer = new PositionsRebalancer(_dataLoader, _commission, _slippage);
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
                VerifySignal(systemState.Signals[i]);
                if (systemState.Signals[i].Rebalance)
                {
                    if (signalSelector(systemState.Signals[i], null, -1))
                        ProcessRebalanceSignal(systemState.Signals[i], ts, systemState, openPriceSelector);
                }
                else
                {
                    (StockPricesData pricesData, int pricesDataIndex) = _dataLoader.GetPricesDataAndIndex(systemState.Signals[i].Stock.Name, systemState.Signals[i].DataRange, systemState.Signals[i].IntradayInterval, ts);
                    if (signalSelector(systemState.Signals[i], pricesData, pricesDataIndex))
                        ProcessStandardSignal(systemState.Signals[i], ts, systemState, openPriceSelector, pricesData, pricesDataIndex);
                }
            }
        }

        private void VerifySignal(Signal signal) =>
            SignalVerifier.Verify(signal);

        private void ProcessRebalanceSignal(Signal signal, DateTime ts, SystemState systemState, Func<Signal, StockPricesData, int, float> openPriceSelector) =>
            _rebalancer.Rebalance(signal, ts, systemState, openPriceSelector);

        private void ProcessStandardSignal(Signal signal, DateTime ts, SystemState systemState, Func<Signal, StockPricesData, int, float> openPriceSelector, StockPricesData pricesData, int pricesDataIndex)
        {
            float openPrice = openPriceSelector(signal, pricesData, pricesDataIndex);

            if (signal.ReversePosition)
                ReversePosition(signal, ts, systemState, openPrice);
            else
                OpenPosition(signal, ts, systemState, openPrice);
        }

        private void OpenPosition(Signal signal, DateTime ts, SystemState systemState, float openPrice) => 
            systemState.Open(ts, signal.Direction, openPrice, signal, _slippage, _commission);

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

        private int FindActivePositionIndex(SystemState systemState, StockDefinition stock) =>
            systemState.PositionsActive.FindIndex(p => p.Stock.ID == stock.ID);
    }
}
