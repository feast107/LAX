using System.Threading.Tasks;

namespace AiController.Abstraction.Operation;

public interface IAsyncOperator<TOperation> : IDescriptor
{
    Task<TOperation> SendAsync(string ask);
}
