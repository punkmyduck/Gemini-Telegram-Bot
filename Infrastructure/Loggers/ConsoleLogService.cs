using GeminiTelegramBot.Application.Services;

namespace GeminiTelegramBot.Infrastructure.Loggers
{
    internal class ConsoleLogService : ILogService
    {
        public Task LogErrorAsync(string message, Exception? ex = null)
        {
            if (ex != null)
            {
                Console.WriteLine(message + " " + ex.Message);
            }
            else
            {
                Console.WriteLine(message);
            }
            return Task.CompletedTask;
        }

        public Task LogInfoAsync(string message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
