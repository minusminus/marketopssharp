using System;
using MarketOps.StockData.Types;
using MarketOps.SystemExecutor.Extensions;
using MarketOps.SystemData.Interfaces;

namespace MarketOps.SystemExecutor.GPW
{
    /// <summary>
    /// Commission according to Bossa for GPW.
    /// </summary>
    public class CommissionGPWBossa : ICommission
    {
        private float CalculateStock(int volume, float price)
        {
            return Math.Max(volume * price * 0.0038f, 5f).TruncateTo2ndPlace();
        }

        private float CalculateIndexFuture(int volume)
        {
            return (volume * 9.9f).TruncateTo2ndPlace();
        }

        public float Calculate(StockType stockType, int volume, float price)
        {
            switch (stockType)
            {
                case StockType.Stock:
                    return CalculateStock(volume, price);
                case StockType.IndexFuture:
                    return CalculateIndexFuture(volume);
            }
            return 0;
        }
    }
}
