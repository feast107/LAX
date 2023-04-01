using LAX.Abstraction.Communication;
using LAX.Abstraction.Operation;
using LAX.Operation.Operators.Base;
using OpenAI.Chat;

namespace LAX.Operation.Operators.Direct
{
    public class Gpt35DirectOperator : Gpt35BasedOperator , IEventOperator<string>
    {
        public Gpt35DirectOperator(IAsyncCommunicator<ChatPrompt[]> communicator) : base(communicator)
        { }

        public IEventOperator<string>.OperationHandler? OnReceiveOperation { get; set; }

        public override string Description
        {
            get => throw new System.NotImplementedException(); 
            set => throw new System.NotImplementedException();
        }

        public void Send(string ask)
        {
            SendAsyncInternal(ask).ContinueWith(r =>
            {
                OnReceiveOperation?.Invoke(r.Result);
            });
        }
    }
}
