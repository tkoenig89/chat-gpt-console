using System.CommandLine;

namespace ChatGptConsole.Commands;

class ConversationCommand : Command
{
    public ConversationCommand(IConversation conversation) : base(conversation.ChatKey)
    {
        var historyCommand = new HistoryCommand(conversation.ChatKey);
        AddCommand(historyCommand);

        var messageArgument = new Argument<string?>("message", () => null, "prompt or question for instant gpt query");
        AddArgument(messageArgument);

        this.SetHandler(msg => conversation.Talk(msg), messageArgument);
    }
}