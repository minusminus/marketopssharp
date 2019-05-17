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
        private static Dictionary<StockType, string> _valueFormat = new Dictionary<StockType, string>()
            {
                { StockType.Stock, "F4" },
                { StockType.Index, "F4" },
                { StockType.Future, "F4" },
                { StockType.InvestmentFund, "F2" },
                { StockType.NBPCurrency, "F2" },
                { StockType.Forex, "F4" },
            };

        /// <summary>
        /// returns formatted string of value
        /// </summary>
        /// <param name="stockData"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatValue(this StockDefinition stockData, double value)
        {
            return value.ToString(_valueFormat[stockData.Type]);
        }
    }
}
