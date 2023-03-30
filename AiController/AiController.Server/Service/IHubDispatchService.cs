using AiController.Abstraction;
using Microsoft.AspNetCore.SignalR;

namespace AiController.Server.Service
{
    public interface IHubDispatchService<THub> where THub : Hub
    {
        void OnHubDisconnect(string connectionId);
        bool OnRegister(THub hub, IDescriptor name);
        void OnReceiveMessage(THub hub, string message);
    }
}
