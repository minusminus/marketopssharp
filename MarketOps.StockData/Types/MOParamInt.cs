using System;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Parameter with integer value
    /// </summary>
    public class MOParamInt : MOParam
    {
        public override MOParam Clone() => new MOParamInt
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
