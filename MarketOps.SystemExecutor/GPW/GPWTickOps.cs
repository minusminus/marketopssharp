using System;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;

namespace MarketOps.SystemExecutor.GPW
{
    /// <summary>
    /// Ticks operations for GPW instruments.
    /// 
    /// Alignes value to possible price according to specifed date for GPW.
    /// Adds ticks to price according to specifed date for GPW.
    /// 
    /// For stocks: before and after 2019-03-04 in 6 liquidity streams.
    /// By default highest liquidity stream is taken.
    /// </summary>
    public class GPWTickOps : ITickAligner, ITickAdder
    {
        private readonly DateTime _changeDate = new DateTime(2019, 3, 4);

        private readonly decimal[][] _stocksTicksPre20190304 =
        {
            new decimal[] {0.01M, 0.02M, 0.05M, 0.10M, 0.20M, 0.50M, 1M, 2M, 5M, 10M, 20M, 50M, 100M, 200M, 500M},
            new decimal[] {0.01M, 0.02M, 0.05M, 0.10M, 0.20M, 0.50M, 1M, 2M, 5M, 10M, 20M, 50M, 100M, 200M},
            new decimal[] {0.01M, 0.02M, 0.05M, 0.10M, 0.20M, 0.50M, 1M, 2M, 5M, 10M, 20M, 50M, 100M},
            new decimal[] {0.01M, 0.02M, 0.05M, 0.10M, 0.20M, 0.50M, 1M, 2M, 5M, 10M, 20M, 50M},
            new decimal[] {0.01M, 0.02M, 0.05M, 0.10M, 0.20M, 0.50M, 1M, 2M, 5M, 10M, 20M},
            new decimal[] {0.01M, 0.02M, 0.05M, 0.10M, 0.20M, 0.50M, 1M, 2M, 5M, 10M},
        };

        private readonly decimal[][] _stocksPriceBordersPre20190304 =
        {
            new decimal[] {2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, Decimal.MaxValue},
            new decimal[] {5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, Decimal.MaxValue},
            new decimal[] {10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, Decimal.MaxValue},
            new decimal[] {20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, Decimal.MaxValue},
            new decimal[] {50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, Decimal.MaxValue},
            new decimal[] {100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, Decimal.MaxValue},
        };

        private readonly decimal[][] _stocksTicksPost20190304 =
        {
            new decimal[] {0.0005M, 0.001M, 0.002M, 0.005M, 0.01M, 0.02M, 0.05M, 0.10M, 0.20M, 0.50M, 1, 2, 5, 10, 20, 50, 100, 200, 500},
            new decimal[] {0.0002M, 0.0005M, 0.001M, 0.002M, 0.005M, 0.01M, 0.02M, 0.05M, 0.10M, 0.20M, 0.50M, 1, 2, 5, 10, 20, 50, 100, 200},
            new decimal[] {0.0001M, 0.0002M, 0.0005M, 0.001M, 0.002M, 0.005M, 0.01M, 0.02M, 0.05M, 0.10M, 0.20M, 0.50M, 1, 2, 5, 10, 20, 50, 100},
            new decimal[] {0.0001M, 0.0001M, 0.0002M, 0.0005M, 0.001M, 0.002M, 0.005M, 0.01M, 0.02M, 0.05M, 0.10M, 0.20M, 0.50M, 1, 2, 5, 10, 20, 50},
            new decimal[] {0.0001M, 0.0001M, 0.0001M, 0.0002M, 0.0005M, 0.001M, 0.002M, 0.005M, 0.01M, 0.02M, 0.05M, 0.10M, 0.20M, 0.50M, 1, 2, 5, 10, 20},
            new decimal[] {0.0001M, 0.0001M, 0.0001M, 0.0001M, 0.0002M, 0.0005M, 0.001M, 0.002M, 0.005M, 0.01M, 0.02M, 0.05M, 0.10M, 0.20M, 0.50M, 1, 2, 5, 10},
        };

        private readonly decimal[] _stocksPriceBordersPost20190304 =
            {0.1M, 0.2M, 0.5M, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, Decimal.MaxValue,};

        public float AlignUp(StockType stockType, DateTime ts, float value) => 
            (float)ExecuteUpDown(Math.Ceiling, stockType, ts, (decimal)value);

        public float AlignDown(StockType stockType, DateTime ts, float value) => 
            (float)ExecuteUpDown(Math.Floor, stockType, ts, (decimal)value);

        public float AddTicks(StockType stockType, DateTime ts, float value, int ticks) => 
            (stockType == StockType.Undefined)
                ? value
                : value + (float)(GetTickSize(stockType, ts, (decimal)value) * ticks);

        private decimal ExecuteUpDown(Func<decimal, decimal> alignOp, StockType stockType, DateTime ts, decimal value) =>
            (stockType == StockType.Undefined)
                ? value
                : AlignValue(alignOp, value, GetTickSize(stockType, ts, value), MinValue(stockType));

        private decimal GetTickSize(StockType stockType, DateTime ts, decimal value) => 
            (stockType == StockType.Stock) ? GetStockTickSize(ts, value) : MinValue(stockType);

        private decimal GetStockTickSize(DateTime ts, decimal value) => 
            (ts < _changeDate)
                ? GetStockTickSizeFromArrays(value, _stocksPriceBordersPre20190304[0], _stocksTicksPre20190304[0])
                : GetStockTickSizeFromArrays(value, _stocksPriceBordersPost20190304, _stocksTicksPost20190304[5]);

        private decimal GetStockTickSizeFromArrays(decimal value, decimal[] priceBorders, decimal[] ticks) => 
            ticks[Array.FindIndex(priceBorders, f => value < f)];

        private decimal AlignValue(Func<decimal, decimal> alignOp, decimal value, decimal tickSize, decimal minValue)
        {
            decimal reducedToOne = value/tickSize;
            return NoReminder(reducedToOne)
                ? value
                : BorderToMinValue(alignOp(reducedToOne) * tickSize, minValue);
        }

        private decimal BorderToMinValue(decimal value, decimal minValue) => 
            (value < minValue) ? minValue : value;

        private bool NoReminder(decimal value) => 
            (value % 1) == 0;

        private decimal MinValue(StockType stockType)
        {
            switch (stockType)
            {
                case StockType.Stock:
                    return 0.01M;
                case StockType.Index:
                    return 0.01M;
                case StockType.IndexFuture:
                    return 1;
                case StockType.InvestmentFund:
                    return 0.0001M;
                case StockType.Forex:
                    return 0.000001M;
                case StockType.NBPCurrency:
                    return 0.0001M;
            }
            return 0;
        }
    }
}
