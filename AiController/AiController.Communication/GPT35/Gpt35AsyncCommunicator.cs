﻿using OpenAI.Chat;
using System.Text;
using AiController.Abstraction.Communication;

namespace AiController.Communication.GPT35
{
    public class Gpt35AsyncCommunicator : GptBaseCommunicator, IAsyncCommunicator
    {
        /// <summary>
        /// 发送消息
        /// 如果被取消则返回 <see cref="string.Empty"/>
        /// </summary>
        /// <exception cref="AggregateException"></exception>
        /// <param name="message"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<string> SendAsync(string message, CancellationToken token = default)
        {
            return Client.ChatEndpoint.GetCompletionAsync(
                GetChatRequest(new ChatPrompt(nameof(Role.user), message)), token)
                .ContinueWith(response =>
                {
                    if (token.IsCancellationRequested || !response.IsCompletedSuccessfully)
                    {
                        return string.Empty;
                    }
                    return response.Result.Choices
                        .Aggregate(new StringBuilder(),
                            (sb, c) => sb.AppendLine(c.Message.Content))
                        .ToString();
                }, token);
        }
    }
}
