using GeminiTelegramBot.Domain.ValueObjects;

namespace GeminiTelegramBot.Application.Commands
{
    public record HandleUserMessageCommand(MessageRequest Request);
}
