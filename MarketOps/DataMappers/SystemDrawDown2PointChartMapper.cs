using MarketOps.SystemData.Types;
using MarketOps.Controls.PointChart;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.DataMappers
{
    /// <summary>
    /// Maps SystemDrawDown to PointChartData
    /// </summary>
    internal static class SystemDrawDown2PointChartMapper
    {
        public static List<PointChartData> Map(List<SystemDrawDown> input) => 
            input.Select(CreateNewDataObject).ToList();

        private static PointChartData CreateNewDataObject(SystemDrawDown dd) => 
            new SystemDrawDownPointChartData(dd);
    }
}
