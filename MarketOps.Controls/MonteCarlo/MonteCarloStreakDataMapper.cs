using MarketOps.SystemAnalysis.MonteCarlo;

namespace MarketOps.Controls.MonteCarlo
{
    /// <summary>
    /// Maps MonteCarloStreakData to grid
    /// </summary>
    internal class MonteCarloStreakDataMapper
    {
        private readonly MonteCarloStreakData _data;
        private readonly float _percent;

        public MonteCarloStreakDataMapper(MonteCarloStreakData data, int totalCount)
        {
            _data = data;
            _percent = 100f * (float)data.Count / (float)totalCount;
        }

        public int Length => _data.Length;
        public int Count => _data.Count;
        public float Percent => _percent;
    }
}
