using System.Threading.Tasks;

namespace LAX.Abstraction.Operation;

public interface IAsyncOperator<TOperation> : IDescriptor
{
    Task<TOperation> SendAsync(string ask);
}
