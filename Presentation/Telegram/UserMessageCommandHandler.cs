using GeminiTelegramBot.Application.Commands;
using GeminiTelegramBot.Application.Handlers;
using GeminiTelegramBot.Domain.ValueObjects;

namespace GeminiTelegramBot.Presentation.Telegram
{
    public class UserMessageCommandHandler : ICommandHandler<HandleUserMessageCommand, MessageResponse>
    {
        public Task<MessageResponse> Handle(HandleUserMessageCommand command, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
