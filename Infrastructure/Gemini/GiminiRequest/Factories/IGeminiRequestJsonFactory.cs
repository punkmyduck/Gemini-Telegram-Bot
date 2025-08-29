using GeminiTelegramBot.Infrastructure.Gemini.GiminiRequest.RequestDTOs;

namespace GeminiTelegramBot.Infrastructure.Gemini.GiminiRequest.Factories
{
    public interface IGeminiRequestJsonFactory
    {
        GeminiRequestJsonDto CreateStringJson(string prompt);
    }
}