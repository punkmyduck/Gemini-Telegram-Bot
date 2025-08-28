namespace GeminiTelegramBot.Domain.ValueObjects
{
    public class MessageRequest
    {
        public MessageRequest(string message, long chatId, string? username)
        {
            Message = message;
            ChatId = chatId;
            Username = username;
        }
        public long ChatId { get; }
        public string? Username { get; }
        public string Message { get; }
    }
}
