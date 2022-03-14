using System.Drawing;
using System.Windows.Forms;
using MarketOps.SystemData.Types;

namespace MarketOps.Controls.SystemPositionsGrid
{
    public partial class SystemPositionsGrid : UserControl
    {
        private readonly Color LossRowBackgroundColor = Color.FromArgb(0xff, 0xdb, 0xdc);

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

        private void dbgPositions_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected) return;
            var positionInfo = (SystemPositionGridRecord)dbgPositions.Rows[e.RowIndex].DataBoundItem;
            if (positionInfo.Profit >= 0) return;

            e.PaintParts &= ~DataGridViewPaintParts.Background;
            using (Brush brush = new SolidBrush(LossRowBackgroundColor))
                e.Graphics.FillRectangle(brush, e.RowBounds);
        }
    }
}
