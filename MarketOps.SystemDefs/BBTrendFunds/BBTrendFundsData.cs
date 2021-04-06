using MarketOps.Stats.Stats;
using MarketOps.StockData.Types;
using MarketOps.SystemDefs.BBTrendRecognizer;

namespace MarketOps.SystemDefs.BBTrendFunds
{
    /// <summary>
    /// Data for bb funds.
    /// </summary>
    internal class BBTrendFundsData
    {
        public readonly StockDefinition[] Stocks;
        public readonly StatBB[] StatsBB;
        public readonly BBTrendType[] CurrentTrends;
        public readonly BBTrendExpectation[] CurrentExpectations;
        public readonly bool[] ExpectationChanged;

        public BBTrendFundsData(int length)
        {
            Stocks = new StockDefinition[length];
            StatsBB = new StatBB[length];
            CurrentTrends = new BBTrendType[length];
            CurrentExpectations = new BBTrendExpectation[length];
            ExpectationChanged = new bool[length];
        }
    }
}
