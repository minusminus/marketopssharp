using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.System;
using MarketOps.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketOps.SystemDefs.PriceCrossingSMA
{
    /// <summary>
    /// Price crossing sma up or down.
    /// </summary>
    public class PriceCrossingSMA : SystemDefinition
    {
        public PriceCrossingSMA(IStockDataProvider dataProvider, ISystemDataLoader dataLoader)
        {
            SignalsPriceCrossingSMA signals = new SignalsPriceCrossingSMA("", StockData.Types.StockDataRange.Daily, 0, dataLoader, dataProvider);

            _dataDefinitionProvider = signals;
            _signalGeneratorOnOpen = null;
            _signalGeneratorOnClose = signals;
            _commission = null;
            _slippage = null;
            _mmPositionCloseCalculator = null;

            SystemParams.Set(PriceCrossingSMAParams.StockName, "");
            SystemParams.Set(PriceCrossingSMAParams.SMAPeriod, 20);
        }
    }
}
