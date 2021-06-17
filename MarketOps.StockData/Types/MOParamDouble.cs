using System;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Parameter with double value
    /// </summary>
    public class MOParamDouble : MOParam
    {
        public override MOParam Clone() => new MOParamDouble
        {
            Name = this.Name,
            Value = this.Value
        };

        protected override void SetValueString(string value)
        {
            Value = Convert.ToDouble(value);
        }
    }
}
