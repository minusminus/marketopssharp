using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MarketOps.Controls.ChartsUtils;
using MarketOps.SystemData.Types;

namespace MarketOps.Controls.SystemEquity
{
    public partial class SystemEquityChart : UserControl
    {
        private ChartArea _areaEquity => chartEquity.ChartAreas["areaEquity"];
        private ChartArea _areaCapitalUsage => chartEquity.ChartAreas["areaCapitalUsage"];
        private Series _seriesEquity => chartEquity.Series["seriesEquity"];
        private Series _seriesCapitalUsage => chartEquity.Series["seriesCapitalUsage"];
        private Title _titleCapitalUsage => chartEquity.Titles["titleCapitalUsage"];

        public SystemEquityChart()
        {
            InitializeComponent();
            _areaCapitalUsage.AxisY.Minimum = 0;
            _areaCapitalUsage.AxisY.Maximum = 100;
            _titleCapitalUsage.Visible = false;
        }

        public void LoadData(List<SystemValue> equity, List<SystemValue> equityCapitalUsage)
        {
            ResizeChartAreas(equityCapitalUsage != null);
            LoadEquity(equity);
            if (equityCapitalUsage != null)
                LoadEquityCapitalUsage(equityCapitalUsage);
        }

        private void ResizeChartAreas(bool capitalUsageProvided)
        {
            _areaCapitalUsage.Visible = capitalUsageProvided;
            _titleCapitalUsage.Visible = capitalUsageProvided;
            if (!capitalUsageProvided)
                _areaEquity.Position.Height = 100;
        }

        private void LoadEquity(List<SystemValue> equity)
        {
            var data = equity
                .Select(x => new SystemValueMapper(x))
                .ToList();
            chartEquity.DataSource = data;
            dbgEquity.DataSource = data;
            chartEquity.DataBind();
        }

        private void LoadEquityCapitalUsage(List<SystemValue> equityCapitalUsage)
        {
            Series series = _seriesCapitalUsage;
            series.Points.Clear();
            for (int i = 0; i < equityCapitalUsage.Count; i++)
                series.Points.AddXY(equityCapitalUsage[i].TS, 100.0f * equityCapitalUsage[i].Value);
            //series.Color = Color.Red;
        }
    }
}
