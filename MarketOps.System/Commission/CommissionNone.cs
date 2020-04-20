using MarketOps.StockData.Types;
using MarketOps.System.Interfaces;

namespace MarketOps.System.Commission
{
    /// <summary>
    /// No commission.
    /// </summary>
    public class CommissionNone : ICommission
    {
        public float Calculate(StockType stockType, int volumme, float price)
        {
            return 0;
        }
    }
}
