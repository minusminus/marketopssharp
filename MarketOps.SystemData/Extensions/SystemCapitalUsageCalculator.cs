using MarketOps.SystemData.Types;
using System.Linq;

namespace MarketOps.SystemData.Extensions
{
    /// <summary>
    /// Calculates percent of system capital used in current positions.
    /// </summary>
    public static class SystemCapitalUsageCalculator
    {
        public static float Calc(SystemState system)
        {
            float capitalUsed = SumActivePositonsOpeningValues(system);
            float totalValue = capitalUsed + system.Cash;
            return (totalValue != 0) ? capitalUsed / totalValue : 0;
        }

        public static float SumActivePositonsOpeningValues(SystemState system) =>
            system.PositionsActive
                .Sum(p => p.OpenValue() + p.OpenCommission);
    }
}
