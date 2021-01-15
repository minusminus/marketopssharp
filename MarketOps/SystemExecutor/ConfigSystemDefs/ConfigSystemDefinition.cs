namespace MarketOps.SystemExecutor.ConfigSystemDefs
{
    /// <summary>
    /// System definition from configuration file.
    /// </summary>
    public class ConfigSystemDefinition
    {
        public string Description { get; set; }
        public string ClassName { get; set; }
        public string ClassLibrary { get; set; }
        public ConfigSystemDefinitionParamDefault[] ParamsDefaults { get; set; }
    }
}
