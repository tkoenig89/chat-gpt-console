using Spectre.Console;

namespace ChatGptConsole;

class CodeConversation : ConsoleConversation, IConversation
{
    private readonly IChat chat;
    
    const string behavior = "Assist me with C# and dotnet 6. Short, precise, and helpful responses. Max 100 chars. No source code explanations.";
    public string ChatKey { get; } = "code";

    public CodeConversation()
    {
        chat = new Chat(ChatKey);
        chat.SetBehavior(behavior);
    }


    public void Talk(string? message)
    {
        Console.CancelKeyPress += (sender, e) => chat.Save();

        message = GetFirstPromptMessage(message);

        do
        {
            var answer = chat.Talk(message);

            WriteToConsoleAndCopyToClipboard(answer);

            message = GetNextPromptMessage();
        } while (message != "exit");
    }
}
