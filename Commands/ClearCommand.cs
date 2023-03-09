using System.CommandLine;

namespace ChatGptConsole.Commands;

class ClearCommand : Command
{
    public ClearCommand() : base("clear", "Clear the chat history")
    {
        var keyArgument = new Argument<string>("chatKey", "chat key to clear history of");
        this.AddArgument(keyArgument);

        this.SetHandler(key => ChatStore.Clear(key), keyArgument);
    }
}
