namespace GeminiTelegramBot.Domain.ClientInterfaces
{
    public interface IResponseGenerator
    {
        Task<string> GenerateAsync(string input, CancellationToken token);
    }
}
