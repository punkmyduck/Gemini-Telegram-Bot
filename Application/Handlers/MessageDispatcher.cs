using GeminiTelegramBot.Application.Commands;
using GeminiTelegramBot.Domain.ValueObjects;

namespace GeminiTelegramBot.Application.Handlers
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly ICommandHandler<StartMessageCommand, MessageResponse> _startHandler;
        private readonly ICommandHandler<UserMessageCommand, MessageResponse> _userHandler;

        public MessageDispatcher(
            ICommandHandler<StartMessageCommand, MessageResponse> startHandler,
            ICommandHandler<UserMessageCommand, MessageResponse> userHandler)
        {
            _startHandler = startHandler;
            _userHandler = userHandler;
        }

        public async Task<MessageResponse> DispatchAsync(MessageRequest request, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
                return new MessageResponse("Сообщение не должно быть пустым.");

            if (request.Message.Equals("/start", StringComparison.OrdinalIgnoreCase))
            {
                var command = new StartMessageCommand(request);
                return await _startHandler.HandleAsync(command, token);
            }

            var userCommand = new UserMessageCommand(request);
            return await _userHandler.HandleAsync(userCommand, token);
        }
    }
}
