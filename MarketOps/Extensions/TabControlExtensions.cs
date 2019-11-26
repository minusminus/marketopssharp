using System;
using System.Windows.Forms;

namespace MarketOps.Extensions
{
    /// <summary>
    /// Extensinos for TabControl
    /// </summary>
    internal static class TabControlExtensions
    {
        public static void RemoveClickedTab(this TabControl tabControl, MouseEventArgs e)
        {
            for (int i = 0; i < tabControl.TabCount; i++)
                if (tabControl.GetTabRect(i).Contains(e.Location))
                {
                    tabControl.TabPages.RemoveAt(i);
                    return;
                }
        }
    }
}
