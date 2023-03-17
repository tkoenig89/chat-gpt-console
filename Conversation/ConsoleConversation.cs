using Spectre.Console;

namespace ChatGptConsole;

abstract class ConsoleConversation
{
    protected static string GetFirstPromptMessage(string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            message = GetNextPromptMessage();
        }

        return message;
    }

    protected static string GetNextPromptMessage(string prompt = "Prompt?")
    {
        return AnsiConsole.Prompt<string>(new TextPrompt<string>(prompt));
    }

    protected static void WriteToConsoleAndCopyToClipboard(string response)
    {
        TextCopy.ClipboardService.SetText(response);
        AnsiConsole.MarkupLineInterpolated($"[bold #dadada]{response}[/][#aaa italic] | Copied to Clipboard[/]");
    }
}