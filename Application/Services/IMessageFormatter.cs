namespace GeminiTelegramBot.Application.Services
{
    public interface IMessageFormatter
    {
        string Format(string rawResponse);
    }
}
