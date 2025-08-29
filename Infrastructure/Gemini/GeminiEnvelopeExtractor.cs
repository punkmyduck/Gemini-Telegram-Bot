using GeminiTelegramBot.Application.Services;
using GeminiTelegramBot.Domain.ClientInterfaces;
using System.Text;
using System.Text.Json;

namespace GeminiTelegramBot.Infrastructure.Gemini
{
    public class GeminiEnvelopeExtractor : IGeminiEnvelopeExtractor
    {
        private readonly ILogService _logService;
        public GeminiEnvelopeExtractor(ILogService logService)
        {
            _logService = logService;
        }
        public string Extract(JsonElement envelope)
        {
            _logService.LogInfoAsync($"[GeminiEnvelopeExtractor][Extract] Extracting properties ...");
            if (!envelope.TryGetProperty("candidates", out var candidates)
                || candidates.ValueKind != JsonValueKind.Array) return string.Empty;

            StringBuilder sb = new StringBuilder();

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

            return sb.ToString().Trim();
        }
    }
}
