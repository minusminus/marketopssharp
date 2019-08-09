using System;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Statistics parameter with float value
    /// </summary>
    public class StockStatParamFloat : StockStatParam
    {
        protected override void SetValueString(string value)
        {
            Value = Convert.ToSingle(value);
        }
    }
}
