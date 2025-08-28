namespace GeminiTelegramBot.Infrastructure.Gemini.GiminiRequest.RequestDTOs
{
    public class GeminiContent
    {
        public string Role { get; set; } = string.Empty;
        public GeminiPart[] Parts { get; set; } = Array.Empty<GeminiPart>();
    }
}
