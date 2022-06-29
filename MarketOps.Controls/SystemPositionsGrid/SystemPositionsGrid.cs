using MarketOps.Controls.Extensions;
using MarketOps.SystemData.Types;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
            dbgPositions.Columns["R"].DefaultCellStyle.Format = "F4";
            dbgPositions.Columns["RProfit"].DefaultCellStyle.Format = "F2";
        }

        public void LoadData(SystemState systemState)
        {
            dbgPositions.DataSource = systemState.PositionsClosed
                .Select((p, i) => new SystemPositionGridRecord(i, p))
                .ToSortableBindingList();
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
