using System.Text.Json;
using System.Text;
using GeminiTelegramBot.Domain.ClientInterfaces;

namespace GeminiTelegramBot.Infrastructure.Gemini
{
    public class GeminiReponseParser : IGeminiReponseParser
    {
        private readonly IGeminiEnvelopeExtractor _extractor;
        public GeminiReponseParser(IGeminiEnvelopeExtractor extractor)
        {
            _extractor = extractor;
        }
        public string ParseGeminiResponse(string responseText)
        {
            try
            {
                using var doc = JsonDocument.Parse(responseText);
                var root = doc.RootElement;

                return root.ValueKind switch
                {
                    JsonValueKind.Object => _extractor.Extract(root),
                    JsonValueKind.Array => string.Join("", root.EnumerateArray()
                                                           .Where(e => e.ValueKind == JsonValueKind.Object)
                                                           .Select(_extractor.Extract)),
                    _ => "Нет текста в ответе модели."
                };
            }
            catch (JsonException ex)
            {
                return $"Ошибка парсинга JSON: {ex.Message}\nСырой ответ: {responseText}";
            }
            catch (Exception ex)
            {
                return $"Неожиданная ошибка: {ex.Message}\nСырой ответ: {responseText}";
            }
        }
    }
}
