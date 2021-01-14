using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemExecutor.MM
{
    /// <summary>
    /// Always returns volume of one.
    /// </summary>
    public class MMSignalVolumeOneItem : IMMSignalVolume
    {
        public int GetSignalVolume(Signal signal)
        {
            return 1;
        }
    }
}
