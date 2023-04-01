using LAX.Abstraction.Operation;

namespace LAX.Operation.Operators
{
    public interface IExtensibleAsyncOperator<TOperation> : 
        IAsyncOperator<TOperation> ,
        IExtensible
    {
    }
}
