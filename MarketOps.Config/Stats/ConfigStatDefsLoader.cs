using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MarketOps.Config.Stats
{
    /// <summary>
    /// Stats definitions loader from configuration file.
    /// </summary>
    public static class ConfigStatDefsLoader
    {
        private readonly static string ConfigFilePath = @"MarketOpsStats.json";

        public static List<ConfigStatDefinition> Load()
        {
            string jsonConfig = File.ReadAllText(ConfigFilePath);
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
            return JsonSerializer
                .Deserialize<IEnumerable<ConfigStatDefinition>>(jsonConfig, serializerOptions)
                .Where(x => !x.StatName.StartsWith("."))
                .OrderBy(x => x.StatName)
                .ToList();
        }
    }
}
