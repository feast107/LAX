using System.Threading;
using System.Threading.Tasks;

namespace LAX.Abstraction.Communication;

public interface IAsyncCommunicator : IAsyncCommunicator<string> { }
public interface IAsyncCommunicator<in TMessage>
{
    Task<string> SendAsync(TMessage message, CancellationToken token = default);
}
