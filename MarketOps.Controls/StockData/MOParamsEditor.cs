using System.Windows.Forms;
using MarketOps.StockData.Types;

namespace MarketOps.Controls.StockData
{
    public partial class MOParamsEditor : UserControl
    {
        public MOParamsEditor()
        {
            InitializeComponent();
        }

        public void LoadParams(MOParams moParams)
        {
            srcParams.Clear();
            foreach (var param in moParams)
                srcParams.Add(new MOParamEditMapper(param.Clone()));
        }

        public void SaveParams(MOParams moParams)
        {
            var list = srcParams.GetEnumerator();
            while (list.MoveNext())
            {
                MOParamEditMapper paramMapper = (MOParamEditMapper)list.Current;
                moParams.Get(paramMapper.Name).ValueString = paramMapper.Value;
            }
        }
    }
}
