using AiController.Abstraction.Operation;
using Microsoft.AspNetCore.SignalR;

namespace AiController.Server.Interface
{
    public interface IHubDispatchService<in THub, in TOperator, TMessage> 
        where THub : Hub 
        where TOperator : IAsyncOperator<TMessage>, IProxied<IAsyncOperator<TMessage>>
    {
        void OnHubDisconnect(string connectionId);
        bool OnRegister(THub hub, TOperator name);
        Task OnReceiveMessage(THub hub, string message);
    }
}
