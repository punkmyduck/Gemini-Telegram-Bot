namespace GeminiTelegramBot.Infrastructure.Gemini.GiminiRequest.RequestDTOs
{
    public class GeminiGenerationConfig
    {
        public double Temperature { get; set; }
        public double TopP { get; set; }
        public int TopK { get; set; }
        public int MaxOutputTokens { get; set; }
    }
}
