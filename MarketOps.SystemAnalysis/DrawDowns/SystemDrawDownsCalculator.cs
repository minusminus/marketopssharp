using System.Collections.Generic;
using System.Linq;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemAnalysis.DrawDowns
{
    /// <summary>
    /// System drawdowns calculator.
    /// Drawdown is active until next value is higher then its top
    /// </summary>
    public class SystemDrawDownsCalculator
    {
        private List<SystemDrawDown> _result;
        private List<SystemValue> _equity;

        public List<SystemDrawDown> Calculate(List<SystemValue> equity)
        {
            if (equity.Count <= 1) return new List<SystemDrawDown>();

            _result = new List<SystemDrawDown>();
            _equity = equity;
            DrawDownsCalculator.Calculate(
                equity.Select(item => item.Value).ToArray(),
                OnDrawDown
                );
            return _result;
        }

        private void OnDrawDown(int startIndex, int lastIndex, int bottomIndex, float topValue, float bottomValue) =>
            _result.Add(
                new SystemDrawDown()
                {
                    TopValue = _equity[startIndex],
                    BottomValue = _equity[bottomIndex],
                    Ticks = lastIndex - startIndex,
                    LastTS = _equity[lastIndex].TS
                });
    }
}
