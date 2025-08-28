using GeminiTelegramBot.Application.Commands;
using GeminiTelegramBot.Application.Services;
using GeminiTelegramBot.Domain.ValueObjects;

namespace GeminiTelegramBot.Application.Handlers
{
    public class StartCommandHandler : ICommandHandler<StartMessageCommand, MessageResponse>
    {
        private readonly ILogService _logService;
        public StartCommandHandler(ILogService logService)
        {
            _logService = logService;
        }
        public async Task<MessageResponse> HandleAsync(StartMessageCommand command, CancellationToken token)
        {
            await _logService.LogInfoAsync($"[UserMessageCommandHandler][Handle] : {command.Request.Message}");
            return new MessageResponse("Можете отправить свой запрос");
        }
    }
}
