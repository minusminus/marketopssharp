using MarketOps.System.Interfaces;
using System;

namespace MarketOps.System.MM
{
    /// <summary>
    /// Sets everlasting position.
    /// </summary>
    public class MMCloseCalculatorNone : IMMPositionCloseCalculator
    {
        public void CalculateCloseMode(Position position, DateTime ts)
        {
            position.CloseMode = PositionCloseMode.DontClose;
        }
    }
}
