using System;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Statistics parameter with integer value
    /// </summary>
    public class StockStatParamInt : StockStatParam
    {
        protected override void SetValueString(string value)
        {
            Value = Convert.ToInt32(value);
        }
    }
}
