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
            FillBindingSource(equity);
        }

        private void FillBindingSource(List<MonteCarloStreakData> streaks)
        {
            srcStreaks.Clear();
            if (streaks.Count == 0) return;
            int totalCount = streaks.Sum(x => x.Count);
            streaks.ForEach(v => srcStreaks.Add(new MonteCarloStreakDataMapper(v, totalCount)));
        }
    }
}
