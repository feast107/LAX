using Microsoft.AspNetCore.SignalR;

namespace AiController.Server.Service
{
    public interface IHubDispatchService<THub> where THub : Hub
    {
        void OnHubDisconnect(THub hub);
        bool OnRegister(THub hub, string name);
        void OnReceiveMessage(THub hub, string message);
    }
}
