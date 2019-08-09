using System;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Statistics parameter with string value
    /// </summary>
    public class StockStatParamString : StockStatParam
    {
        protected override void SetValueString(string value)
        {
            Value = value;
        }
    }
}
