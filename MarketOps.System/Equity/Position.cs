using System;
using MarketOps.StockData.Types;

namespace MarketOps.System.Equity
{
    /// <summary>
    /// Position data.
    /// </summary>
    internal struct Position
    {
        public StockDefinition Stock;
        public DateTime TSOpen;
        public DateTime TSClose;
        public float Open;
        public float Close;
    }
}
