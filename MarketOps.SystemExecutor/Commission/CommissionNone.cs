using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;

namespace MarketOps.SystemExecutor.Commission
{
    /// <summary>
    /// No commission.
    /// </summary>
    public class CommissionNone : ICommission
    {
        public float Calculate(StockType stockType, float volumme, float price)
        {
            return 0;
        }
    }
}
