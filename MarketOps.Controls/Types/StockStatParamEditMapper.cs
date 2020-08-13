using MarketOps.StockData.Types;

namespace MarketOps.Controls.Types
{
    /// <summary>
    /// Maping StockStatParam to enable edit in DatGridView
    /// </summary>
    internal class StockStatParamEditMapper
    {
        private readonly StockStatParam _statParam;

        public StockStatParamEditMapper(StockStatParam statParam)
        {
            _statParam = statParam;
        }

        public string Name
        {
            get { return _statParam.Name; }
            set { _statParam.Name = value; }
        }

        public string Value
        {
            get { return _statParam.ValueString; }
            set { _statParam.ValueString = value; }
        }
    }
}
