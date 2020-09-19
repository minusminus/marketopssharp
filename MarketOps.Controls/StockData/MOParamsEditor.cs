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

        public void LoadStatParams(MOParams statParams)
        {
            foreach (var param in statParams)
                srcParams.Add(new MOParamEditMapper(param.Clone()));
        }

        public void SaveStatParams(MOParams statParams)
        {
            var list = srcParams.GetEnumerator();
            while (list.MoveNext())
            {
                MOParamEditMapper paramMapper = (MOParamEditMapper)list.Current;
                statParams.Get(paramMapper.Name).ValueString = paramMapper.Value;
            }
        }
    }
}
