using Telegram.Bot;
using Telegram.Bot.Types;

namespace GeminiTelegramBot.Presentation.Telegram
{
    public class TelegramUpdateHandler
    {
        public TelegramUpdateHandler()
        {
            
        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message == null || update.Message.Text == null)
                return;


        }
    }
}
