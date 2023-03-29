namespace AiController.Abstraction.Conversion
{
    public interface IOperationConverter : IOperationConverter<object> { }

    public interface IOperationConverter<out TOperation>
    {
        string ToMessage(object ask);
        TOperation ToOperation(string reply);
    }
}
