using System.Windows.Forms;
using MarketOps.Controls.ChartsUtils;

namespace MarketOps.Controls.Extensions
{
    /// <summary>
    /// Extensions for TabPage.
    /// </summary>
    public static class TabPageExtensions
    {
        public static void HidePriceAreaToolTips(this TabPage tabPage)
        {
            var tbl = tabPage.Controls.Find("pvp", true);
            ((PriceVolumePanel)tbl[0]).Chart.HidePriceAreaToolTips();
        }
    }
}
