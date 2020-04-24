namespace MarketOps.System.Interfaces
{
    /// <summary>
    /// Interface for Money Management new position volume calculation.
    /// </summary>
    public interface IMMSignalVolume
    {
        int GetSignalVolume(Signal signal);
    }
}
