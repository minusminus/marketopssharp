using System.Windows.Forms;
using MarketOps.SystemData.Types;

namespace MarketOps.Controls.SystemPositionsGrid
{
    public partial class SystemPositionsGrid : UserControl
    {
        public delegate void PositionClick(Position position);
        public event PositionClick OnPositionClick;

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

        private void dbgPositions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            OnPositionClick?.Invoke(((SystemPositionGridRecord)dbgPositions.Rows[e.RowIndex].DataBoundItem).Position);
        }
    }
}
