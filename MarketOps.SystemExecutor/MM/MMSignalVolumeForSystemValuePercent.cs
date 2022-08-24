using MarketOps.StockData.Types;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemExecutor.MM
{
    /// <summary>
    /// Returns volume for specified percent of system value.
    /// </summary>
    public class MMSignalVolumeForSystemValuePercent : MMSignalVolumeBase, IMMSignalVolume
    {
        private readonly float _percentOfValue;
        private readonly ISystemDataLoader _dataLoader;

        public MMSignalVolumeForSystemValuePercent(float percentOfValue, ICommission commission, ISystemDataLoader dataLoader)
            : base(commission)
        {
            _percentOfValue = percentOfValue;
            _dataLoader = dataLoader;
        }

        public float Calculate(SystemState systemState, StockType stockType, float price, float initialRisk)
        {
            float percentOfSystemValue = CalculatePercentOfSystemValue(systemState);
            if (NotEnoughCash(percentOfSystemValue, systemState.Cash)) return 0;

            float volume = CalculateVolume(percentOfSystemValue, price);
            if (volume <= 0) return 0;

            return CorrectVolumeForAvailableCash(volume, price, CalculateCommission(stockType, price, volume), systemState.Cash);
        }

        private float CalculatePercentOfSystemValue(SystemState systemState) =>
            _percentOfValue * SystemValueCalculator.Calc(systemState, systemState.LastProcessedTS, _dataLoader);

        private bool NotEnoughCash(float value, float availableCash) =>
            value > availableCash;
    }
}
