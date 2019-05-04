using MarketOps.StockData.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketOps
{
    /// <summary>
    /// gathers stock data to be displayed
    /// </summary>
    public class StockDisplayData
    {
        public StockDefinition stock;
        public StockPricesData prices;
    }
}
