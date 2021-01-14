using MarketOps.StockData.Types;

namespace MarketOps.SystemData.Interfaces
{
    /// <summary>
    /// Interface for system slippage calculation.
    /// Calculates value of commission.
    /// </summary>
    public interface ICommission
    {
        float Calculate(StockType stockType, int volume, float price);
    }
}
