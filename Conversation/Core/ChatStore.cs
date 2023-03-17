using System.Text.Json;
using ChatGptConsole.OpenAi;

namespace ChatGptConsole;

class ChatStore
{
    private string chatKey;

    public ChatStore(string chatKey)
    {
        this.chatKey = chatKey;
    }

    public void Save(IEnumerable<OpenAi.Message> messages)
    {
        var directoryPath = GetSaveDirectory();
        Directory.CreateDirectory(directoryPath);

        var json = JsonSerializer.Serialize(messages);

        File.WriteAllText(GetFilePath(chatKey), json);
    }

    public List<OpenAi.Message> Load()
    {
        var filePath = GetFilePath(chatKey);
        if (!File.Exists(filePath))
        {
            return new();
        }

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<OpenAi.Message>>(json)!;
    }

    public void Clear()
    {
        var filePath = GetFilePath(chatKey);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    private static string GetSaveDirectory()
    {
        return Path.Combine(ConfigurationProvider.Instance.Get().AppDir, "history");
    }

    private static string GetFilePath(string chatKey)
    {
        return Path.Combine(GetSaveDirectory(), $"{chatKey}.json");
    }
}
