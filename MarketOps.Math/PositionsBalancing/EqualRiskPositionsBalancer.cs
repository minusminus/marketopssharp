using System.Linq;
using System;

namespace MarketOps.Maths.PositionsBalancing
{
    /// <summary>
    /// Calculates positions balance based on provided expected risk.
    /// Risk should be provided in values, not in percents.
    /// Returns balanced sizes of positions.
    /// All positions will have equal risk after balance.
    /// </summary>
    public static class EqualRiskPositionsBalancer
    {
        public static float[] Calculate(float[] expectedRisks, float[] prices, float equityValue)
        {
            CheckEqualLengths(expectedRisks, prices);

            float[] weightedRisks = CalculateWeightedRisks(expectedRisks, equityValue);
            float[] balance = CalculateInitialBalance(weightedRisks);
            return AdjustBalanceToEquityValue(balance, prices, equityValue);
        }

        private static void CheckEqualLengths(float[] expectedRisks, float[] prices)
        {
            if (expectedRisks.Length == 0)
                throw new ArgumentException("Zero length tables");
            if (expectedRisks.Length != prices.Length)
                throw new ArgumentException("Tables lengths differ");
        }

        private static float[] CalculateWeightedRisks(float[] expectedRisks, float equityValue)
        {
            float[] result = new float[expectedRisks.Length];

            for (int i = 0; i < expectedRisks.Length; i++)
                result[i] = expectedRisks[i] / equityValue;

            return result;
        }

        private static float[] CalculateInitialBalance(float[] weightedRisks)
        {
            float[] result = new float[weightedRisks.Length];

            float maxWeightedRisk = weightedRisks.Max();
            for (int i = 0; i < weightedRisks.Length; i++)
                result[i] = maxWeightedRisk / weightedRisks[i];

            return result;
        }

        private static float[] AdjustBalanceToEquityValue(float[] balance, float[] prices, float equityValue)
        {
            float sum = 0;
            for (int i = 0; i < balance.Length; i++)
                sum += balance[i] * prices[i];
            float balanceMultiplier = equityValue / sum;

            for (int i = 0; i < balance.Length; i++)
                balance[i] *= balanceMultiplier;

            return balance;
        }
    }
}
