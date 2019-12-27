using System;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Statistics parameter with string value
    /// </summary>
    public class StockStatParamString : StockStatParam
    {
        public override StockStatParam Clone()
        {
            StockStatParamString res = new StockStatParamString();
            res.Name = this.Name;
            res.Value = this.Value;
            return res;
        }

        protected override void SetValueString(string value)
        {
            Value = value;
        }
    }
}
