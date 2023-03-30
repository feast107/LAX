using System.Threading;
using System.Threading.Tasks;

namespace AiController.Abstraction.Communication;

public interface IAsyncCommunicator : IAsyncCommunicator<string> { }
public interface IAsyncCommunicator<TMessage>
{
    Task<string> SendAsync(TMessage message, CancellationToken token = default);
}
