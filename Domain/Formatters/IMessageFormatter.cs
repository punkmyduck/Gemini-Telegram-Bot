namespace GeminiTelegramBot.Domain.Formatters
{
    public interface IMessageFormatter
    {
        string Format(string rawResponse);
    }
}
