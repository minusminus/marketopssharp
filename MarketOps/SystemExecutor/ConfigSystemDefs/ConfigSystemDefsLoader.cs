using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace MarketOps.SystemExecutor.ConfigSystemDefs
{
    /// <summary>
    /// Systems definitions loader from configuration file.
    /// </summary>
    internal static class ConfigSystemDefsLoader
    {
        private readonly static string ConfigFilePath = @"MarketOpsSystems.json";

        public static List<ConfigSystemDefinition> Load()
        {
            string jsonConfig = File.ReadAllText(ConfigFilePath);
            return JsonSerializer
                .Deserialize<IEnumerable<ConfigSystemDefinition>>(jsonConfig)
                .OrderBy(x => x.Description)
                .ToList();
        }
    }
}
