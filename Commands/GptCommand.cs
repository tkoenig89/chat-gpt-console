using System.CommandLine;
using Spectre.Console;

namespace ChatGptConsole.Commands;

class GptCommand : RootCommand
{

    public GptCommand() : base("gpt")
    {

        var codeConversationCommand = new ConversationCommand(new CodeConversation());
        AddCommand(codeConversationCommand);

        var translateConversationCommand = new ConversationCommand(new TranslateConversation());
        AddCommand(translateConversationCommand);

        var apiKeyOption = new Option<string?>(new string[] { "-a", "--api-key" }, "openAI API Key");
        AddOption(apiKeyOption);

        this.SetHandler(OnTriggered, apiKeyOption);
    }

    private void OnTriggered(string? apiKey)
    {
        if (apiKey is not null)
        {
            ConfigurationProvider.Instance.Set(apiKey);
        }
    }
}