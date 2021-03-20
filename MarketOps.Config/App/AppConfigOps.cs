using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MarketOps.Config.App
{
    /// <summary>
    /// Loading and sacing AppConfig object to/from file.
    /// </summary>
    public static class AppConfigOps
    {
        private readonly static string ConfigFilePath = @"MarketOps.json";

        public static AppConfig Load()
        {
            if (!File.Exists(ConfigFilePath)) return new AppConfig();
            return JsonSerializer.Deserialize<AppConfig>(File.ReadAllText(ConfigFilePath), SerializerOptions());
        }

        public static void Save(AppConfig appConfig) => 
            File.WriteAllText(ConfigFilePath, JsonSerializer.Serialize<AppConfig>(appConfig, SerializerOptions()));

        private static JsonSerializerOptions SerializerOptions() =>
            new JsonSerializerOptions()
            {
                WriteIndented = true,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
    }
}
