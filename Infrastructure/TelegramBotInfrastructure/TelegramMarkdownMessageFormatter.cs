using GeminiTelegramBot.Application.Services;

namespace GeminiTelegramBot.Infrastructure.TelegramBotInfrastructure
{
    internal class TelegramMarkdownMessageFormatter : IMessageFormatter
    {
        public string Format(string rawResponse)
        {
            rawResponse = System.Text.RegularExpressions.Regex.Replace(rawResponse, @"\*\*(.+?)\*\*", @"*$1*");

            rawResponse = System.Text.RegularExpressions.Regex.Replace(rawResponse, @"^\*+\s+", "- ", System.Text.RegularExpressions.RegexOptions.Multiline);

            var charsToEscape = new[] { "_", "[", "]", "(", ")", "~", "`", ">", "#", "+", "=", "|", "{", "}", ".", "!", "-" };
            foreach (var c in charsToEscape)
            {
                rawResponse = rawResponse.Replace(c, "\\" + c);
            }

            return rawResponse;
        }
    }
}
