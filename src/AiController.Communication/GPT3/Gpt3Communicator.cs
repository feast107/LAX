using System.Text;
using OpenAI;
using OpenAI.Chat;

namespace AiController.Communication.GPT3
{
    public class Gpt3Communicator : IAsyncCommunicator
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

        public TimeSpan Timeout { get; init; }

        private OpenAIClient Client { get; } 
        public Task<string> SendAsync(string message)
        {
            TaskCompletionSource<string> source = new();
            var expire = new CancellationTokenSource();
            var complete = new CancellationTokenSource();
            Task.Run(async() =>
            {
                await Task.Delay(Timeout, expire.Token);
                if(expire.IsCancellationRequested)return;
                complete.Cancel();
                source.SetException(new TimeoutException($"Wait time expired {Timeout.TotalSeconds}s"));
            }, expire.Token);
            Client.ChatEndpoint.StreamCompletionAsync(
                new ChatRequest(
                    new[] { new ChatPrompt("user", message) },
                    ModelName,
                    Temperature), response =>
                {
                    expire.Cancel();
                    if (response == null) return;
                    var content = response.Choices.FirstOrDefault()?.Delta?.Content;
                    if (string.IsNullOrEmpty(content)) return;
                    var sb = new StringBuilder();
                    sb.Append(content);
                    source.SetResult(sb.ToString());
                }, complete.Token);
            return source.Task;
        }
    }
}
