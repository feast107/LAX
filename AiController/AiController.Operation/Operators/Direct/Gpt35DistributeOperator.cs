using AiController.Abstraction.Communication;
using AiController.Communication.GPT35;
using AiController.Operation.Operators.Base;
using OpenAI.Chat;
using System;

namespace AiController.Operation.Operators.Direct
{
    public class Gpt35DistributeOperator : Gpt35BasedOperator<Tuple<string, string>?>
    {
        public Gpt35DistributeOperator(IAsyncCommunicator<ChatPrompt[]> communicator)
        {
            this.communicator = communicator;
        }
        private readonly IAsyncCommunicator<ChatPrompt[]> communicator;

        public override void Send(string ask)
        {
            communicator.SendAsync(new ChatPrompt[] {
                Identifier.ToChatPrompt(Role.system),
                Description.ToChatPrompt(Role.system),
                ask.ToChatPrompt(Role.user)
            }).ContinueWith(r => {
                var ls = r.Result.Split('|');
                OnReceiveOperation?.Invoke(r.Result);
            });
        }
    }
}
