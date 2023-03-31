using AiController.Abstraction.Operation;
using AiController.Transmission.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace AiController.Server.Interface
{
    public interface IHubDispatchService<in THub, in TOperator> 
        where THub : Hub 
        where TOperator : IAsyncOperator<DistributeMessageModel?>, IProxied<IAsyncOperator<DistributeMessageModel?>>
    {
        void OnHubDisconnect(string connectionId);
        bool OnRegister(THub hub, TOperator name);
        Task OnReceiveMessage(THub hub, string message);
    }
}
