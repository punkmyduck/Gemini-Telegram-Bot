using System.Text.Json;
using GeminiTelegramBot.Infrastructure.Gemini.GiminiRequest.RequestDTOs;

namespace GeminiTelegramBot.Infrastructure.Gemini.GiminiRequest.Factories
{
    public class GeminiRequestJsonFactory : IGeminiRequestJsonFactory
    {
        public GeminiRequestJsonDto CreateJson(string prompt)
        {
            GeminiRequestBody requestBody = new GeminiRequestBody
            {
                Contents = new[]
                {
                    new GeminiContent
                    {
                        Role = "user",
                        Parts = new GeminiPart[]
                        {
                            new GeminiPart { Text = prompt }
                        }
                    }
                },
                SystemInstruction = new GeminiInstruction
                {
                    Role = "system",
                    Parts = new GeminiPart[]
                    {
                        new GeminiPart { Text = "Отвечай кратко и по существу. Не развертывай длинные объяснения, если не было просьбы об обратном." }
                    }
                },
                GenerationConfig = new GeminiGenerationConfig
                {
                    Temperature = 0.7,
                    TopP = 0.95,
                    TopK = 40,
                    MaxOutputTokens = 1000
                }
            };

            var json = JsonSerializer.Serialize(requestBody,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

            return new GeminiRequestJsonDto(json);
        }
    }
}
