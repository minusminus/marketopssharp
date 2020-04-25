using MarketOps.System.Interfaces;

namespace MarketOps.System.MM
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
