using System.Text.Json.Serialization;

namespace ChatGptConsole.OpenAi;

public record Message
{
    public Message()
    {
    }

    public Message(string role, string content)
    {
        Role = role;
        Content = content;
    }

    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; }  = string.Empty;    
}

