namespace AiController.Abstraction.Conversion
{
    public interface IOperationConverter : IOperator<object> { }

    public interface IOperator<out TOperation> : IDescriptor
    {
        TOperation Send(object ask);
    }
}
