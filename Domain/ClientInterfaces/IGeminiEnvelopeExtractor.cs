using System.Text.Json;

namespace GeminiTelegramBot.Domain.ClientInterfaces
{
    public interface IGeminiEnvelopeExtractor
    {
        string Extract(JsonElement envelope);
    }
}
