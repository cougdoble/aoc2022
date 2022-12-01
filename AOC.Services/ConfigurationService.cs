using System.Text.Json;
using System.Text.Json.Serialization;

namespace AOC.Services;

public static class ConfigurationService
{
    private static readonly Config Config = GetConfig();

    public static string GetCookie()
    {
        return Config.Cookie;
    }

    public static int GetYear()
    {
        return Config.Year;
    }

    public static int[] GetDays()
    {
        return Config.Days;
    }

    private static Config GetConfig(string path = "config.json")
    {
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
        Config config;
        if (File.Exists(path))
        {
            config = JsonSerializer.Deserialize<Config>(File.ReadAllText(path), options);
            config.SetDefaults();
        }
        else
        {
            config = new Config();
            config.SetDefaults();
            File.WriteAllText(path, JsonSerializer.Serialize(config, options));
        }

        return config;
    }
}