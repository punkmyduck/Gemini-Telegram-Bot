using GeminiTelegramBot.Infrastructure.Gemini;

namespace GeminiTelegramBot.Domain.ClientInterfaces
{
    public interface IGeminiApiClient
    {
        Task<GeminiResponseDto> CallApiAsync(string json);
    }
}