using GeminiTelegramBot.Application.Handlers;
using GeminiTelegramBot.Infrastructure.Gemini;
using GeminiTelegramBot.Infrastructure.Gemini.GiminiRequest.Factories;
using GeminiTelegramBot.Infrastructure.Loggers;
using GeminiTelegramBot.Infrastructure.TelegramBotInfrastructure;
using GeminiTelegramBot.Presentation.Telegram;
using Telegram.Bot;

namespace GeminiTelegramBot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            TelegramBotClient botClient = new TelegramBotClient("");

            StartCommandHandler startCommandHandler = new StartCommandHandler(new ConsoleLogService());

            UserMessageCommandHandler userMessageCommandHandler =
                new UserMessageCommandHandler(
                    new ConsoleLogService(),
                    new GeminiResponseGenerator(
                        new GeminiReponseParser(new GeminiEnvelopeExtractor()),
                        new GeminiRequestJsonFactory(),
                        new GeminiApiClient(new HttpClient())),
                    new TelegramMarkdownMessageFormatter());

            TelegramUpdateHandler telegramUpdateHandler =
                new TelegramUpdateHandler(
                    new ConsoleLogService(),
                    new MessageDispatcher(startCommandHandler, userMessageCommandHandler),
                    new TelegramBotClientAdapter(new ConsoleLogService()));

            var me = await botClient.GetMe();
            botClient.StartReceiving(
                telegramUpdateHandler.HandleUpdateAsync,
                telegramUpdateHandler.HandleErrorAsync,
                cancellationToken: new CancellationTokenSource().Token
                );

            Console.ReadLine();
        }
    }
}
