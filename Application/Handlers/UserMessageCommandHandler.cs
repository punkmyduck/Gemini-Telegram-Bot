using GeminiTelegramBot.Application.Commands;
using GeminiTelegramBot.Application.Services;
using GeminiTelegramBot.Domain.ClientInterfaces;
using GeminiTelegramBot.Domain.ValueObjects;

namespace GeminiTelegramBot.Application.Handlers
{
    public class UserMessageCommandHandler : ICommandHandler<UserMessageCommand, MessageResponse>
    {
        private readonly ILogService _logService;
        private readonly IResponseGenerator _responseGenerator;
        private readonly IMessageFormatter _messageFormatter;
        public UserMessageCommandHandler(
            ILogService logService,
            IResponseGenerator responseGenerator,
            IMessageFormatter messageFormatter)
        {
            _logService = logService;
            _responseGenerator = responseGenerator;
            _messageFormatter = messageFormatter;
        }
        public async Task<MessageResponse> HandleAsync(UserMessageCommand command, CancellationToken token)
        {
            await _logService.LogInfoAsync($"[UserMessageCommandHandler][Handle] : {command.Request.Message}");

            await _logService.LogInfoAsync($"[UserMessageCommandHandler][Handle] : Generating response...");
            var response = await _responseGenerator.GenerateAsync(command.Request.Message, token);

            await _logService.LogInfoAsync($"[UserMessageCommandHandler][Handle] : Formating response...");
            var result = _messageFormatter.Format(response);
            return new MessageResponse(result);
        }
    }
}
