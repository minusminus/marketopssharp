namespace MarketOps.SystemData.Interfaces
{
    /// <summary>
    /// System execution logging interface.
    /// </summary>
    public interface ISystemExecutionLogger
    {
        void Add(string message);
    }
}
