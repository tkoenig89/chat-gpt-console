namespace ChatGptConsole;

class CodeChat : IChat
{
    const string chatKey = "code";
    const string behavior = "Hilf mir mit C# und dotnet 6. Kurze, präzise und hilfreiche Antworten. Maximal 100 Zeichen. Keine Erklärungen zu Source Code";
    private readonly Chat chat;

    public CodeChat()
    {
        chat = new Chat(chatKey, behavior);
    }

    public string Talk(string message)
    {
        return chat.Talk(message);
    }
}