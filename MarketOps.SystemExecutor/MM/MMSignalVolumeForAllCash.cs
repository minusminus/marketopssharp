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

        public int Calculate(SystemState systemState, StockType stockType, float price)
        {
            int res = (int)Math.Floor(systemState.Cash / price);
            float commission = _commission.Calculate(stockType, res, price);

            if ((float)res * price + commission <= systemState.Cash) return res;
            return res - (int)Math.Ceiling(commission / price);
        }
    }
}
