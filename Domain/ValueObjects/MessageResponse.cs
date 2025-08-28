namespace GeminiTelegramBot.Domain.ValueObjects
{
    public class MessageResponse
    {
        public MessageResponse(long chatId, int messageId, string message)
        {
            ChatId = chatId;
            MessageId = messageId;
            Message = message;
        }

        public long ChatId { get; }
        public int MessageId { get; }
        public string Message { get; }
    }
}
