using System.Drawing;
using System.Windows.Forms;
using MarketOps.Controls.Types;
using MarketOps.StockData.Types;

namespace MarketOps.Controls.PriceChart
{
    public partial class FormEditStockStatParams : Form
    {
        private StockStat _stat;

        public FormEditStockStatParams()
        {
            InitializeComponent();
        }

        public bool Execute(StockStat stat)
        {
            _stat = stat;
            SetCaption(stat.Name);
            LoadStatParams(stat.StatParams);
            LoadStatDataColors(stat);
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

        private void OnDataColorButtonClick(object sender, System.EventArgs e)
        {
            Button btn = (Button) sender;

            dlgColor.Color = btn.BackColor;
            if (dlgColor.ShowDialog(this) != DialogResult.OK) return;
            btn.BackColor = dlgColor.Color;
            _stat.DataColor[(int)btn.Tag] = dlgColor.Color;
        }
    }
}
