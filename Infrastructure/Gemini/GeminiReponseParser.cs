using System.Text.Json;
using System.Text;
using GeminiTelegramBot.Domain.ClientInterfaces;

namespace GeminiTelegramBot.Infrastructure.Gemini
{
    public class GeminiReponseParser : IGeminiReponseParser
    {
        public string ParseGeminiResponse(string responseText)
        {
            JsonElement root;
            JsonDocument doc;
            try
            {
                doc = JsonDocument.Parse(responseText);
                root = doc.RootElement;
            }
            catch (JsonException ex)
            {
                return $"Ошибка парсинга JSON: {ex.Message}\nСырой ответ: {responseText}";
            }
            catch (Exception ex)
            {
                return $"Неожиданная ошибка: {ex.Message}\nСырой ответ: {responseText}";
            }
            var sb = new StringBuilder();

            if (root.ValueKind == JsonValueKind.Object)
            {
                // Обычный (нестриминговый) ответ
                AppendFromEnvelope(root, sb);
            }
            else if (root.ValueKind == JsonValueKind.Array)
            {
                // Стриминговый ответ — несколько объектов
                foreach (var item in root.EnumerateArray())
                {
                    if (item.ValueKind == JsonValueKind.Object)
                        AppendFromEnvelope(item, sb);
                }
            }

            var text = sb.ToString().Trim();

            doc.Dispose();

            return string.IsNullOrWhiteSpace(text) ? "Нет текста в ответе модели." : text;
        }

        private void AppendFromEnvelope(JsonElement envelope, StringBuilder sb)
        {
            if (!envelope.TryGetProperty("candidates", out var candidates) ||
                candidates.ValueKind != JsonValueKind.Array) return;

            foreach (var cand in candidates.EnumerateArray())
            {
                if (!cand.TryGetProperty("content", out var content)) continue;
                if (!content.TryGetProperty("parts", out var parts) ||
                    parts.ValueKind != JsonValueKind.Array) continue;

                foreach (var part in parts.EnumerateArray())
                {
                    if (part.TryGetProperty("text", out var t))
                    {
                        var chunk = t.GetString();
                        if (!string.IsNullOrEmpty(chunk))
                            sb.Append(chunk);
                    }
                }
            }
        }
    }
}
