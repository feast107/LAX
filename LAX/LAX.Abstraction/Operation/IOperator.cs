namespace LAX.Abstraction.Operation;

public interface IOperator : IOperator<object> { }

public interface IOperator<out TOperation> : IDescriptor
{
    TOperation Send(string ask);
}
