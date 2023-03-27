using System.Threading;
using System.Threading.Tasks;

namespace AiController.Abstraction.Communication
{
    public interface IStreamCommunicator
    {
        delegate void MessageHandler(string message);
        Task Send(string message, MessageHandler handler, CancellationToken token = default);
    }
}
