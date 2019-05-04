using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketOps.StockData.Types;

namespace MarketOps.Controls.Types
{
    /// <summary>
    /// interface for generating info text on stock data
    /// </summary>
    public interface IStockInfoGenerator
    {
        string GetStockInfo(StockDisplayData data);
        string GetStockSelectedInfo(StockDisplayData data, int selectedIndex);
    }
}
