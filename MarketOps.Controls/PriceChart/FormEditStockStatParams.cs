using System.Windows.Forms;
using MarketOps.Controls.Types;
using MarketOps.StockData.Types;

namespace MarketOps.Controls.PriceChart
{
    public partial class FormEditStockStatParams : Form
    {
        public FormEditStockStatParams()
        {
            InitializeComponent();
        }

        public bool Execute(StockStatParams statParams, string statName)
        {
            SetCaption(statName);
            LoadStatParams(statParams);
            return (ShowDialog() == DialogResult.OK);
        }

        private void SetCaption(string statName)
        {
            Text = $"{statName} parameters";
        }

        private void LoadStatParams(StockStatParams statParams)
        {
            foreach (var param in statParams)
                srcParams.Add(new StockStatParamEditMapper(param));
        }
    }
}
