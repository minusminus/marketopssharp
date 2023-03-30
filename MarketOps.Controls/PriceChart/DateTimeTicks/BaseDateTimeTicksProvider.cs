using ScottPlot;
using System;

namespace MarketOps.Controls.PriceChart.DateTimeTicks
{
    /// <summary>
    /// Base class for calculating datetime ticks string values for price chart.
    /// 
    /// Requires sequential turned on on candlesticks chart.
    /// </summary>
    internal abstract class BaseDateTimeTicksProvider : IDateTimeTicksProvider
    {
        private const int InternalTicksCount = 8;
        private const int TotalTicksCount = InternalTicksCount + 2;

        public (string[] values, double[] positions) Get(in DateTime[] tsArray, in AxisLimits axisLimits)
        {
            int iMin = GetRangeIndex(axisLimits.XMin, tsArray.Length);
            int iMax = GetRangeIndex(axisLimits.XMax, tsArray.Length);
            return GenerateValues(iMin, iMax, GetStep(iMax, iMin), tsArray);
        }

        protected abstract string MapTsToString(DateTime ts);

        private int GetRangeIndex(double value, int tsArrayLength)
        {
            int result = (int)Math.Floor(value);
            if (result < 0) result = 0;
            if (result >= tsArrayLength) result = tsArrayLength - 1;
            return result;
        }

        private int GetStep(int iMax, int iMin)
        {
            int result = (iMax - iMin) / InternalTicksCount;
            return (result > 0) ? result : 1;
        }

        private (string[], double[]) GenerateValues(int iStart, int iStop, int step, in DateTime[] tsArray)
        {
            string[] values = new string[TotalTicksCount];
            double[] positions = new double[TotalTicksCount];

            int currentIndex = 0;
            for (int i = iStart; (i <= iStop) && (i < tsArray.Length) && (currentIndex < values.Length); i += step, currentIndex++)
            {
                positions[currentIndex] = i;
                values[currentIndex] = MapTsToString(tsArray[i]);
            }

            return (values, positions);
        }
    }
}
