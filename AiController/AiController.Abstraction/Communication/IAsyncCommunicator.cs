using System.Threading;
using System.Threading.Tasks;

namespace AiController.Abstraction.Communication
{
    public interface IAsyncCommunicator
    {
        Task<string> SendAsync(string message, CancellationToken token = default);
    }
}
