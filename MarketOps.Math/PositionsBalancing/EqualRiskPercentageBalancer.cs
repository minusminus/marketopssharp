using System.Linq;

namespace MarketOps.Maths.PositionsBalancing
{
    /// <summary>
    /// Calculates balance based on risk in percents.
    /// Returns balanced percentages of equal risk positions.
    /// All positions will have equal risk after balance.
    /// </summary>
    public static class EqualRiskPercentageBalancer
    {
        public static float[] Calculate(in float[] expectedRisks)
        {
            if (expectedRisks.Length == 0) return new float[0];

            float[] balance = CalculateAmounts(expectedRisks);
            CalculateBalance(balance);
            return balance;
        }

        private static float[] CalculateAmounts(in float[] risks)
        {
            float[] amounts = new float[risks.Length];

            float maxValue = risks.Max();
            for (int i = 0; i < risks.Length; i++)
                amounts[i] = maxValue / risks[i];

            return amounts;
        }

        private static void CalculateBalance(float[] amounts)
        {
            float totalValue = amounts.Sum();
            for (int i = 0; i < amounts.Length; i++)
                amounts[i] /= totalValue;
        }
    }
}
