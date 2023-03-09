using System.CommandLine;
using ChatGptConsole.Commands;

var rootCommand = new RootCommand();

rootCommand.AddCommand(new InitCommand());
rootCommand.AddCommand(new ChatCommand());
rootCommand.AddCommand(new ClearCommand());

return rootCommand.Invoke(args);