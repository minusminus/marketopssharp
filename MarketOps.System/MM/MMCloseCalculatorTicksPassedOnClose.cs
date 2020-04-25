using MarketOps.System.Interfaces;

namespace MarketOps.System.MM
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

        public void CalculateCloseMode(ref Position position)
        {
            position.CloseMode = (position.TicksActive < _ticks) ? PositionCloseMode.DontClose : PositionCloseMode.OnClose;
        }
    }
}
