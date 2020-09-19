namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Parameter definition
    /// </summary>
    public abstract class MOParam
    {
        public string Name;
        public object Value;

        public string ValueString
        {
            get { return Value.ToString(); }
            set { SetValueString(value); }
        }

        protected abstract void SetValueString(string value);

        public abstract MOParam Clone();
    }
}
