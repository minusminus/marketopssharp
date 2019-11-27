using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// range of supported stock data
    /// </summary>
    public enum StockDataRange
    {
        Undefined,
        Daily,
        Weekly,
        Monthly,
        Intraday,
        Tick
    }
}
