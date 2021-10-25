using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;

namespace MarketOps.SystemExecutor.MM
{
    /// <summary>
    /// Closes on next close after specifed number of ticks.
    /// </summary>
    public class MMCloseCalculatorTicksPassedOnClose : IMMPositionCloseCalculator
    {
        private readonly int _ticks;

        public MMCloseCalculatorTicksPassedOnClose(int ticks)
        {
            _ticks = ticks;
        }

        public void CalculateCloseMode(Position position, DateTime ts)
        {
            position.CloseMode = (position.TicksActive < _ticks) ? PositionCloseMode.DontClose : PositionCloseMode.OnClose;
        }
    }
}
