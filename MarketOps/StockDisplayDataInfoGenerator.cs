using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketOps.StockData.Types;
using MarketOps.Controls.Types;

namespace MarketOps
{
    /// <summary>
    /// generates label information of selected point in stock data
    /// </summary>
    internal class StockDisplayDataInfoGenerator : IStockInfoGenerator
    {
        private string FormatTSAccordingToDataRange(DateTime ts, StockDataRange range)
        {
            Dictionary<StockDataRange, string> formatStrings = new Dictionary<StockDataRange, string>()
            {
                { StockDataRange.Day, "yyyy-MM-dd" },
                { StockDataRange.Week, "yyyy-MM-dd" },
                { StockDataRange.Month, "yyyy-MM-dd" },
                { StockDataRange.Intraday, "yyyy-MM-dd hh:mm" },
                { StockDataRange.Tick, "yyyy-MM-dd hh:mm:ss" },
            };
            return ts.ToString(formatStrings[range]);
        }

        public string GetStockInfo(StockDisplayData data)
        {
            return $"{data.stock.Name} [{FormatTSAccordingToDataRange(data.prices.TS[0], data.prices.Range)} - {FormatTSAccordingToDataRange(data.prices.TS[data.prices.Length - 1], data.prices.Range)}]";
        }

        public string GetStockSelectedInfo(StockDisplayData data, int selectedIndex)
        {
            return $"{FormatTSAccordingToDataRange(data.prices.TS[selectedIndex], data.prices.Range)} OHLC({data.prices.O[selectedIndex]}, {data.prices.H[selectedIndex]}, {data.prices.L[selectedIndex]}, {data.prices.C[selectedIndex]}) V={data.prices.V[selectedIndex]}";
        }
    }
}
