namespace GeminiTelegramBot.Application.Services
{
    public interface ILogService
    {
        void LogInfo(string message);
        void LogError(string message, Exception? ex = null);
    }
}
