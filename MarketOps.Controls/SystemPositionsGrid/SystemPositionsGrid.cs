using System.Windows.Forms;
using MarketOps.SystemData.Types;

namespace MarketOps.Controls.SystemPositionsGrid
{
    public partial class SystemPositionsGrid : UserControl
    {
        public SystemPositionsGrid()
        {
            InitializeComponent();
        }

        public void LoadData(SystemState systemState)
        {
            srcPositions.Clear();
            for (int i = 0; i < systemState.PositionsClosed.Count; i++)
                srcPositions.Add(new SystemPositionGridRecord(i, systemState.PositionsClosed[i]));
        }
    }
}
