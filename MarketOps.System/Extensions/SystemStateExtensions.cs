using MarketOps.StockData.Types;
using MarketOps.System.Interfaces;
using System;
using System.Linq;

namespace MarketOps.System.Extensions
{
    /// <summary>
    /// Extensions to SystemState class.
    /// </summary>
    public static class SystemStateExtensions
    {
        public static void Open(this SystemState systemState, StockDefinition stock, PositionDir dir, DateTime ts, float price, int volume, float commission, StockDataRange dataRange, int intradayInterval, Signal entrySignal)
        {
            Position pos = new Position
            {
                Stock = stock,
                DataRange = dataRange,
                IntradayInterval = intradayInterval,
                Direction = dir,
                Open = price,
                OpenCommission = commission,
                TSOpen = ts,
                Volume = volume,
                EntrySignal = entrySignal,
                TicksActive = 1
            };
            systemState.PositionsActive.Add(pos);
            systemState.Cash -= pos.DirectionMultiplier() * pos.OpenValue();
            systemState.Cash -= pos.OpenCommission;
        }

        public static void Open(this SystemState systemState, DateTime ts, PositionDir dir, float price, Signal signal, ISlippage slippage, ICommission commission)
        {
            float openPrice = systemState.CalculateSlippageOpen(slippage, ts, signal, price);
            systemState.Open(signal.Stock, dir, ts, openPrice, signal.Volume, systemState.CalculateCommission(commission, signal, openPrice), signal.DataRange, signal.IntradayInterval, signal);
        }

        public static void Close(this SystemState system, int positionIndex, DateTime ts, float price, float commission)
        {
            Position pos = system.PositionsActive[positionIndex];
            pos.Close = price;
            pos.CloseCommission = commission;
            pos.TSClose = ts;
            system.PositionsActive.RemoveAt(positionIndex);
            system.PositionsClosed.Add(pos);
            system.Cash += pos.DirectionMultiplier() * pos.CloseValue();
            system.Cash -= pos.CloseCommission;
            system.ClosedPositionsEquity.Add(new SystemValue()
            {
                Value = system.ClosedPositionsEquity.LastOrDefault().Value + pos.Value() - pos.OpenCommission - pos.CloseCommission,
                TS = ts
            });
        }

        public static void Close(this SystemState systemState, int positionIndex, DateTime ts, float price, ISlippage slippage, ICommission commission)
        {
            float closePrice = systemState.CalculateSlippageClose(slippage, ts, positionIndex, price);
            systemState.Close(positionIndex, ts, closePrice, systemState.CalculateCommission(commission, positionIndex, closePrice));
        }

        public static void CloseAll(this SystemState systemState, DateTime ts, float price, ISlippage slippage, ICommission commission)
        {
            while (systemState.PositionsActive.Count > 0)
                systemState.Close(0, ts, price, slippage, commission);
        }

        public static void CalcCurrentValue(this SystemState systemState, DateTime ts, ISystemDataLoader dataLoader)
        {
            systemState.Equity.Add(new SystemValue()
            {
                Value = new SystemValueCalculator().Calc(systemState, ts, dataLoader),
                TS = ts
            });
        }

        public static float CalculateCommission(this SystemState systemState, ICommission commission, StockType stockType, int volume, float price)
        {
            return commission.Calculate(stockType, volume, price);
        }

        public static float CalculateCommission(this SystemState systemState, ICommission commission, int positionIndex, float price)
        {
            return systemState.CalculateCommission(commission, systemState.PositionsActive[positionIndex].Stock.Type, systemState.PositionsActive[positionIndex].Volume, price);
        }

        public static float CalculateCommission(this SystemState systemState, ICommission commission, Signal signal, float price)
        {
            return systemState.CalculateCommission(commission, signal.Stock.Type, signal.Volume, price);
        }

        public static float CalculateSlippageOpen(this SystemState systemState, ISlippage slippage, StockType stockType, DateTime ts, PositionDir dir, float price)
        {
            return slippage.CalculateOpen(stockType, ts, dir, price);
        }

        public static float CalculateSlippageOpen(this SystemState systemState, ISlippage slippage, DateTime ts, Signal signal, float price)
        {
            return systemState.CalculateSlippageOpen(slippage, signal.Stock.Type, ts, signal.Direction, price);
        }

        public static float CalculateSlippageClose(this SystemState systemState, ISlippage slippage, StockType stockType, DateTime ts, PositionDir dir, float price)
        {
            return slippage.CalculateClose(stockType, ts, dir, price);
        }

        public static float CalculateSlippageClose(this SystemState systemState, ISlippage slippage, DateTime ts, int positionIndex, float price)
        {
            return systemState.CalculateSlippageClose(slippage, systemState.PositionsActive[positionIndex].Stock.Type, ts, systemState.PositionsActive[positionIndex].Direction, price);
        }
    }
}
