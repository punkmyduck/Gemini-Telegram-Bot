using GeminiTelegramBot.Application.Services;

namespace GeminiTelegramBot.Infrastructure.Loggers
{
    internal class ConsoleLogService : ILogService
    {
        public void LogError(string message, Exception? ex = null)
        {
            throw new NotImplementedException();
        }

        public void LogInfo(string message)
        {
            throw new NotImplementedException();
        }
    }
}
