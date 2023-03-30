using AiController.Abstraction.Conversion;
using AiController.Abstraction.Operation;
using System;

namespace AiController.Conversion.Converters.Base
{
    public abstract class Gpt35BasedOperator<TOperation> : IEventOperator<TOperation>
    {
        public abstract IEventOperator<TOperation>.OperationHandler OnReceiveOperation { get; set; }
        public object Description { get; set; } = string.Empty;
        public required object Subject { get; init; }

        public abstract void Send(object ask);
    }
}
