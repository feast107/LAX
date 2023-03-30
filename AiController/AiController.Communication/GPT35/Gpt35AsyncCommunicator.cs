using OpenAI.Chat;
using System.Text;
using AiController.Abstraction.Communication;
using AiController.Infrastructure;

namespace AiController.Communication.GPT35
{
    public class Gpt35AsyncCommunicator : GptBaseCommunicator, IAsyncCommunicator<ChatPrompt[]>
    {
        /// <summary>
        /// 发送消息
        /// 如果被取消则返回 <see cref="string.Empty"/>
        /// </summary>
        /// <exception cref="AggregateException"></exception>
        /// <param name="message"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<string> SendAsync(ChatPrompt[] message, CancellationToken token = default)
        {
            return Client.ChatEndpoint.GetCompletionAsync(
                GetChatRequest(message), token)
                .ContinueWith(response =>
                {
                    if (token.IsCancellationRequested || !response.IsCompletedSuccessfully)
                    {
                        return string.Empty.With(
                            response.Exception != null ? 
                            $"ChatGPT异常: {response.Exception}" : "");
                    }
                    return response.Result.Choices
                        .Aggregate(new StringBuilder(),
                            (sb, c) => sb.AppendLine(c.Message.Content))
                        .ToString();
                }, token);
        }
    }
}
