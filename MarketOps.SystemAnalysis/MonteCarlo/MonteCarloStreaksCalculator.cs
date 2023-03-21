using System.Collections.Generic;
using System.Linq;

namespace MarketOps.SystemAnalysis.MonteCarlo
{
    /// <summary>
    /// Calculates all streaks lengths from monte carlo simulation.
    /// </summary>
    internal static class MonteCarloStreaksCalculator
    {
        enum StreakDirection {Up, Down};

        private class CurrentStreak
        {
            public StreakDirection Direction;
            public int Length;
        }

        public static void Calculate(double[][] data, out List<MonteCarloStreakData> resultWinningStreaks, out List<MonteCarloStreakData> resultLosingStreaks)
        {
            Dictionary<int, MonteCarloStreakData> winningStreaks = new Dictionary<int, MonteCarloStreakData>();
            Dictionary<int, MonteCarloStreakData> losingStreaks = new Dictionary<int, MonteCarloStreakData>();

            if ((data.Length > 0) && (data[0].Length > 1))
                for (int i = 0; i < data.Length; i++)
                    CalculateForSingleRow(data[i], winningStreaks, losingStreaks);

            resultWinningStreaks = GetOrderedListOfStreaks(winningStreaks);
            resultLosingStreaks = GetOrderedListOfStreaks(losingStreaks);
        }

        private static void CalculateForSingleRow(double[] row, Dictionary<int, MonteCarloStreakData> winningStreaks, Dictionary<int, MonteCarloStreakData> losingStreaks)
        {
            CurrentStreak currentStreak = new CurrentStreak();
            InitializeCurrentStreak(currentStreak, row[1], row[0]);
            for (int i = 2; i < row.Length; i++)
            {
                if (currentStreak.Direction == GetDirection(row[i], row[i - 1]))
                    currentStreak.Length++;
                else
                {
                    UpdateStreakCount(GetStreaks(currentStreak.Direction, winningStreaks, losingStreaks), currentStreak.Length);
                    InitializeCurrentStreak(currentStreak, row[i], row[i - 1]);
                }
            }
            UpdateStreakCount(GetStreaks(currentStreak.Direction, winningStreaks, losingStreaks), currentStreak.Length);
        }

        private static void InitializeCurrentStreak(CurrentStreak currentStreak, double secondValue, double firstValue)
        {
            currentStreak.Direction = GetDirection(secondValue, firstValue);
            currentStreak.Length = 1;
        }

        private static Dictionary<int, MonteCarloStreakData> GetStreaks(StreakDirection direction, Dictionary<int, MonteCarloStreakData> winningStreaks, Dictionary<int, MonteCarloStreakData> losingStreaks) =>
            (direction == StreakDirection.Up) ? winningStreaks : losingStreaks;

        private static void UpdateStreakCount(Dictionary<int, MonteCarloStreakData> streaks, int length)
        {
            if (streaks.TryGetValue(length, out MonteCarloStreakData valueData))
                valueData.Count++;
            else
                streaks.Add(length, new MonteCarloStreakData() { Length = length, Count = 1 });
        }

        private static StreakDirection GetDirection(double current, double previous) =>
            (current > previous) ? StreakDirection.Up : StreakDirection.Down;

        private static List<MonteCarloStreakData> GetOrderedListOfStreaks(Dictionary<int, MonteCarloStreakData> streaks) => 
            streaks.OrderBy(o => o.Key).Select(o => o.Value).ToList();
    }
}
