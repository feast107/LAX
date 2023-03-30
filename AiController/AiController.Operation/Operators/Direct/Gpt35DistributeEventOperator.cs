using AiController.Abstraction.Communication;
using AiController.Abstraction.Operation;
using AiController.Communication.GPT35;
using AiController.Operation.Operators.Base;
using OpenAI.Chat;
using System;
using System.Threading.Tasks;

namespace AiController.Operation.Operators.Direct
{
    public class Gpt35DistributeEventOperator : Gpt35BasedOperator , IEventOperator<Tuple<string, string>?>
    {
        public Gpt35DistributeEventOperator(IAsyncCommunicator<ChatPrompt[]> communicator) :base(communicator)
        {}

        public IEventOperator<Tuple<string, string>?>.OperationHandler? OnReceiveOperation { get; set; }

        public void Send(string ask)
        {
            SendAsyncInternal(ask).ContinueWith(r => {
                Console.WriteLine(r);
                var ls = r.Result.Split('|');
                if(ls.Length > 2)
                {
                    OnReceiveOperation?.Invoke(new(ls[0], ls[1]));
                }
            });
        }

    }

    public class Gpt35DistributeAsyncOperator : Gpt35BasedOperator, IAsyncOperator<Tuple<string, string>?>
    {
        public Gpt35DistributeAsyncOperator(IAsyncCommunicator<ChatPrompt[]> communicator) : base(communicator)
        { }

        public Task<Tuple<string, string>?> SendAsync(string ask)
        {
            return base.SendAsyncInternal(ask)
                .ContinueWith<Tuple<string, string>?>(r =>
                {
                    Console.WriteLine(r);
                    var ls = r.Result.Split('|');
                    return (ls.Length > 1) ?
                        new(ls[0], ls[1]) :
                        null;
                });
        }
    }
}
