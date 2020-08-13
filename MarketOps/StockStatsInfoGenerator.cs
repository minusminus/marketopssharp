using System.Collections.Generic;
using MarketOps.StockData.Types;
using MarketOps.StockData;
using MarketOps.Controls.Types;

namespace MarketOps
{
    /// <summary>
    /// generates label information of all stats for selected stock data point
    /// </summary>
    internal class StockStatsInfoGenerator : IStockStatsInfoGenerator
    {
        public string GetStatsSelectedInfo(StockDisplayData data, int selectedIndex)
        {
            List<string> values = new List<string>();
            foreach (StockStat stat in data.Stats)
                values.Add($"{GetStatHeader(stat)}=[{GetStatValue(stat, selectedIndex, data.Prices.Length)}]");
            return string.Join(", ", values);
        }

        public string GetStatHeader(StockStat stat) => $"{stat.Name}({GetStatHeaderParams(stat.StatParams)})";

        private string GetStatHeaderParams(StockStatParams statParams)
        {
            List<string> values = new List<string>();
            foreach (StockStatParam param in statParams)
                values.Add(param.Value.ToString());
            return string.Join(",", values);
        }

        private string GetStatValue(StockStat stat, int selectedIndex, int dataPricesLength)
        {
            int emptyStart = dataPricesLength - stat.Data(0).Length;
            if (selectedIndex < emptyStart) return "";

            List<string> values = new List<string>();
            for (int i = 0; i < stat.DataCount; i++)
                values.Add(DataFormatting.FormatStatValue(stat.Data(i)[selectedIndex - emptyStart]));
            return string.Join(", ", values);
        }
    }
}
