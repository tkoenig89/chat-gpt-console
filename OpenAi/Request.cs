using System.Text.Json.Serialization;

namespace ChatGptConsole.OpenAi;

record Request
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = "gpt-3.5-turbo";

    [JsonPropertyName("messages")]
    public List<Message> Messages { get; set; } = new();
}
