namespace MarketOps.StockData.Types
{
    /// <summary>
    /// stock definition
    /// </summary>
    public class StockDefinition
    {
        public int ID;
        public StockType Type;
        public bool Enabled;
        public string FullName;
        public string Name;
        public string FileNameDaily;
    }
}
