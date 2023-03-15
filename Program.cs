using System.CommandLine;
using ChatGptConsole;
using Spectre.Console;

var rootCommand = new RootCommand();

var apiKeyOption = new Option<string?>(new string[] { "-a", "--api-key" }, "openAI API Key");
rootCommand.AddOption(apiKeyOption);

var clearOption = new Option<bool>(new string[] { "-c", "--clear" }, "clears the conversation");
rootCommand.AddOption(clearOption);

var messageArgument = new Argument<string?>("message", () => null, "prompt or question for instant gpt query");
rootCommand.AddArgument(messageArgument);

rootCommand.SetHandler((apiKey, clear, msg) =>
{
    if (apiKey is not null)
    {
        ConfigurationProvider.Instance.Set(apiKey);
    }

    if (clear)
    {
        ChatStore.Clear("code");
    }

    if (string.IsNullOrWhiteSpace(msg))
    {
        msg = AnsiConsole.Prompt<string>(new TextPrompt<string>("Wie kann ich helfen?"));
    }
    var chat = new CodeChat();
    var resp = chat.Talk(msg);

    AnsiConsole.WriteLine(resp);
}, apiKeyOption, clearOption, messageArgument);

return rootCommand.Invoke(args);
