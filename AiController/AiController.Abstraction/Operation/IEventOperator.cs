namespace AiController.Abstraction.Operation;

public interface IEventOperator<TOperation> : IDescriptor
{
    void Send(string ask);

    delegate void OperationHandler(TOperation operation);
    OperationHandler OnReceiveOperation { get; set; } 
}
