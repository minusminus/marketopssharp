namespace MarketOps.Config.Stats
{
    /// <summary>
    /// Stat definition from configuration file.
    /// </summary>
    public class ConfigStatDefinition
    {
        public string StatName { get; set; }
        public string ClassName { get; set; }
        public string ClassLibrary { get; set; }
        public ConfigDisplayStatType DisplayStatType { get; set; }
    }
}
