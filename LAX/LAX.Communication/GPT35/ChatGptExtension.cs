using OpenAI.Chat;

namespace LAX.Communication.GPT35;

public static class ChatGptExtension 
{
    public static ChatPrompt ToChatPrompt(this string message, Role role = Role.user) => new (role.ToString(), message);
}