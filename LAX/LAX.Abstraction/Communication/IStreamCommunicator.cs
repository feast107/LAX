using System.Threading;
using System.Threading.Tasks;

namespace LAX.Abstraction.Communication;

public interface IStreamCommunicator : IStreamCommunicator<string> { }
public interface IStreamCommunicator<TMessage>
{
    delegate void MessageHandler(string message);
    Task Send(TMessage message, MessageHandler handler, CancellationToken token = default);
}
