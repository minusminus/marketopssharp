namespace MarketOps.SystemExecutor.Interfaces
{
    /// <summary>
    /// Interface for system data definition generator.
    /// </summary>
    public interface ISystemDataDefinitionProvider
    {
        SystemDataDefinition GetDataDefinition();
    }
}
