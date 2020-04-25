using MarketOps.System.Interfaces;

namespace MarketOps.System.MM
{
    /// <summary>
    /// Sets everlasting position.
    /// </summary>
    public class MMCloseCalculatorNone : IMMPositionCloseCalculator
    {
        public void CalculateCloseMode(ref Position position)
        {
            position.CloseMode = PositionCloseMode.DontClose;
        }
    }
}
