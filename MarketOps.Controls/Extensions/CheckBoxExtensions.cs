using System.Windows.Forms;
using System;

namespace MarketOps.Controls.Extensions
{
    /// <summary>
    /// Extensions for CheckBox
    /// </summary>
    internal static class CheckBoxExtensions
    {
        public static void SetCheckedWithoutEventCall(this CheckBox checkBox, bool newValue, EventHandler eventHandler)
        {
            checkBox.CheckedChanged -= eventHandler;
            try
            {
                checkBox.Checked = newValue;
            }
            finally
            {
                checkBox.CheckedChanged += eventHandler;
            }
        }
    }
}
