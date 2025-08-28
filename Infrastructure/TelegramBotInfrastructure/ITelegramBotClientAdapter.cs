using Telegram.Bot;

namespace GeminiTelegramBot.Infrastructure.TelegramBotInfrastructure
{
    public interface ITelegramBotClientAdapter
    {
        Task EditMessageAsync(ITelegramBotClient bot, long chatId, int messageId, string text, CancellationToken cancellationToken);
        Task<int> SendMessageAsync(ITelegramBotClient bot, long chatId, string text, CancellationToken cancellationToken);
    }
}