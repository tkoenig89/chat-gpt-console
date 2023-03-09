using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ChatGptConsole.OpenAi
{
    public class API
    {
        private readonly HttpClient client;

        public API(string token)
        {
            if(string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Missing token. Please initialize API token first!", nameof(token));
            }

            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<Message> SendAsync(IEnumerable<Message> messages)
        {
            var request = new Request
            {
                Messages = messages.ToList()
            };
            var resp = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", request);
            resp.EnsureSuccessStatusCode();
            var respObject = await resp.Content.ReadFromJsonAsync<Response>();

            return respObject!.Choices[0].Message;
        }
    }
}