using MarketOps.Controls.Types;
using MarketOps.StockData.Types;
using MarketOps.StockData;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps
{
    /// <summary>
    /// generates label information of all stats for selected stock data point
    /// </summary>
    internal class StockStatsInfoGenerator : IStockStatsInfoGenerator
    {
        public string GetStatsSelectedInfo(StockDisplayData data, int selectedIndex) => 
            string.Join(", ",
                data.Stats
                    .Select(stat => $"{GetStatHeader(stat)}=[{GetStatValue(stat, selectedIndex, data.Prices.Length)}]")
                );

        public string GetStatHeader(StockStat stat) => $"{stat.Name}({GetStatHeaderParams(stat.StatParams)})";

        private string GetStatHeaderParams(MOParams statParams)
        {
            IEnumerable<string> ParamsToStrings()
            {
                foreach (MOParam param in statParams)
                    yield return param.Value.ToString();
            }

            return string.Join(",", ParamsToStrings());
        }

        private string GetStatValue(StockStat stat, int selectedIndex, int dataPricesLength)
        {
            int emptyStart = dataPricesLength - stat.Data(0).Length;
            if (selectedIndex < emptyStart) return "";

            return string.Join(", ",
                Enumerable.Range(0, stat.DataCount)
                    .Select(i => DataFormatting.FormatStatValue(stat.Data(i)[selectedIndex - emptyStart]))
                );
        }
    }
}
