using System.Text.Json.Serialization;

namespace ChatGptConsole.OpenAi;

record Response
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("object")]
    public string Object { get; set; } = string.Empty;

    [JsonPropertyName("created")]
    public int Created { get; set; }    

    [JsonPropertyName("choices")]
    public Choice[] Choices { get; set; } = new Choice[0];

    [JsonPropertyName("usage")]
    public Usage Usage { get; set; } = new Usage();
}
