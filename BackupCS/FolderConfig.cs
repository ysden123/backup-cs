using System.Text.Json;
using System.Text.Json.Serialization;

namespace BackupCS
{
    public class FolderConfig
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("source")]
        public string? Source { get; set; }
        [JsonPropertyName("destination")]
        public string? Destination { get; set; }
        [JsonPropertyName("directoriesToSkip")]
        public string[]? DirectoriesToSkip { get; set; }
        [JsonPropertyName("maxBackupDirectories")]
        public int MaxBackupDirectories { get; set; }

        public override string ToString()
        {
            return $"FolderConfig: {JsonSerializer.Serialize<FolderConfig>(this)}";
        }

        public static List<FolderConfig>? ReadConfiguration()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "backup-s3", "configuration.json");
            List<FolderConfig>? configuration = JsonSerializer.Deserialize<List<FolderConfig>>(File.ReadAllText(path));
            return configuration;
        }
    }
}
