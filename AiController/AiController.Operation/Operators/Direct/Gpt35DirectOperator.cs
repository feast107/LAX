using AiController.Abstraction.Communication;
using AiController.Operation.Operators.Base;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiController.Operation.Operators.Direct
{
    public class Gpt35DirectOperator : Gpt35BasedOperator<string>
    {
        public Gpt35DirectOperator(IAsyncCommunicator<ChatPrompt[]> communicator) : base(communicator)
        { }

        public override void Send(string ask)
        {
            SendAsync(ask).ContinueWith(r =>
            {
                OnReceiveOperation?.Invoke(r.Result);
            });
        }
    }
}
