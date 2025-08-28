namespace GeminiTelegramBot.Infrastructure.Gemini.GiminiRequest.RequestDTOs
{
    public class GeminiRequestBody
    {
        public GeminiContent[] Contents { get; set; } = Array.Empty<GeminiContent>();
        public GeminiInstruction SystemInstruction { get; set; } = new();
        public GeminiGenerationConfig GenerationConfig { get; set; } = new();
    }
}
