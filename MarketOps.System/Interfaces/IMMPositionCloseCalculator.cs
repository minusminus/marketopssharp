using System;

namespace MarketOps.System.Interfaces
{
    /// <summary>
    /// Interface for postion close mode and level calculator.
    /// Implementation should update CloseMode and CloseModePrice.
    /// </summary>
    public interface IMMPositionCloseCalculator
    {
        void CalculateCloseMode(ref Position position, DateTime ts);
    }
}
