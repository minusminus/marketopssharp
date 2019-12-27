using System;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Statistics parameter with integer value
    /// </summary>
    public class StockStatParamInt : StockStatParam
    {
        public override StockStatParam Clone()
        {
            StockStatParamInt res = new StockStatParamInt();
            res.Name = this.Name;
            res.Value = this.Value;
            return res;
        }

        protected override void SetValueString(string value)
        {
            Value = Convert.ToInt32(value);
        }
    }
}
