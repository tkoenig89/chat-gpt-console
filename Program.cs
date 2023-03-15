using System.CommandLine;
using ChatGptConsole.Commands;

var rootCommand = new GptCommand();
rootCommand.Invoke(args);