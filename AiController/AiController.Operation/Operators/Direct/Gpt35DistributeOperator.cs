using AiController.Abstraction.Communication;
using AiController.Communication.GPT35;
using AiController.Operation.Operators.Base;
using OpenAI.Chat;
using System;

namespace AiController.Operation.Operators.Direct
{
    public class Gpt35DistributeOperator : Gpt35BasedOperator<Tuple<string, string>?>
    {
        public Gpt35DistributeOperator(IAsyncCommunicator<ChatPrompt[]> communicator) :base(communicator)
        {}

        public override void Send(string ask)
        {
            SendAsync(ask).ContinueWith(r => {
                Console.WriteLine(r);
                var ls = r.Result.Split('|');
                if(ls.Length > 2)
                {
                    OnReceiveOperation?.Invoke(new(ls[0], ls[1]));
                }
            });
        }
    }
}
