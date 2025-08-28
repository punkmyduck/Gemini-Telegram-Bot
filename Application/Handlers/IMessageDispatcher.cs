using GeminiTelegramBot.Domain.ValueObjects;

namespace GeminiTelegramBot.Application.Handlers
{
    public interface IMessageDispatcher
    {
        Task<MessageResponse> DispatchAsync(MessageRequest request, CancellationToken token);
    }
}