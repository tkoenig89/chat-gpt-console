using System.Text.Json;

namespace ChatGptConsole;

class ChatStore
{
    private string chatKey;
    private List<OpenAi.Message> messages = new();

    public ChatStore(string chatKey)
    {
        this.chatKey = chatKey;
    }

    public void Add(OpenAi.Message message)
    {
        messages.Add(message);
        Save();
    }

    public List<OpenAi.Message> GetAll()
    {
        return messages;
    }

    public void Clear()
    {
        var filePath = GetFilePath(chatKey);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public static bool TryLoadFromFile(string chatKey, out ChatStore store)
    {
        store = new(chatKey);

        var filePath = GetFilePath(chatKey);
        if (!File.Exists(filePath))
        {
            return false;
        }

        var json = File.ReadAllText(filePath);
        store.messages = JsonSerializer.Deserialize<List<OpenAi.Message>>(json)!;

        return true;
    }

    private void Save()
    {
        var json = JsonSerializer.Serialize(messages);
        File.WriteAllText(GetFilePath(chatKey), json);
    }

    private static string GetFilePath(string chatKey)
    {
        return Path.Combine(ConfigurationProvider.Instance.Get().AppDir, $"{chatKey}-history.json");
    }
}
