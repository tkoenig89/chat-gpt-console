using ChatGptConsole.OpenAi;

namespace ChatGptConsole;

class Chat : IChat
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

    public string Talk(string message)
    {
        history.Add(new Message("user", message));
        var resp = api.SendAsync(history.GetAll()).Result;
        history.Add(resp);

        return resp.Content;
    }
}