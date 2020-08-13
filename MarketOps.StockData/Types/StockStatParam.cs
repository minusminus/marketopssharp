namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Statistics parameter definition
    /// </summary>
    public abstract class StockStatParam
    {
        public string Name;
        public object Value;

        public string ValueString
        {
            get { return Value.ToString(); }
            set { SetValueString(value); }
        }

        protected abstract void SetValueString(string value);

        public abstract StockStatParam Clone();
    }
}
