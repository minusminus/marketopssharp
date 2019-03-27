using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketOps.StockData.Types;

namespace MarketOps.StockData
{
    /// <summary>
    /// merger of data prices objects
    /// </summary>
    public class StockPricesDataMerger
    {
        public StockPricesData Merge(StockPricesData Data1, StockPricesData Data2)
        {
            return new StockPricesData(0);
        }
    }
}
