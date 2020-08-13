namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Statistics parameter with string value
    /// </summary>
    public class StockStatParamString : StockStatParam
    {
        public override StockStatParam Clone() => new StockStatParamString
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
