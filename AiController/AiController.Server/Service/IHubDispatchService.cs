using AiController.Abstraction;
using Microsoft.AspNetCore.SignalR;

namespace AiController.Server.Service
{
    public interface IHubDispatchService<in THub> where THub : Hub
    {
        void OnHubDisconnect(string connectionId);
        bool OnRegister(THub hub, IDescriptor name);
        Task OnReceiveMessage(THub hub, string message);
    }
}
