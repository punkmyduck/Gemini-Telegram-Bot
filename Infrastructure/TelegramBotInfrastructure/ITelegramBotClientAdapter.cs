using Telegram.Bot;

namespace GeminiTelegramBot.Infrastructure.TelegramBotInfrastructure
{
    public interface ITelegramBotClientAdapter
    {
        Task EditMessageAsync(long chatId, int messageId, string text, CancellationToken cancellationToken);
        Task<int> SendMessageAsync(long chatId, string text, CancellationToken cancellationToken);
    }
}