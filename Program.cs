using GeminiTelegramBot.Application.Handlers;
using GeminiTelegramBot.Infrastructure.Gemini;
using GeminiTelegramBot.Infrastructure.Gemini.GiminiRequest.Factories;
using GeminiTelegramBot.Infrastructure.Loggers;
using GeminiTelegramBot.Infrastructure.TelegramBotInfrastructure;
using GeminiTelegramBot.Presentation.Telegram;
using Telegram.Bot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GeminiTelegramBot.Infrastructure.Options;
using GeminiTelegramBot.Domain.ClientInterfaces;
using GeminiTelegramBot.Application.Services;
using GeminiTelegramBot.Application.Commands;
using GeminiTelegramBot.Domain.ValueObjects;
using GeminiTelegramBot.Domain.Formatters;
using Microsoft.Extensions.Options;

namespace GeminiTelegramBot
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            services.Configure<GeminiApiOptions>(configuration.GetSection("GeminiApi"));
            services.Configure<TelegramBotOptions>(configuration.GetSection("TelegramBot"));

            services.AddSingleton<ITelegramBotClient>(provider =>
            {
                var opts = provider.GetRequiredService<IOptions<TelegramBotOptions>>().Value;
                return new TelegramBotClient(opts.Token);
            });

            services.AddScoped<IGeminiApiClient, GeminiApiClient>();

            services.AddScoped<ILogService, ConsoleLogService>();
            services.AddScoped<ICommandHandler<StartMessageCommand, MessageResponse>, StartCommandHandler>();
            services.AddScoped<ICommandHandler<UserMessageCommand, MessageResponse>, UserMessageCommandHandler>();
            services.AddScoped<IMessageFormatter, TelegramMarkdownMessageFormatter>();

            services.AddScoped<IGeminiEnvelopeExtractor, GeminiEnvelopeExtractor>();
            services.AddScoped<IGeminiApiClient, GeminiApiClient>();
            services.AddScoped<IGeminiRequestJsonFactory, GeminiRequestJsonFactory>();
            services.AddScoped<IResponseGenerator, GeminiResponseGenerator>();
            services.AddScoped<IGeminiReponseParser, GeminiResponseParser>();
            services.AddScoped<HttpClient>();

            services.AddScoped<IMessageDispatcher, MessageDispatcher>();
            services.AddScoped<ITelegramBotClientAdapter, TelegramBotClientAdapter>();
            services.AddScoped<TelegramUpdateHandler>();

            var provider = services.BuildServiceProvider();

            var telegramUpdateHandler = provider.GetRequiredService<TelegramUpdateHandler>();


            var botClient = provider.GetRequiredService<ITelegramBotClient>();

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
