namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Parameter with string value
    /// </summary>
    public class MOParamString : MOParam
    {
        public override MOParam Clone() => new MOParamString
        {
            Name = this.Name,
            Value = this.Value
        };

        protected override void SetValueString(string value)
        {
            Value = value;
        }
    }
}
