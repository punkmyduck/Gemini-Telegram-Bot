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
            string url = $"https://aiplatform.googleapis.com/v1/publishers/google/models/gemini-2.0-flash-lite-001:streamGenerateContent?key=" + "";

            using var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            return new GeminiResponseDto
            {
                Response = await response.Content.ReadAsStringAsync()
            };
        }
    }
}
