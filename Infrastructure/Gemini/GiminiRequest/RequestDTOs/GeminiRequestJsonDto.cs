namespace GeminiTelegramBot.Infrastructure.Gemini.GiminiRequest.RequestDTOs
{
    public class GeminiRequestJsonDto
    {
        public GeminiRequestJsonDto(string json)
        {
            Json = json;
        }
        public string Json { get; set; }
    }
}
