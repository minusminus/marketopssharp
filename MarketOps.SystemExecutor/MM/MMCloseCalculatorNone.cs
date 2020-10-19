using MarketOps.SystemExecutor.Interfaces;
using System;

namespace MarketOps.SystemExecutor.MM
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
