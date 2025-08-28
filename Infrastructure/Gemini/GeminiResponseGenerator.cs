using GeminiTelegramBot.Domain.ClientInterfaces;

namespace GeminiTelegramBot.Infrastructure.Gemini
{
    public class GeminiResponseGenerator : IResponseGenerator
    {
        public Task<string> GenerateAsync(string input, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
