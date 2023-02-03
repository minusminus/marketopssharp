using MarketOps.StockData.Types;

namespace MarketOps.SystemDefs.NTopFunds
{
    /// <summary>
    /// Calculation data for N top funds.
    /// </summary>
    internal class NTopFundsData
    {
        public readonly StockDefinition[] Stocks;
        public readonly bool[] Active;
        public readonly double[] Prices;
        public readonly double[] Risk;
        //public readonly double[] AvgChangeSigma;
        public readonly double[] Profit;

        public NTopFundsData(int length)
        {
            Stocks = new StockDefinition[length];
            Active = new bool[length];
            Prices = new double[length];
            Risk = new double[length];
            //AvgChangeSigma = new double[length];
            Profit = new double[length];
        }
    }
}
