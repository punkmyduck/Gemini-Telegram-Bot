using GeminiTelegramBot.Application.Services;
using GeminiTelegramBot.Domain.ClientInterfaces;
using GeminiTelegramBot.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace GeminiTelegramBot.Infrastructure.Gemini
{
    public class GeminiApiClient : IGeminiApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly GeminiApiOptions _options;
        private readonly ILogService _logService;
        public GeminiApiClient(
            HttpClient httpClient, 
            IOptions<GeminiApiOptions> options,
            ILogService logService)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _logService = logService;
        }
        public async Task<GeminiResponseDto> CallApiAsync(string json)
        {
            var url = $"{_options.BaseUrl}/{_options.Model}:{_options.Endpoint}?key={_options.ApiKey}";

            using var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            await _logService.LogInfoAsync($"[GeminiApiClient][CallApiAsync] : Posting request ...");
            var response = await _httpClient.PostAsync(url, content);
            await _logService.LogInfoAsync($"[GeminiApiClient][CallApiAsync] : Response received");
            return new GeminiResponseDto
            {
                Response = await response.Content.ReadAsStringAsync()
            };
        }
    }
}
