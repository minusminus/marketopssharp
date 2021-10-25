using System;

namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Executes operation on ring buffer with specified length.
    /// Returns array of calculated values.
    /// </summary>
    public static class RingBufferCalculator<T>
    {
        public static T[] Calculate(T[] data, int bufferLength, Func<T[], T> operation)
        {
            if (!CanCalculate(data, bufferLength)) return new T[0];

            var buffer = new RingBuffer<T>(bufferLength);
            InitialBufferFill(buffer, data);
            return CalculateResult(buffer, data, operation);
        }

        private static bool CanCalculate(T[] data, int bufferLength) =>
            (data.Length >= bufferLength) && (bufferLength > 0);

        private static void InitialBufferFill(RingBuffer<T> buffer, T[] data)
        {
            for (int i = 0; i < buffer.Length - 1; i++)
                buffer.Add(data[i]);
        }

        private static T[] CalculateResult(RingBuffer<T> buffer, T[] data, Func<T[], T> operation)
        {
            T[] res = new T[data.Length - buffer.Length + 1];

            for (int i = buffer.Length - 1; i < data.Length; i++)
            {
                buffer.Add(data[i]);
                res[i - (buffer.Length - 1)] = operation(buffer.Buffer);
            }

            return res;
        }
    }
}
