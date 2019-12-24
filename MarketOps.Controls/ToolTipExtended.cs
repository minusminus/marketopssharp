using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MarketOps.Controls
{
    public partial class ToolTipExtended : ToolTip
    {
        public ToolTipExtended()
        {
            InitializeComponent();
            OwnerDraw = true;
            Draw += OnDraw;
            //Popup += OnPopup;

            ForeColor = Color.DimGray;
            BackColor = Color.WhiteSmoke;
        }

        [Category("Custom")]
        public Font TooltipFont { get; set; }

        private readonly PositionChangeChecker _positionChangeChecker = new PositionChangeChecker();

        //private const int Margin = 4;

        //private void OnPopup(object sender, PopupEventArgs e)
        //{
        //    e.ToolTipSize = TextRenderer.MeasureText(GetToolTip(e.AssociatedControl), TooltipFont) + new Size(Margin, Margin);
        //}

        private void OnDraw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                sf.FormatFlags = StringFormatFlags.NoWrap;
                e.Graphics.DrawString(e.ToolTipText, TooltipFont, new SolidBrush(ForeColor), e.Bounds, sf);
            }
        }

        public void ShowIfPosChanged(string text, IWin32Window window, int x, int y)
        {
            if (!_positionChangeChecker.SetAndCheckChange(x, y)) return;
            RemoveAll();
            Show(text, window, x, y);
        }
    }
}
