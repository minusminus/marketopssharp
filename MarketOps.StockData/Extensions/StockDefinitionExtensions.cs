using MarketOps.StockData.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketOps.StockData.Extensions
{
    /// <summary>
    /// StockDefinition extension methods
    /// </summary>
    public static class StockDefinitionExtensions
    {
        /// <summary>
        /// returns formatted string of price value
        /// </summary>
        /// <param name="stockData"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatPrice(this StockDefinition stockData, double value)
        {
            return DataFormatting.FormatPrice(stockData.Type, value);
        }
    }
}
