using ChatGptConsole.OpenAi;

namespace ChatGptConsole;

class Chat : IChat
{
    private readonly API api;
    private List<OpenAi.Message> messages = new();

    public string ChatKey { get; }

    public Chat(string chatKey)
    {
        ChatKey = chatKey;
        api = new(ConfigurationProvider.Instance.Get().Token);
    }

    public void Save()
    {
        var store = new ChatStore(ChatKey);
        var history = store.Load();
        history.AddRange(messages);
        store.Save(history);
    }

    public void SetBehavior(string behavior)
    {
        var systemMsg = new OpenAi.Message("system", behavior);
        messages = new() { systemMsg };
    }

    public virtual string Talk(string message)
    {
        messages.Add(new OpenAi.Message("user", message));
        
        var resp = api.SendAsync(messages).Result;

        messages.Add(resp);

        return resp.Content;
    }
}
