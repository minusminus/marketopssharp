using System.Collections.Generic;
using System.Windows.Forms;
using MarketOps.SystemExecutor;

namespace MarketOps.Controls.SystemEquity
{
    public partial class SystemEquityChart : UserControl
    {
        public SystemEquityChart()
        {
            InitializeComponent();
        }

        public void LoadData(List<SystemValue> equity)
        {
            FillBindingSource(equity);
            BindChartData();
        }

        private void FillBindingSource(List<SystemValue> equity)
        {
            srcEquity.Clear();
            equity.ForEach(v => srcEquity.Add(new SystemValueMapper(v)));
        }

        private void BindChartData()
        {
            chartEquity.DataBind();
        }
    }
}
