using AiController.Abstraction.Operation;

namespace AiController.Operation.Operators.Base;

public abstract class Gpt35BasedOperator<TOperation> : IEventOperator<TOperation>
{
    public IEventOperator<TOperation>.OperationHandler? OnReceiveOperation { get; set; }
    public string Description { get; set; } = string.Empty;
    public required string Identifier { get; init; }
    public abstract void Send(object ask);
}
