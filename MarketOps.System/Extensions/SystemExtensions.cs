using MarketOps.StockData.Types;
using MarketOps.System.Interfaces;
using System;
using System.Linq;

namespace MarketOps.System.Extensions
{
    /// <summary>
    /// Extensions to System class.
    /// </summary>
    public static class SystemExtensions
    {
        public static void Open(this System system, StockDefinition stock, PositionDir dir, DateTime ts, float price, int volume, StockDataRange dataRange, int intradayInterval)
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

        public static void Close(this System system, int positionIndex, DateTime ts, float price)
        {
            Position pos = system.PositionsActive[positionIndex];
            pos.Close = price;
            pos.TSClose = ts;
            system.PositionsActive.RemoveAt(positionIndex);
            system.PositionsClosed.Add(pos);
            system.Cash += pos.ClosedValue();
            system.ValueOnPositions.Add(system.ValueOnPositions.LastOrDefault() + pos.ClosedValue());
        }

        public static void CloseAll(this System system, DateTime ts, float price)
        {
            while (system.PositionsActive.Count > 0)
                system.Close(0, ts, price);
        }

        public static void CalcCurrentValue(this System system, DateTime ts, IDataLoader dataLoader)
        {
            system.Value.Add(new SystemValueCalculator().Calc(system, ts, dataLoader));
        }
    }
}
