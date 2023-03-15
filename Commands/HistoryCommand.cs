using System.CommandLine;
using Spectre.Console;

namespace ChatGptConsole.Commands;

class HistoryCommand : Command
{
    private readonly CodeChat chat;

    public HistoryCommand() : base("history")
    {
        chat = new CodeChat();

        var tailOption = new Option<int?>(new string[] { "-t", "--tail" }, "show last n entries");
        AddOption(tailOption);

        var clearOption = new Option<bool>(new string[] { "-c", "--clear" }, "clears the conversation");
        AddOption(clearOption);

        this.SetHandler(OnTriggered, tailOption, clearOption);
    }

    private void OnTriggered(int? tail, bool clear)
    {
        if (clear)
        {
            chat.ClearHistory();
            AnsiConsole.MarkupLine("[dim]History has been cleared.[/]");
            return;
        }

        IEnumerable<OpenAi.Message> history = chat.GetHistory();
        if (tail is not null)
        {
            history = history.TakeLast(tail.Value);
        }

        foreach (var he in history)
        {
            AnsiConsole.MarkupLineInterpolated($"[bold underline dim]{he.Role}[/]\n[italic]{he.Content}[/]\n");
        }
    }
}