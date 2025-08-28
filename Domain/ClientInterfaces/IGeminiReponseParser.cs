namespace GeminiTelegramBot.Domain.ClientInterfaces
{
    public interface IGeminiReponseParser
    {
        string ParseGeminiResponse(string responseText);
    }
}
