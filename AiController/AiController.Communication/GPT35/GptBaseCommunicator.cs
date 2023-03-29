using OpenAI;

namespace AiController.Communication.GPT35
{
    public abstract class GptBaseCommunicator
    {
        public required string ModelName { get; init; }
        public required double Temperature { get; init; }
        public required string ApiKey { get; init; }
        public string? ApiHost { get; init; }
        protected OpenAIClient Client => client ??= new(
            new(ApiKey),
            ApiHost != null ? new(ApiHost) : null);
        private OpenAIClient? client;
    }
}
