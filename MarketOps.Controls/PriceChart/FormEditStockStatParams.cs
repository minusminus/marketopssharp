using System.Windows.Forms;
using MarketOps.StockData.Types;

namespace MarketOps.Controls.PriceChart
{
    public partial class FormEditStockStatParams : Form
    {
        public FormEditStockStatParams()
        {
            InitializeComponent();
        }

        public bool Execute(StockStat stat)
        {
            SetCaption(stat.Name);
            dgvParams.LoadParams(stat.StatParams);
            LoadStatDataColors(stat);
            if (ShowDialog() == DialogResult.OK)
            {
                dgvParams.SaveParams(stat.StatParams);
                SaveStatDataColors(stat);
                return true;
            }
            return false;
        }

        private void SetCaption(string statName)
        {
            Text = $"{statName} parameters";
        }

        private void LoadStatDataColors(StockStat stat)
        {
            pnlTblLayoutColors.ColumnStyles.Clear();
            pnlTblLayoutColors.ColumnCount = stat.DataCount;
            for (int i = 0; i < stat.DataCount; i++)
            {
                pnlTblLayoutColors.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / (float)stat.DataCount));
                Button btn = new Button
                {
                    Text = stat.DataName(i),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = stat.DataColor[i],
                    Dock = DockStyle.Fill,
                    Tag = i,
                };
                btn.Click += OnDataColorButtonClick;
                pnlTblLayoutColors.Controls.Add(btn, i, 0);
            }
        }

        private void SaveStatDataColors(StockStat stat)
        {
            foreach(var control in pnlTblLayoutColors.Controls)
            {
                if (!(control is Button)) continue;
                Button btn = (Button)control;
                stat.DataColor[(int)btn.Tag] = btn.BackColor;
            }
        }

        private void OnDataColorButtonClick(object sender, System.EventArgs e)
        {
            Button btn = (Button) sender;

            dlgColor.Color = btn.BackColor;
            if (dlgColor.ShowDialog(this) != DialogResult.OK) return;
            btn.BackColor = dlgColor.Color;
        }
    }
}
