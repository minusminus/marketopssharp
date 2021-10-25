using System;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Parameter with float value
    /// </summary>
    public class MOParamFloat : MOParam
    {
        public override MOParam Clone() => new MOParamFloat
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
