using MarketOps.StockData.Types;

namespace MarketOps.Controls.StockData
{
    /// <summary>
    /// Mapping MOParam to enable edit in DatGridView
    /// </summary>
    internal class MOParamEditMapper
    {
        private readonly MOParam _statParam;

        public MOParamEditMapper(MOParam statParam)
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
