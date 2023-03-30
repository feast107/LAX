namespace AiController.Abstraction.Conversion
{
    public interface IOperator : IOperator<object> { }

    public interface IOperator<out TOperation> : IDescriptor
    {
        TOperation Send(object ask);
    }
}
