namespace ChatGptConsole;

interface IChat
{
    string Talk(string message);
    
    List<OpenAi.Message> GetHistory();

    void ClearHistory();
}
