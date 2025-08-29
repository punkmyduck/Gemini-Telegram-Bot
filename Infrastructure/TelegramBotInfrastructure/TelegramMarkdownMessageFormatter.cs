using System.Text.RegularExpressions;
using GeminiTelegramBot.Application.Services;

namespace GeminiTelegramBot.Infrastructure.TelegramBotInfrastructure
{
    public class TelegramMarkdownMessageFormatter : IMessageFormatter
    {
        public string Format(string rawResponse)
        {
            if (string.IsNullOrWhiteSpace(rawResponse))
                return string.Empty;

            var charsToEscape = new[] { "_", "[", "]", "(", ")", "~", "`", ">", "#", "+", "-", "=", "|", "{", "}", ".", "!" };
            foreach (var c in charsToEscape)
            {
                rawResponse = rawResponse.Replace(c, "\\" + c);
            }

            rawResponse = Regex.Replace(rawResponse, @"\*\*(.+?)\*\*", @"*$1*");

            rawResponse = Regex.Replace(rawResponse, @"(?<=^|\n)-", @"\-");

            return rawResponse;
        }
    }
}
