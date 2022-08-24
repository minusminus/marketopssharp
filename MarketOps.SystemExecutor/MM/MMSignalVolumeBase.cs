using MarketOps.Maths;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using System;

namespace MarketOps.SystemExecutor.MM
{
    /// <summary>
    /// Base class for signal volume calculations.
    /// </summary>
    public abstract class MMSignalVolumeBase
    {
        private const float Accuracy = 0.0001f;

        protected readonly ICommission _commission;

        protected MMSignalVolumeBase(ICommission commission)
        {
            _commission = commission;
        }

        protected static float CalculateVolume(float cash, float price) => 
            cash.FloorDivideWithAccuracy(price, Accuracy);

        protected float CalculateCommission(StockType stockType, float price, float volume) =>
            (_commission != null) ? _commission.Calculate(stockType, volume, price) : 0;

        protected static float CorrectVolumeForAvailableCash(float volume, float price, float commission, float availableCash) =>
            (volume * price + commission <= availableCash)
                ? volume
                : volume - commission.CeilingDivideWithAccuracy(price, Accuracy);
    }
}
