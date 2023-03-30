using AiController.Abstraction.Conversion;
using AiController.Abstraction.Operation;
using System;

namespace AiController.Conversion.Converters.Base
{
    public abstract class Gpt35BasedOperator<TOperation> : IEventOperator<TOperation>
    {
        public object Context { get; set; }
        public abstract IEventOperator<TOperation>.OperationHandler OnReceiveOperation { get; set; }

        public abstract void Send(string ask);

        public string ToMessage(object ask)
        {
            throw new NotImplementedException();
        }

        public string ToMessage(object subject, object ask)
        {
            throw new NotImplementedException();
        }

        public abstract TOperation ToOperation(string reply);
    }
}
