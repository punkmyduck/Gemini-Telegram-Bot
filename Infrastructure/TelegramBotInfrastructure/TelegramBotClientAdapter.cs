using GeminiTelegramBot.Application.Services;
using Telegram.Bot;

namespace GeminiTelegramBot.Infrastructure.TelegramBotInfrastructure
{
    public class TelegramBotClientAdapter : ITelegramBotClientAdapter
    {
        private readonly ILogService _logService;
        private readonly ITelegramBotClient _bot;
        public TelegramBotClientAdapter(
            ILogService logService,
            ITelegramBotClient bot)
        {
            _logService = logService;
            _bot = bot;
        }
        public async Task<int> SendMessageAsync(long chatId, string text, CancellationToken cancellationToken)
        {
            var typingMessage = await _bot.SendMessage(
                chatId: chatId,
                text: text,
                cancellationToken: cancellationToken
            );
            await _logService.LogInfoAsync($"[TelegramBotClientAdapter][SendMessage][{chatId}.{typingMessage.MessageId}] : {text}");
            return typingMessage.MessageId;
        }

        public async Task EditMessageAsync(long chatId, int messageId, string text, CancellationToken cancellationToken)
        {
            try
            {
                await _bot.EditMessageText(
                    chatId: chatId,
                    messageId: messageId,
                    text: text,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.MarkdownV2,
                    cancellationToken: cancellationToken
                );
                await _logService.LogInfoAsync($"[TelegramBotClientAdapter][EditMessage][{chatId}.{messageId}] : {text}");
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync($"[TelegramBotClientAdapter][Exception][EditMessage] : {ex}");

                try
                {
                    await _bot.EditMessageText(
                        chatId: chatId,
                        messageId: messageId,
                        text: $"Произошла ошибка при выполнении запроса!",
                        cancellationToken: cancellationToken
                    );
                }
                catch (Exception innerException)
                {
                    await _logService.LogErrorAsync($"[TelegramBotClientAdapter][Exception][EditMessage] : {innerException}");
                }
            }
        }
    }
}
