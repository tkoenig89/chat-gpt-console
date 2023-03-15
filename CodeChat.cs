using ChatGptConsole.OpenAi;

namespace ChatGptConsole;

class CodeChat : IChat
{
    const string chatKey = "code";
    const string behavior = "Hilf mir mit C# und dotnet 6. Kurze, präzise und hilfreiche Antworten. Maximal 100 Zeichen. Keine Erklärungen zu Source Code";
    private readonly IChat chat;

    public CodeChat()
    {
        chat = new Chat(chatKey, behavior);
    }

    public void ClearHistory()
    {
        chat.ClearHistory();
    }

    public List<Message> GetHistory()
    {
        return chat.GetHistory();
    }

    public string Talk(string message)
    {
        return chat.Talk(message);
    }
}