using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using ChatGptConsole.OpenAi;

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

    public IEnumerable<OpenAi.Message> GetAll()
    {
        return messages;
    }

    public static void Clear(string chatKey)
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

class Chat
{
    private readonly API api;
    private readonly ChatStore history;

    public Chat(string chatKey, string behavior)
    {
        api = new(ConfigurationProvider.Instance.Get().Token);

        if (!ChatStore.TryLoadFromFile(chatKey, out history))
        {
            history = new(chatKey);
            SetBehavior(behavior);
        }
    }

    private void SetBehavior(string behavior)
    {
        var systemMsg = new OpenAi.Message("system", behavior);
        history.Add(systemMsg);
    }


    public void Talk(string message)
    {
        history.Add(new Message("user", message));
        var resp = api.SendAsync(history.GetAll()).Result;
        history.Add(resp);

        Console.WriteLine(resp.Content);
    }
}