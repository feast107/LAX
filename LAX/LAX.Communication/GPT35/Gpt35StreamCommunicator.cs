using System.Text;
using LAX.Abstraction.Communication;
using OpenAI.Chat;

namespace LAX.Communication.GPT35
{
    public class Gpt35StreamCommunicator : GptBaseCommunicator, IStreamCommunicator<ChatPrompt[]>
    {
        public Task Send(ChatPrompt[] message,
            IStreamCommunicator<ChatPrompt[]>.MessageHandler handler,
            CancellationToken token = default)
        {
            return Client.ChatEndpoint.StreamCompletionAsync(
               GetChatRequest(message),
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
