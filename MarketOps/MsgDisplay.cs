using System.Windows.Forms;

namespace MarketOps
{
    /// <summary>
    /// Displays message boxes
    /// </summary>
    internal class MsgDisplay
    {
        private readonly IWin32Window _owner;
        private readonly string _caption;

        public MsgDisplay(IWin32Window owner, string caption)
        {
            _owner = owner;
            _caption = caption;
        }

        public void Error(string msg)
        {
            MessageBox.Show(_owner, msg, _caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void Info(string msg)
        {
            MessageBox.Show(_owner, msg, _caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
