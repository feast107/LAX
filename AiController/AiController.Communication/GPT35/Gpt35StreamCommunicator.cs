using System.Text;
using AiController.Abstraction.Communication;
using OpenAI.Chat;

namespace AiController.Communication.GPT35
{
    public class Gpt35StreamCommunicator : GptBaseCommunicator, IStreamCommunicator
    {
        public Task Send(string message,
            IStreamCommunicator.MessageHandler handler,
            CancellationToken token = default)
        {
            return Client.ChatEndpoint.StreamCompletionAsync(
               GetChatRequest(new ChatPrompt(nameof(Role.user), message)),
               response =>
                {
                    if (response == null || token.IsCancellationRequested) return;
                    handler?
                        .Invoke(response.Choices
                        .Aggregate(new StringBuilder(),
                            (sb, c) => sb.AppendLine(c.Delta.Content))
                        .ToString());
                }, token);
        }
    }
}
