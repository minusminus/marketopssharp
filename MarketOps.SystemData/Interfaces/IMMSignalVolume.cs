using MarketOps.StockData.Types;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemData.Interfaces
{
    /// <summary>
    /// Interface for Money Management new position volume calculation.
    /// </summary>
    public interface IMMSignalVolume
    {
        float Calculate(SystemState systemState, StockType stockType, float price);
    }
}
