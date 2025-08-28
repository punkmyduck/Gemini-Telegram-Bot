using System.Text.Json;
using System.Text;
using GeminiTelegramBot.Domain.ClientInterfaces;

namespace GeminiTelegramBot.Infrastructure.Gemini
{
    public class GeminiResponseGenerator : IResponseGenerator
    {
        public async Task<string> GenerateAsync(string prompt, CancellationToken token)
        {
            string url = $"https://aiplatform.googleapis.com/v1/publishers/google/models/gemini-2.0-flash-lite-001:streamGenerateContent?key=" + "AQ.Ab8RN6K1nUo_Hc9vaSGLN2EzGWMkYtXPBPHJFRtbxZJgpvecXQ";

            var requestBody = new
            {
                contents = new[]
                {
                new
                {
                    role = "user",
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            },
                systemInstruction = new
                {
                    role = "system",
                    parts = new[]
                    {
                    new { text = "Отвечай кратко и по существу. Не развертывай длинные объяснения, если не было просьбы об обратном." }
                }
                },
                generationConfig = new
                {
                    temperature = 0.7,
                    topP = 0.95,
                    topK = 40,
                    maxOutputTokens = 1000
                }
            };

            using var httpClient = new HttpClient();
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);
            var responseText = await response.Content.ReadAsStringAsync();

            try
            {
                return ParseGeminiResponse(responseText);
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

        static string ParseGeminiResponse(string responseText)
        {
            using var doc = JsonDocument.Parse(responseText);
            var root = doc.RootElement;
            var sb = new StringBuilder();

            void AppendFromEnvelope(JsonElement envelope)
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

            if (root.ValueKind == JsonValueKind.Object)
            {
                // Обычный (нестриминговый) ответ
                AppendFromEnvelope(root);
            }
            else if (root.ValueKind == JsonValueKind.Array)
            {
                // Стриминговый ответ — несколько объектов
                foreach (var item in root.EnumerateArray())
                {
                    if (item.ValueKind == JsonValueKind.Object)
                        AppendFromEnvelope(item);
                }
            }

            var text = sb.ToString().Trim();
            return string.IsNullOrWhiteSpace(text) ? "Нет текста в ответе модели." : text;
        }
    }
}
