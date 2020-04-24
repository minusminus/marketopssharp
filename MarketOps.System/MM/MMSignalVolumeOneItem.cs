using MarketOps.System.Interfaces;

namespace MarketOps.System.MM
{
    public class MMSignalVolumeOneItem : IMMSignalVolume
    {
        public int GetSignalVolume(Signal signal)
        {
            return 1;
        }
    }
}
