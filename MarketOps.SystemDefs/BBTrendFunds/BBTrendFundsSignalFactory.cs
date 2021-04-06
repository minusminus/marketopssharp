using MarketOps.StockData.Types;
using MarketOps.SystemData.Types;
using System.Linq;

namespace MarketOps.SystemDefs.BBTrendFunds
{
    /// <summary>
    /// Signal creator for bb trend funds.
    /// </summary>
    internal static class BBTrendFundsSignalFactory
    {
        public static Signal CreateSignal(float[] newBalance, StockDataRange dataRange, BBTrendFundsData fundsData) =>
            new Signal()
            {
                DataRange = dataRange,
                IntradayInterval = 0,
                Type = SignalType.EnterOnOpen,
                Direction = PositionDir.Long,
                Rebalance = true,
                NewBalance = fundsData.Stocks.Select((def, i) => (def, newBalance[i])).ToList()
            };
    }
}
