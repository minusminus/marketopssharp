using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketOps.Extensions
{
    /// <summary>
    /// extensions for StockDisplayData
    /// </summary>
    internal static class StockDisplayDataExtensions
    {
        public static string GetInfo(this StockDisplayData data, int selectedIndex)
        {
            return (new StockDisplayDataInfoGenerator()).GetInfo(data, selectedIndex);
        }
    }
}
