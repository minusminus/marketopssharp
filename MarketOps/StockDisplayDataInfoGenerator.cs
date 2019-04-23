using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketOps.StockData.Types;

namespace MarketOps
{
    /// <summary>
    /// generates label inforamtion of selected point in stock data
    /// </summary>
    internal class StockDisplayDataInfoGenerator
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

        public string GetInfo(StockDisplayData data, int selectedIndex)
        {
            return $"{data.stock.Name} - {FormatTSAccordingToDataRange(data.prices.TS[selectedIndex], data.prices.Range)} OHLC({data.prices.O[selectedIndex]}, {data.prices.H[selectedIndex]}, {data.prices.L[selectedIndex]}, {data.prices.C[selectedIndex]}) V={data.prices.V[selectedIndex]}";
        }
    }
}
