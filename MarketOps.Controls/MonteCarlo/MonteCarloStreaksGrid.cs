using MarketOps.SystemAnalysis.MonteCarlo;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MarketOps.Controls.MonteCarlo
{
    public partial class MonteCarloStreaksGrid : UserControl
    {
        public MonteCarloStreaksGrid()
        {
            InitializeComponent();
        }
        public void LoadData(List<MonteCarloStreakData> equity)
        {
            int totalCount = equity.Sum(x => x.Count);
            dbgStreaks.DataSource = equity
                .Select(x => new MonteCarloStreakDataMapper(x, totalCount))
                .ToList();
        }
    }
}
