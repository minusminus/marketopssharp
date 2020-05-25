namespace MarketOps.System.Interfaces
{
    /// <summary>
    /// Interface for system data definition generator.
    /// </summary>
    public interface ISystemDataDefinitionProvider
    {
        SystemDataDefinition GetDataDefinition();
    }
}
