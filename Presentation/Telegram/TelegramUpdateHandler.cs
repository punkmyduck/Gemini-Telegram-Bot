using GeminiTelegramBot.Application.Handlers;
using GeminiTelegramBot.Application.Services;
using GeminiTelegramBot.Domain.ValueObjects;
using GeminiTelegramBot.Infrastructure.TelegramBotInfrastructure;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GeminiTelegramBot.Presentation.Telegram
{
    public class TelegramUpdateHandler
    {
        private readonly ILogService _logService;
        private readonly IMessageDispatcher _messageDispatcher;
        private readonly ITelegramBotClientAdapter _telegramBotClientAdapter;
        public TelegramUpdateHandler(
            ILogService logService, 
            IMessageDispatcher messageDispatcher,
            ITelegramBotClientAdapter telegramBotClientAdapter)
        {
            _logService = logService;
            _messageDispatcher = messageDispatcher;
            _telegramBotClientAdapter = telegramBotClientAdapter;
        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message == null || update.Message.Text == null)
                return;

            await _logService.LogInfoAsync($"[TelegramUpdateHandler][HandleUpdate][{update.Message.Chat.Id}][{update.Message.Chat.Username}] : {update.Message.Text}");

            var messageId = await _telegramBotClientAdapter.SendMessageAsync(update.Message.Chat.Id, "⏳ Подождите, формирую ответ...", cancellationToken);

            var request = new MessageRequest(update.Message.Text, update.Message.Chat.Id, update.Message.Chat.Username);
            var response = await _messageDispatcher.DispatchAsync(request, cancellationToken);

            await _telegramBotClientAdapter.EditMessageAsync(update.Message.Chat.Id, messageId, response.Message, cancellationToken);
        }
        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            await _logService.LogErrorAsync("Telegram Bot Error : ", exception);
        }
    }
}
