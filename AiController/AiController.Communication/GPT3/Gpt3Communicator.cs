using System.Text;
using OpenAI;
using OpenAI.Chat;

namespace AiController.Communication.GPT3
{
    public class Gpt3Communicator : IStreamCommunicator
    {
        public readonly string ModelName;
        public readonly double Temperature;
        public Gpt3Communicator(
            string apiKey,
            string apiHost,
            string modelName,
            double temperature)
        {
            ModelName = modelName;
            Temperature = temperature;
            Client = new OpenAIClient(
                new OpenAIAuthentication(apiKey),
                new OpenAIClientSettings(apiHost));
        }

        private OpenAIClient Client { get; }

        public Task Send(string message,
            IStreamCommunicator.MessageHandler handler,
            CancellationToken token = default)
        {
            return Client.ChatEndpoint.StreamCompletionAsync(
                new ChatRequest(
                    new[] { new ChatPrompt("user", message) },
                    ModelName,
                    Temperature), response =>
                {
                    if (response == null || token.IsCancellationRequested) return;
                    var s = response.Choices.Aggregate(new StringBuilder(), (sb, c) => sb.Append(c.Delta.Content));
                    handler?.Invoke(s.ToString());
                }, token);
        }
    }
}
