using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;
using System.Linq;

namespace MarketOps.SystemData.Extensions
{
    /// <summary>
    /// Extensions to SystemState class.
    /// </summary>
    public static class SystemStateExtensions
    {
        public static void Open(this SystemState systemState, StockDefinition stock, PositionDir dir, DateTime ts, float price, float volume, float commission, StockDataRange dataRange, int intradayInterval, Signal entrySignal)
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
                CloseMode = (entrySignal.InitialStopMode == SignalInitialStopMode.OnPrice) ? PositionCloseMode.OnStopHit : PositionCloseMode.DontClose,
                CloseModePrice = (entrySignal.InitialStopMode == SignalInitialStopMode.OnPrice) ? entrySignal.InitialStopValue : 0,
                TicksActive = 1
            };
            systemState.PositionsActive.Add(pos);
            systemState.Cash -= pos.DirectionMultiplier() * pos.OpenValue();
            systemState.Cash -= pos.OpenCommission;
        }

        public static void Open(this SystemState systemState, DateTime ts, PositionDir dir, float price, Signal signal, ISlippage slippage, ICommission commission)
        {
            systemState.Open(ts, dir, price, signal.Volume, signal, slippage, commission);
        }

        public static void Open(this SystemState systemState, DateTime ts, PositionDir dir, float price, float volume, Signal signal, ISlippage slippage, ICommission commission)
        {
            float openPrice = systemState.CalculateSlippageOpen(slippage, ts, signal, price);
            systemState.Open(signal.Stock, dir, ts, openPrice, volume, systemState.CalculateCommission(commission, signal, openPrice), signal.DataRange, signal.IntradayInterval, signal);
        }

        public static void Open(this SystemState systemState, DateTime ts, PositionDir dir, float price, float volume, StockDefinition stock, Signal signal, ISlippage slippage, ICommission commission)
        {
            float openPrice = systemState.CalculateSlippageOpen(slippage, stock.Type, ts, signal.Direction, price);
            systemState.Open(stock, dir, ts, openPrice, volume, systemState.CalculateCommission(commission, stock.Type, signal.Volume, openPrice), signal.DataRange, signal.IntradayInterval, signal);
        }

        public static void Close(this SystemState systemState, int positionIndex, DateTime ts, float price, float commission)
        {
            Position pos = systemState.PositionsActive[positionIndex];
            pos.Close = price;
            pos.CloseCommission = commission;
            pos.TSClose = ts;
            pos.R = pos.RValue();
            pos.RProfit = pos.CalculateRProfit();
            systemState.PositionsActive.RemoveAt(positionIndex);
            systemState.PositionsClosed.Add(pos);
            systemState.Cash += pos.DirectionMultiplier() * pos.CloseValue();
            systemState.Cash -= pos.CloseCommission;
            systemState.ClosedPositionsEquity.Add(new SystemValue()
            {
                Value = systemState.ClosedPositionsEquity.LastOrDefault().Value + pos.Value() - pos.OpenCommission - pos.CloseCommission,
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

        public static void AddToPosition(this SystemState systemState, int positionIndex, DateTime ts, float price, float volume, Signal signal, ISlippage slippage, ICommission commission)
        {
            Position pos = systemState.PositionsActive[positionIndex];
            systemState.Close(positionIndex, ts, price, slippage, commission);
            if (pos.Volume + volume > 0)
            {
                float openPrice = systemState.CalculateSlippageOpen(slippage, ts, signal, price);
                systemState.Open(signal.Stock, pos.Direction, ts, openPrice, pos.Volume + volume, systemState.CalculateCommission(commission, signal, openPrice), signal.DataRange, signal.IntradayInterval, signal);
            }
        }

        public static void ReducePosition(this SystemState systemState, int positionIndex, DateTime ts, float price, float volume, Signal signal, ISlippage slippage, ICommission commission)
        {
            systemState.AddToPosition(positionIndex, ts, price, -volume, signal, slippage, commission);
        }

        public static void CalcCurrentValue(this SystemState systemState, DateTime ts, ISystemDataLoader dataLoader)
        {
            systemState.Equity.Add(new SystemValue()
            {
                Value = new SystemValueCalculator().Calc(systemState, ts, dataLoader),
                TS = ts
            });
        }

        public static float CalculateCommission(this SystemState systemState, ICommission commission, StockType stockType, float volume, float price)
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
