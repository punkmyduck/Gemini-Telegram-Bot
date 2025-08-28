using GeminiTelegramBot.Domain.ClientInterfaces;

namespace GeminiTelegramBot.Infrastructure.Gemini
{
    public class GeminiApiClient : IGeminiApiClient
    {
        private readonly HttpClient _httpClient;
        public GeminiApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<GeminiResponseDto> CallApiAsync(string json)
        {
            string url = $"";

            using var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            return new GeminiResponseDto
            {
                Response = await response.Content.ReadAsStringAsync()
            };
        }
    }
}
