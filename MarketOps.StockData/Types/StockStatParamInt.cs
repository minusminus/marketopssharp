using System;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Statistics parameter with integer value
    /// </summary>
    public class StockStatParamInt : StockStatParam
    {
        public override StockStatParam Clone() => new StockStatParamInt
        {
            Name = this.Name,
            Value = this.Value
        };

        protected override void SetValueString(string value)
        {
            Value = Convert.ToInt32(value);
        }
    }
}
