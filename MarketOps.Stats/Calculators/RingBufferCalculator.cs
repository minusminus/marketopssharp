using System;

namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Executes operation on ring buffer with specified length.
    /// Returns array of calculated values.
    /// </summary>
    public static class RingBufferCalculator
    {
        public static float[] Calculate(float[] data, int bufferLength, Func<float[], float> operation)
        {
            if (!CanCalculate(data, bufferLength)) return new float[0];

            var buffer = new RingBuffer<float>(bufferLength);
            InitialBufferFill(buffer, data);
            return CalculateResult(buffer, data, operation);
        }

        private static bool CanCalculate(float[] data, int bufferLength) =>
            (data.Length >= bufferLength) && (bufferLength > 0);

        private static void InitialBufferFill(RingBuffer<float> buffer, float[] data)
        {
            for (int i = 0; i < buffer.Length - 1; i++)
                buffer.Add(data[i]);
        }

        private static float[] CalculateResult(RingBuffer<float> buffer, float[] data, Func<float[], float> operation)
        {
            float[] res = new float[data.Length - buffer.Length + 1];

            for (int i = buffer.Length - 1; i < data.Length; i++)
            {
                buffer.Add(data[i]);
                res[i - (buffer.Length - 1)] = operation(buffer.Buffer);
            }

            return res;
        }
    }
}
