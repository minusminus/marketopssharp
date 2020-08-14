using MarketOps.StockData.Types;
using MarketOps.System.Interfaces;
using System;
using System.Linq;

namespace MarketOps.System.Extensions
{
    /// <summary>
    /// Extensions to System class.
    /// </summary>
    public static class SystemEquityExtensions
    {
        public static void Open(this SystemEquity system, StockDefinition stock, PositionDir dir, DateTime ts, float price, int volume, float commission, StockDataRange dataRange, int intradayInterval, Signal entrySignal)
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
                EntrySignal = entrySignal
            };
            system.PositionsActive.Add(pos);
            system.Cash -= pos.OpenValue();
            system.Cash -= pos.OpenCommission;
        }

        public static void Open(this SystemEquity system, DateTime ts, PositionDir dir, float price, Signal signal, ISlippage slippage, ICommission commission)
        {
            float openPrice = system.CalculateSlippageOpen(slippage, ts, signal, price);
            system.Open(signal.Stock, dir, ts, openPrice, signal.Volume, system.CalculateCommission(commission, signal, openPrice), signal.DataRange, signal.IntradayInterval, signal);
        }

        public static void Close(this SystemEquity system, int positionIndex, DateTime ts, float price, float commission)
        {
            Position pos = system.PositionsActive[positionIndex];
            pos.Close = price;
            pos.CloseCommission = commission;
            pos.TSClose = ts;
            system.PositionsActive.RemoveAt(positionIndex);
            system.PositionsClosed.Add(pos);
            system.Cash += pos.CloseValue();
            system.Cash -= pos.CloseCommission;
            system.ClosedPositionsValue.Add(new SystemValue()
            {
                Value = system.ClosedPositionsValue.LastOrDefault().Value + pos.Value() - pos.OpenCommission - pos.CloseCommission,
                TS = ts
            });
        }

        public static void Close(this SystemEquity system, int positionIndex, DateTime ts, float price, ISlippage slippage, ICommission commission)
        {
            float closePrice = system.CalculateSlippageClose(slippage, ts, positionIndex, price);
            system.Close(positionIndex, ts, closePrice, system.CalculateCommission(commission, positionIndex, closePrice));
        }

        public static void CloseAll(this SystemEquity system, DateTime ts, float price, ISlippage slippage, ICommission commission)
        {
            while (system.PositionsActive.Count > 0)
                system.Close(0, ts, price, slippage, commission);
        }

        public static void CalcCurrentValue(this SystemEquity system, DateTime ts, IDataLoader dataLoader)
        {
            system.Value.Add(new SystemValue()
            {
                Value = new SystemValueCalculator().Calc(system, ts, dataLoader),
                TS = ts
            });
        }

        public static float CalculateCommission(this SystemEquity system, ICommission commission, StockType stockType, int volume, float price)
        {
            return commission.Calculate(stockType, volume, price);
        }

        public static float CalculateCommission(this SystemEquity system, ICommission commission, int positionIndex, float price)
        {
            return system.CalculateCommission(commission, system.PositionsActive[positionIndex].Stock.Type, system.PositionsActive[positionIndex].Volume, price);
        }

        public static float CalculateCommission(this SystemEquity system, ICommission commission, Signal signal, float price)
        {
            return system.CalculateCommission(commission, signal.Stock.Type, signal.Volume, price);
        }

        public static float CalculateSlippageOpen(this SystemEquity system, ISlippage slippage, StockType stockType, DateTime ts, PositionDir dir, float price)
        {
            return slippage.CalculateOpen(stockType, ts, dir, price);
        }

        public static float CalculateSlippageOpen(this SystemEquity system, ISlippage slippage, DateTime ts, Signal signal, float price)
        {
            return system.CalculateSlippageOpen(slippage, signal.Stock.Type, ts, signal.Direction, price);
        }

        public static float CalculateSlippageClose(this SystemEquity system, ISlippage slippage, StockType stockType, DateTime ts, PositionDir dir, float price)
        {
            return slippage.CalculateClose(stockType, ts, dir, price);
        }

        public static float CalculateSlippageClose(this SystemEquity system, ISlippage slippage, DateTime ts, int positionIndex, float price)
        {
            return system.CalculateSlippageClose(slippage, system.PositionsActive[positionIndex].Stock.Type, ts, system.PositionsActive[positionIndex].Direction, price);
        }
    }
}
