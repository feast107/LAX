using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI.Chat;

namespace AiController.Communication.GPT35
{
    public class Gpt35AsyncCommunicator : GptBaseCommunicator, IAsyncCommunicator
    {
        public Task<string> SendAsync(string message, CancellationToken token = default)
        {
            return Client.ChatEndpoint.GetCompletionAsync(new(
                    new[] { new ChatPrompt("user", message) },
                    ModelName,
                    Temperature), token)
                .ContinueWith(response =>
                {
                    if (token.IsCancellationRequested || !response.IsCompletedSuccessfully)
                        return string.Empty;
                    return response.Result.Choices
                        .Aggregate(new StringBuilder(),
                            (sb, c) => sb.AppendLine(c.Delta.Content))
                        .ToString();
                }, token);
        }
    }
}
