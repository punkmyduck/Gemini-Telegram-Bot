using GeminiTelegramBot.Domain.Formatters;

namespace GeminiTelegramBot.Infrastructure.TelegramBotInfrastructure
{
    internal class TelegramMarkdownMessageFormatter : IMessageFormatter
    {
        public string Format(string rawResponse)
        {
            // Жирный: **xxx** → *xxx*
            rawResponse = System.Text.RegularExpressions.Regex.Replace(rawResponse, @"\*\*(.+?)\*\*", @"*$1*");

            // Списки: *   text → - text
            rawResponse = System.Text.RegularExpressions.Regex.Replace(rawResponse, @"^\*+\s+", "- ", System.Text.RegularExpressions.RegexOptions.Multiline);

            // Экранирование спецсимволов MarkdownV2
            var charsToEscape = new[] { "_", "[", "]", "(", ")", "~", "`", ">", "#", "+", "=", "|", "{", "}", ".", "!", "-" };
            foreach (var c in charsToEscape)
            {
                rawResponse = rawResponse.Replace(c, "\\" + c);
            }

            return rawResponse;
        }
    }
}
