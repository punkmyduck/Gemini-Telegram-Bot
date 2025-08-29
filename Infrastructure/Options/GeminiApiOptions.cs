namespace GeminiTelegramBot.Infrastructure.Options
{
    public class GeminiApiOptions
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
}
