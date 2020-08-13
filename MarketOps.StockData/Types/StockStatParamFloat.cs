using System;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Statistics parameter with float value
    /// </summary>
    public class StockStatParamFloat : StockStatParam
    {
        public override StockStatParam Clone() => new StockStatParamFloat
        {
            Name = this.Name,
            Value = this.Value
        };

        protected override void SetValueString(string value)
        {
            Value = Convert.ToSingle(value);
        }
    }
}
