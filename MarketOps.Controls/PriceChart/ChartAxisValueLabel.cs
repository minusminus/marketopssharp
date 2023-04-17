using System;
using System.Windows.Forms;

namespace MarketOps.Controls.PriceChart
{
    public partial class ChartAxisValueLabel : UserControl
    {
        //private readonly Chart _chart;

        //public ChartAxisValueLabel(Chart chart)
        //{
        //    _chart = chart;
        //    InitializeComponent();

        //    Parent = _chart;
        //    Visible = false;
        //}

        //private readonly PositionChangeChecker _positionChangeChecker = new PositionChangeChecker();

        //public void ShowCenterOnValueHorizontally(string text, int x, int y)
        //{
        //    SetLabelText(text);
        //    ShowIfPosChanged(x - Width/2, y);
        //}

        //public void ShowCenterOnValueVertically(string text, int x, int y)
        //{
        //    SetLabelText(text);
        //    ShowIfPosChanged(x, y - Height/2);
        //}

        //private void SetLabelText(string text)
        //{
        //    lblValue.Text = text;
        //}

        //private void ShowIfPosChanged(int x, int y)
        //{
        //    if (!_positionChangeChecker.SetAndCheckChange(x, y)) return;
        //    Left = Math.Min(Math.Max(x, 0), _chart.Width - Width);
        //    Top = Math.Max(y, 0);
        //    Visible = true;
        //}
    }
}
