namespace ChatGptConsole;

interface IChat
{
    string Talk(string message);
    void SetBehavior(string behavior);
    void Save();
}
