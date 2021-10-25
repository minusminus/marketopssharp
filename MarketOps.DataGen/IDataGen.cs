using MarketOps.StockData.Types;

namespace MarketOps.DataGen
{
    /// <summary>
    /// Interface for data generation mechanism.
    /// </summary>
    public interface IDataGen
    {
        void GenerateWeekly(StockDefinition stockDefinition);
        void GenerateMonthly(StockDefinition stockDefinition);
    }
}
