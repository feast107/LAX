using AiController.Abstraction.Operation;

namespace AiController.Operation.Operators
{
    public interface IExtensibleAsyncOperator<TOperation> : 
        IAsyncOperator<TOperation> ,
        IExtensible
    {
    }
}
