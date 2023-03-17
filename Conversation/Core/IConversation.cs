namespace ChatGptConsole;

interface IConversation
{
    string ChatKey { get; }
    void Talk(string? message);
}
