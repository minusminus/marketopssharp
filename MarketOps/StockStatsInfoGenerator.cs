using System;
using System.Collections.Generic;
using MarketOps.StockData.Types;
using MarketOps.StockData;
using MarketOps.StockData.Extensions;

namespace MarketOps
{
    /// <summary>
    /// generates label information of all stats for selected stock data point
    /// </summary>
    internal class StockStatsInfoGenerator
    {
        public string GetStatsSelectedInfo(StockDisplayData data, int selectedIndex)
        {
            List<string> values = new List<string>();
            foreach (StockStat stat in data.Stats)
                values.Add($"{GetStatHeader(stat)}=[{GetStatValue(stat, selectedIndex)}]");
            return string.Join(", ", values);
        }

        private string GetStatHeader(StockStat stat)
        {
            return $"{stat.Name}({GetStatHeaderParams(stat.StatParams)})";
        }

        private string GetStatHeaderParams(StockStatParams statParams)
        {
            List<string> values = new List<string>();
            foreach (StockStatParam param in statParams)
                values.Add(param.As<string>());
            return string.Join(",", values);
        }

        private string GetStatValue(StockStat stat, int selectedIndex)
        {
            List<string> values = new List<string>();
            for (int i = 0; i < stat.DataCount; i++)
                values.Add(DataFormatting.FormatStatValue(stat.Data(i)[selectedIndex]));
            return string.Join(",", values);
        }
    }
}
