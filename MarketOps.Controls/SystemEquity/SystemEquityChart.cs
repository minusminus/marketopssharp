using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MarketOps.SystemData.Types;

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
            var data = equity
                .Select(x => new SystemValueMapper(x))
                .ToList();
            chartEquity.DataSource = data;
            dbgEquity.DataSource = data;
            chartEquity.DataBind();
        }
    }
}
