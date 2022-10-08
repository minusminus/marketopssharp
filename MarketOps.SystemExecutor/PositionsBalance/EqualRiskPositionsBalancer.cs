using System.Linq;
using System;

namespace MarketOps.SystemExecutor.PositionsBalance
{
    /// <summary>
    /// Calculates positions balance based on provided expected risk.
    /// All positions will have equal risk after balance.
    /// Each position size will be no more than provided weight.
    /// Weights should sum to <= 1.
    /// </summary>
    public static class EqualRiskPositionsBalancer
    {
        public static float[] CalculateBalanceEqualWeights(float[] expectedRisks, float[] prices, float equityValue) =>
            CalculateBalance(expectedRisks, Enumerable.Repeat(1f / expectedRisks.Length, expectedRisks.Length).ToArray(), prices, equityValue);

        public static float[] CalculateBalance(float[] expectedRisks, float[] positionsWeights, float[] prices, float equityValue)
        {
            CheckEqualLengths(expectedRisks, positionsWeights, prices);

            float[] weightedRisks = CalculateWeightedRisks(expectedRisks, positionsWeights, equityValue);
            float[] balance = CalculateInitialBalance(weightedRisks);
            return AdjustBalanceToEquityValue(balance, prices, CalculateWeightedEquityValue(positionsWeights, equityValue));
        }

        private static void CheckEqualLengths(float[] expectedRisks, float[] positionsWeights, float[] prices)
        {
            if (expectedRisks.Length == 0)
                throw new ArgumentException("Zero length tables");
            if (expectedRisks.Length != positionsWeights.Length
                || expectedRisks.Length != prices.Length)
                throw new ArgumentException("Tables lengths differ");
        }

        private static float[] CalculateWeightedRisks(float[] expectedRisks, float[] positionsWeights, float equityValue)
        {
            float[] result = new float[expectedRisks.Length];

            for (int i = 0; i < expectedRisks.Length; i++)
                result[i] = positionsWeights[i] * expectedRisks[i] / equityValue;

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

        private static float CalculateWeightedEquityValue(float[] positionsWeights, float equityValue) =>
            positionsWeights.Sum() * equityValue;

        private static float[] AdjustBalanceToEquityValue(float[] balance, float[] prices, float weightedEquityValue)
        {
            float sum = 0;
            for (int i = 0; i < balance.Length; i++)
                sum += balance[i] * prices[i];
            float balanceMultiplier = weightedEquityValue / sum;

            for (int i = 0; i < balance.Length; i++)
                balance[i] *= balanceMultiplier;

            return balance;
        }
    }
}
