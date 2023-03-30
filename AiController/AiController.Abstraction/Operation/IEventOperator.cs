namespace AiController.Abstraction.Operation
{
    public interface IEventOperator<TOperation>
    {
        void Send(object ask);

        delegate void OperationHandler(TOperation operation);
        OperationHandler OnReceiveOperation { get; set; } 
    }
}
