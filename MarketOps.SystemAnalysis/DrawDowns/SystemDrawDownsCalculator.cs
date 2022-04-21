using System.Collections.Generic;
using System.Linq;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemAnalysis.DrawDowns
{
    /// <summary>
    /// System drawdowns calculator.
    /// Drawdown is active until next value is higher then its top
    /// </summary>
    public static class SystemDrawDownsCalculator
    {
        public static List<SystemDrawDown> Calculate(List<SystemValue> equity)
        {
            if (equity.Count <= 1) return new List<SystemDrawDown>();

            SystemValue lastValue = equity[0];
            SystemDrawDown lastDD = null;
            return equity
                .Skip(1)
                .Select(currValue => ProcessSystemValue(currValue, ref lastValue, ref lastDD))
                .Where(o => o != null)
                .ToList();
        }

        private static SystemDrawDown ProcessSystemValue(SystemValue currValue, ref SystemValue lastValue, ref SystemDrawDown lastDD)
        {
            SystemDrawDown currDD = UpdateDD(CreateNewDDIfRequired(CloseDDIfRequired(lastDD, currValue), currValue, lastValue), currValue);
            lastValue = currValue;
            if ((currDD == null) || (currDD == lastDD)) return null;
            lastDD = currDD;
            return currDD;
        }

        private static SystemDrawDown CloseDDIfRequired(SystemDrawDown currDD, SystemValue currValue) =>
            (currDD?.TopValue.Value < currValue.Value) ? null : currDD;

        private static SystemDrawDown CreateNewDDIfRequired(SystemDrawDown currDD, SystemValue currValue, SystemValue lastValue) => 
            (currDD == null) && (currValue.Value < lastValue.Value) ? CreateNewDD(lastValue, currValue) : currDD;

        private static SystemDrawDown UpdateDD(SystemDrawDown currDD, SystemValue currValue)
        {
            if (currDD == null) return null;
            if (currDD.BottomValue.Value > currValue.Value)
                currDD.BottomValue = currValue;
            currDD.LastTS = currValue.TS;
            currDD.Ticks++;
            return currDD;
        }

        private static SystemDrawDown CreateNewDD(SystemValue lastValue, SystemValue currValue) =>
            new SystemDrawDown()
            {
                TopValue = lastValue,
                BottomValue = currValue,
                Ticks = 0
            };
    }
}
