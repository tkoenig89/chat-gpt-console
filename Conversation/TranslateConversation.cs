using Spectre.Console;

namespace ChatGptConsole;

class TranslateConversation : ConsoleConversation, IConversation
{
    private readonly IChat chat;
    
    const string behavior = "You are a translator. German to English.";
    public string ChatKey { get; } = "translate";

    public TranslateConversation()
    {
        chat = new Chat(ChatKey);
        chat.SetBehavior(behavior);
    }


    public void Talk(string? message)
    {
        message = GetFirstPromptMessage(message);

        var translation = chat.Talk(message);

        WriteToConsoleAndCopyToClipboard(translation);
    }
}