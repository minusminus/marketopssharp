using System;
using System.Collections.Generic;

namespace MarketOps.Maths
{
    /// <summary>
    /// Calculates changes of data in percents current to previous.
    /// </summary>
    public static class PercentChanges
    {
        public static float[] Calculate(float[] data) =>
            Calculate(data, 0, data.Length);

        public static float[] Calculate(float[] data, int startIndex, int length)
        {
            if (!CanCalculate(data.Length, startIndex, length)) return new float[0];

            float[] result = new float[length - 1];
            for (int i = 1; i < length; i++)
                result[i - 1] = ChangeInPercent.Calculate(data[startIndex + i], data[startIndex + i - 1]);
            return result;
        }

        public static float[] Calculate<T>(List<T> data, Func<T, float> valueGetter) =>
            Calculate<T>(data, 0, data.Count, valueGetter);

        public static float[] Calculate<T>(List<T> data, int startIndex, int length, Func<T, float> valueGetter)
        {
            if (!CanCalculate(data.Count, startIndex, length)) return new float[0];

            float[] result = new float[length - 1];
            for (int i = 1; i < length; i++)
                result[i - 1] = ChangeInPercent.Calculate(valueGetter(data[startIndex + i]), valueGetter(data[startIndex + i - 1]));
            return result;
        }

        private static bool CanCalculate(int dataLength, int startIndex, int length) =>
            (dataLength > 0)
            && (dataLength >= startIndex + length)
            && (startIndex >= 0)
            && (length > 0);
    }
}
