using MarketOps.System.Interfaces;

namespace MarketOps.System.MM
{
    /// <summary>
    /// Closes on next open after specifed number of ticks.
    /// </summary>
    public class MMCloseCalculatorTicksPassedOnOpen : IMMPositionCloseCalculator
    {
        private readonly int _ticks;

        public MMCloseCalculatorTicksPassedOnOpen(int ticks)
        {
            _ticks = ticks;
        }

        public void CalculateCloseMode(ref Position position)
        {
            position.CloseMode = (position.TicksActive < _ticks) ? PositionCloseMode.DontClose : PositionCloseMode.OnOpen;
        }
    }
}
