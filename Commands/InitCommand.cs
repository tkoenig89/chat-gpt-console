using System.CommandLine;

namespace ChatGptConsole.Commands;

class InitCommand : Command
{
    public InitCommand() : base("init", "Initialize the chat bot")
    {
        var tokenOption = new Option<string>(new string[] { "--token", "-t" }, "the OpenAI API token");
        this.AddOption(tokenOption);

        this.SetHandler(token => ConfigurationProvider.Instance.Set(token), tokenOption);
    }
}