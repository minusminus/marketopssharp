using System;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Statistics parameter with float value
    /// </summary>
    public class StockStatParamFloat : StockStatParam
    {
        public override StockStatParam Clone()
        {
            StockStatParamFloat res = new StockStatParamFloat();
            res.Name = this.Name;
            res.Value = this.Value;
            return res;
        }

        protected override void SetValueString(string value)
        {
            Value = Convert.ToSingle(value);
        }
    }
}
