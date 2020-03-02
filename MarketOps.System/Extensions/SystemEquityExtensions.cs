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
        public static void Open(this SystemEquity system, StockDefinition stock, PositionDir dir, DateTime ts, float price, int volume, StockDataRange dataRange, int intradayInterval)
        {
            Position pos = new Position
            {
                Stock = stock,
                DataRange = dataRange,
                IntradayInterval = intradayInterval,
                Direction = dir,
                Open = price,
                TSOpen = ts,
                Volume = volume
            };
            system.PositionsActive.Add(pos);
            system.Cash -= pos.OpenValue();
        }

        public static void Close(this SystemEquity system, int positionIndex, DateTime ts, float price)
        {
            Position pos = system.PositionsActive[positionIndex];
            pos.Close = price;
            pos.TSClose = ts;
            system.PositionsActive.RemoveAt(positionIndex);
            system.PositionsClosed.Add(pos);
            system.Cash += pos.CloseValue();
            system.ClosedPositionsValue.Add(new SystemValue()
            {
                Value = system.ClosedPositionsValue.LastOrDefault().Value + pos.Value(),
                TS = ts
            });
        }

        public static void CloseAll(this SystemEquity system, DateTime ts, float price)
        {
            while (system.PositionsActive.Count > 0)
                system.Close(0, ts, price);
        }

        public static void CalcCurrentValue(this SystemEquity system, DateTime ts, IDataLoader dataLoader)
        {
            system.Value.Add(new SystemValue()
            {
                Value = new SystemValueCalculator().Calc(system, ts, dataLoader),
                TS = ts
            });
        }
    }
}
