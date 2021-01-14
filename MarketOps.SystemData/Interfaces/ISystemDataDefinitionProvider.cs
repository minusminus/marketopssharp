using MarketOps.SystemData.Types;

namespace MarketOps.SystemData.Interfaces
{
    /// <summary>
    /// Interface for system data definition generator.
    /// </summary>
    public interface ISystemDataDefinitionProvider
    {
        SystemDataDefinition GetDataDefinition();
    }
}
