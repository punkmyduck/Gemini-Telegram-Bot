namespace GeminiTelegramBot.Application.Services
{
    public interface ILogService
    {
        Task LogInfoAsync(string message);
        Task LogErrorAsync(string message, Exception? ex = null);
    }
}
