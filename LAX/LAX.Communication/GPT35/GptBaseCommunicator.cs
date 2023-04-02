using OpenAI;
using OpenAI.Chat;

namespace LAX.Communication.GPT35
{
    public enum Role
    {
        system,
        assistant,
        user,
    }

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

        public ChatRequest GetChatRequest(params ChatPrompt[] prompts) => new ChatRequest(prompts, ModelName, Temperature);
    }

    public static class ChatGptExtension 
    {
        public static ChatPrompt ToChatPrompt(this string message, Role role = Role.user) => new (role.ToString(), message);
    }
}
