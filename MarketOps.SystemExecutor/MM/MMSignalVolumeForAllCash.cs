using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemExecutor.MM
{
    /// <summary>
    /// Returns volume for all available cash.
    /// </summary>
    public class MMSignalVolumeForAllCash : MMSignalVolumeBase, IMMSignalVolume
    {
        public MMSignalVolumeForAllCash(ICommission commission)
            : base(commission)
        { }

        public float Calculate(SystemState systemState, StockType stockType, float price, float initialRisk)
        {
            float volume = CalculateVolume(systemState.Cash, price);
            if (volume <= 0) return 0;

            return CorrectVolumeForAvailableCash(volume, price, CalculateCommission(stockType, price, volume), systemState.Cash);
        }
    }
}
