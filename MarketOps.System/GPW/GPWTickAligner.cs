using System;
using MarketOps.StockData.Types;
using MarketOps.System.Interfaces;

namespace MarketOps.System.GPW
{
    /// <summary>
    /// Alignes value to possible price according to specifed date for GPW.
    /// 
    /// Before and after 2019-03-04 in 6 liquidity streams.
    /// By default highest liquidity stream is taken.
    /// </summary>
    internal class GPWTickAligner : ITickAligner
    {
        private readonly DateTime _changeDate = new DateTime(2019, 3, 4);
        private readonly float _stocksMinPrice = 0.01F;

        private readonly float[][] _stocksTicksPre20190304 =
        {
            new float[] {0.01F, 0.02F, 0.05F, 0.10F, 0.20F, 0.50F, 1F, 2F, 5F, 10F, 20F, 50F, 100F, 200F, 500F},
            new float[] {0.01F, 0.02F, 0.05F, 0.10F, 0.20F, 0.50F, 1F, 2F, 5F, 10F, 20F, 50F, 100F, 200F},
            new float[] {0.01F, 0.02F, 0.05F, 0.10F, 0.20F, 0.50F, 1F, 2F, 5F, 10F, 20F, 50F, 100F},
            new float[] {0.01F, 0.02F, 0.05F, 0.10F, 0.20F, 0.50F, 1F, 2F, 5F, 10F, 20F, 50F},
            new float[] {0.01F, 0.02F, 0.05F, 0.10F, 0.20F, 0.50F, 1F, 2F, 5F, 10F, 20F},
            new float[] {0.01F, 0.02F, 0.05F, 0.10F, 0.20F, 0.50F, 1F, 2F, 5F, 10F},
        };

        private readonly float[][] _stocksPriceBordersPre20190304 =
        {
            new float[] {2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, Single.MaxValue},
            new float[] {5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, Single.MaxValue},
            new float[] {10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, Single.MaxValue},
            new float[] {20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, Single.MaxValue},
            new float[] {50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, Single.MaxValue},
            new float[] {100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, Single.MaxValue},
        };

        private readonly float[][] _stocksTicksPost20190304 =
        {
            new float[] {0.0005F, 0.001F, 0.002F, 0.005F, 0.01F, 0.02F, 0.05F, 0.10F, 0.20F, 0.50F, 1, 2, 5, 10, 20, 50, 100, 200, 500},
            new float[] {0.0002F, 0.0005F, 0.001F, 0.002F, 0.005F, 0.01F, 0.02F, 0.05F, 0.10F, 0.20F, 0.50F, 1, 2, 5, 10, 20, 50, 100, 200},
            new float[] {0.0001F, 0.0002F, 0.0005F, 0.001F, 0.002F, 0.005F, 0.01F, 0.02F, 0.05F, 0.10F, 0.20F, 0.50F, 1, 2, 5, 10, 20, 50, 100},
            new float[] {0.0001F, 0.0001F, 0.0002F, 0.0005F, 0.001F, 0.002F, 0.005F, 0.01F, 0.02F, 0.05F, 0.10F, 0.20F, 0.50F, 1, 2, 5, 10, 20, 50},
            new float[] {0.0001F, 0.0001F, 0.0001F, 0.0002F, 0.0005F, 0.001F, 0.002F, 0.005F, 0.01F, 0.02F, 0.05F, 0.10F, 0.20F, 0.50F, 1, 2, 5, 10, 20},
            new float[] {0.0001F, 0.0001F, 0.0001F, 0.0001F, 0.0002F, 0.0005F, 0.001F, 0.002F, 0.005F, 0.01F, 0.02F, 0.05F, 0.10F, 0.20F, 0.50F, 1, 2, 5, 10},
        };

        private readonly float[] _stocksPriceBordersPost20190304 =
            {0.1F, 0.2F, 0.5F, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, Single.MaxValue,};

        public float Up(StockType stockType, DateTime ts, float value)
        {
            if (stockType == StockType.Stock)
                return StockUp(ts, value);
            return value;
        }

        public float Down(StockType stockType, DateTime ts, float value)
        {
            if (stockType == StockType.Stock)
                return StockDown(ts, value);
            return value;
        }

        private float StockUp(DateTime ts, float value)
        {
            float tickSize = GetStockTickSize(ts, value);
            float reducedToOne = value/tickSize;
            return NoReminder(reducedToOne) ? value : BorderStockToMinValue((float) Math.Ceiling(reducedToOne)*tickSize);
        }

        private float StockDown(DateTime ts, float value)
        {
            float tickSize = GetStockTickSize(ts, value);
            float reducedToOne = value / tickSize;
            return NoReminder(reducedToOne) ? value : BorderStockToMinValue((float) Math.Floor(value/tickSize)*tickSize);
        }

        private float GetStockTickSize(DateTime ts, float value)
        {
            return ts < _changeDate
                ? GetStockTickSizeFromArrays(value, _stocksPriceBordersPre20190304[0], _stocksTicksPre20190304[0])
                : GetStockTickSizeFromArrays(value, _stocksPriceBordersPost20190304, _stocksTicksPost20190304[5]);
        }

        private float GetStockTickSizeFromArrays(float value, float[] priceBorders, float[] ticks)
        {
            return ticks[Array.FindIndex(priceBorders, f => value < f)];
        }

        private float BorderStockToMinValue(float value)
        {
            return value < _stocksMinPrice ? _stocksMinPrice : value;
        }

        private bool NoReminder(float value)
        {
            return value%1 == 0;
        }
    }
}
