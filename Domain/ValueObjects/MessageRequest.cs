namespace GeminiTelegramBot.Domain.ValueObjects
{
    public class MessageRequest
    {
        public MessageRequest(long chatId, string? username, string? message)
        {
            ChatId = chatId;
            Username = username;
            Message = message;
        }

        public long ChatId { get; }
        public string? Username { get; }
        public string? Message { get; }
    }
}
