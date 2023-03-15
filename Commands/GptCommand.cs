using System.CommandLine;
using Spectre.Console;

namespace ChatGptConsole.Commands;

class GptCommand : RootCommand
{
    private readonly CodeChat chat;

    public GptCommand() : base("gpt")
    {
        chat = new CodeChat();

        var apiKeyOption = new Option<string?>(new string[] { "-a", "--api-key" }, "openAI API Key");
        AddOption(apiKeyOption);

        var historyCommand = new HistoryCommand();
        AddCommand(historyCommand);

        var messageArgument = new Argument<string?>("message", () => null, "prompt or question for instant gpt query");
        AddArgument(messageArgument);

        this.SetHandler(OnTriggered, apiKeyOption, messageArgument);
    }

    private void OnTriggered(string? apiKey, string? msg)
    {
        InitApiKey(apiKey);

        HandleChatInteraction(msg);
    }

    private void HandleChatInteraction(string? msg)
    {
        if (string.IsNullOrWhiteSpace(msg))
        {
            msg = AnsiConsole.Prompt<string>(new TextPrompt<string>("Wie kann ich helfen?"));
        }

        var resp = chat.Talk(msg);
        AnsiConsole.WriteLine(resp);
    }

    private void ShowHistory(bool showHistory)
    {
        var history = chat.GetHistory().TakeLast(5);
        foreach (var he in history)
        {
            AnsiConsole.MarkupLineInterpolated($"[bold #aaa]{he.Role}[/]\n[#fff]{he.Content}[/]\n");
        }
    }


    private static void InitApiKey(string? apiKey)
    {
        if (apiKey is not null)
        {
            ConfigurationProvider.Instance.Set(apiKey);
        }
    }
}