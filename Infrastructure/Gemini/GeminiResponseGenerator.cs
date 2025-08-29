using GeminiTelegramBot.Application.Services;
using GeminiTelegramBot.Domain.ClientInterfaces;
using GeminiTelegramBot.Infrastructure.Gemini.GiminiRequest.Factories;
using GeminiTelegramBot.Infrastructure.Gemini.GiminiRequest.RequestDTOs;

namespace GeminiTelegramBot.Infrastructure.Gemini
{
    public class GeminiResponseGenerator : IResponseGenerator
    {
        private readonly IGeminiReponseParser _geminiReponseParser;
        private readonly IGeminiRequestJsonFactory _geminiJsonRequestFactory;
        private readonly IGeminiApiClient _geminiApiClient;
        private readonly ILogService _logService;
        public GeminiResponseGenerator(
            IGeminiReponseParser geminiReponseParser, 
            IGeminiRequestJsonFactory geminiContentFactory,
            IGeminiApiClient geminiApiClient,
            ILogService logService)
        {
            _geminiReponseParser = geminiReponseParser;
            _geminiJsonRequestFactory = geminiContentFactory;
            _geminiApiClient = geminiApiClient;
            _logService = logService;
        }
        public async Task<string> GenerateAsync(string prompt, CancellationToken token)
        {
            GeminiRequestJsonDto json = _geminiJsonRequestFactory.CreateStringJson(prompt);

            GeminiResponseDto response = await _geminiApiClient.CallApiAsync(json.Json);

            if (response.Response == null)
            {
                throw new ArgumentNullException("Ошибка при получении ответа", nameof(response));
            }

            return _geminiReponseParser.ParseGeminiResponse(response.Response);
        }
    }
}
