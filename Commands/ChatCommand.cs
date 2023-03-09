using System.CommandLine;

namespace ChatGptConsole.Commands;

class ChatCommand : Command
{
    const string chatKey = "code";
    const string behavior = "Hilf mir mit C# und dotnet 6. Kurze, präzise und hilfreiche Antworten. Maximal 100 Zeichen. Keine Erklärungen zu Source Code";

    public ChatCommand() : base("chat")
    {
        var messageOption = new Argument<string>("message", "chat message");
        AddArgument(messageOption);

        this.SetHandler((message) => new Chat(chatKey, behavior).Talk(message), messageOption);
    }
}