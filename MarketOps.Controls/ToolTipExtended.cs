using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Popup += OnPopup;

            TooltipFontColor = Color.DimGray;
            TooltipBackgroundColor = Color.WhiteSmoke;
        }

        [Category("Custom")]
        public Font TooltipFont { get; set; }
        [Category("Custom")]
        public Color TooltipFontColor { get; set; }
        [Category("Custom")]
        public Color TooltipBackgroundColor { get; set; }

        private const int Margin = 4;

        private void OnPopup(object sender, PopupEventArgs e)
        {
            e.ToolTipSize = TextRenderer.MeasureText(GetToolTip(e.AssociatedControl), TooltipFont) + new Size(Margin, Margin);
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(TooltipBackgroundColor), e.Bounds);
            e.DrawBorder();
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                sf.FormatFlags = StringFormatFlags.NoWrap;
                e.Graphics.DrawString(e.ToolTipText, TooltipFont, new SolidBrush(TooltipFontColor), e.Bounds, sf);
            }
        }
    }
}
