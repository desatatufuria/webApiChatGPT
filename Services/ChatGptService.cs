using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using webApiChatGPT.Interfaces;
using webApiChatGPT.Models;

namespace webApiChatGPT.Services
{
    public class ChatGptService : IChatGptService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenAISettings _openAISettings;

        public ChatGptService(IOptions<OpenAISettings> openAISettings, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _openAISettings = openAISettings.Value;
        }

        public async Task<string> GetResponseAsync(string prompt)
        {
            var requestBody = new
            {
                model = _openAISettings.Model,
                messages = new[]
                {
                new { role = "user", content = prompt }
            },
                max_tokens = 150
            };

            _httpClient.DefaultRequestHeaders.Authorization = new 
                AuthenticationHeaderValue("Bearer", _openAISettings.ApiKey);

            var response = await _httpClient.PostAsJsonAsync(_openAISettings.ApiUrl, requestBody);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
 
        }
    }
}
