using System.Text.Json;

namespace ChatGptConsole;

public record Configuration(string Token, string AppDir);

public class ConfigurationProvider
{
    #region Singleton
    private static ConfigurationProvider? instance;
    public static ConfigurationProvider Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new();
            }

            return instance;
        }
    }
    #endregion

    private static string appDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ChatGptConsole");
    private static string configFile = Path.Combine(appDir, "config.json");

    private Configuration? configration;

    public Configuration Get()
    {
        if (configration == null)
        {
            configration = LoadFromFile();
        }

        return configration;
    }

    public void Set(string token)
    {
        Directory.CreateDirectory(appDir);

        var cfg = new Configuration(token, appDir);
        var json = JsonSerializer.Serialize(cfg);
        File.WriteAllText(configFile, json);
    }

    private static Configuration LoadFromFile()
    {
        if (!File.Exists(configFile))
        {
            return new(string.Empty, string.Empty);
        }

        var json = File.ReadAllText(configFile);
        return JsonSerializer.Deserialize<Configuration>(json)!;
    }
}
