using MarketOps.SystemAnalysis.DrawDowns;

namespace MarketOps.SystemAnalysis.MonteCarlo
{
    /// <summary>
    /// Calculates max draw down from monte carlo data.
    /// </summary>
    internal class MonteCarloMaxDrawDownCalculator
    {
        private float _maxDrawDown;
        private int _longestDrawDown;

        public void Calculate(MonteCarloResult data, out float maxDrawDown, out int longestDrawDown)
        {
            float[] buffer = new float[data.AverageData.Length];

            _maxDrawDown = 0;
            _longestDrawDown = 0;
            for (int i = 0; i < data.Count; i++)
            {
                CopyToBuffer(data.Data[i]);
                DrawDownsCalculator.Calculate(buffer, OnDrawDown);
            }
            maxDrawDown = _maxDrawDown;
            longestDrawDown = _longestDrawDown;

            void CopyToBuffer(double[] source)
            {
                for(int i=0;  i < source.Length; i++)
                    buffer[i] = (float)source[i];
            }
        }

        private void OnDrawDown(int startIndex, int lastIndex, int bottomIndex, float topValue, float bottomValue)
        {
            float currentDrawDown = (topValue - bottomValue) / topValue;
            int currentDrowDownLength = lastIndex - startIndex;

            if (currentDrawDown > _maxDrawDown) _maxDrawDown = currentDrawDown;
            if (currentDrowDownLength > _longestDrawDown) _longestDrawDown = currentDrowDownLength;
        }
    }
}
