using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketOps.Controls.Extensions
{
    /// <summary>
    /// Rozszerzenie umożliwiające uruchomienie modalnej formy asynchronicznie.
    ///
    /// Na podstawie:
    /// https://stackoverflow.com/a/33411037/5912466
    /// </summary>
    internal static class ShowDialogExtensions
    {
        public static async Task<DialogResult> ShowDialogAsync(this Form frm)
        {
            await Task.Yield();
            return frm.IsDisposed ? DialogResult.OK : frm.ShowDialog();
        }
    }
}
