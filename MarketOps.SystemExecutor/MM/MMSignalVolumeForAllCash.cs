using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;

namespace MarketOps.SystemExecutor.MM
{
    /// <summary>
    /// Returns volume for all available cash.
    /// </summary>
    public class MMSignalVolumeForAllCash : IMMSignalVolume
    {
        private readonly ICommission _commission;

        public MMSignalVolumeForAllCash(ICommission commission)
        {
            _commission = commission;
        }

        public float Calculate(SystemState systemState, StockType stockType, float price)
        {
            float res = (float)Math.Floor(systemState.Cash / price);
            if (res <= 0) return 0;
            float commission = _commission.Calculate(stockType, res, price);

            if (res * price + commission <= systemState.Cash) return res;
            return res - (float)Math.Ceiling(commission / price);
        }
    }
}
