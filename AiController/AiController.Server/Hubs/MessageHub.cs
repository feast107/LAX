using AiController.Abstraction.Operation;
using AiController.Server.Interface;
using AiController.Transmission.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace AiController.Server.Hubs
{
    public class MessageHub<TOperator> : Hub
        where TOperator : class, IAsyncOperator<DistributeMessageModel?>, IProxied<IAsyncOperator<DistributeMessageModel?>>, new()
    {
        public MessageHub(IHubDispatchService<MessageHub<TOperator>, TOperator> service)
        {
            Service = service;
        }
        public readonly IHubDispatchService<MessageHub<TOperator>, TOperator> Service;

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Service.OnHubDisconnect(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task Register(TOperator message) =>
            await Clients
            .Caller
            .SendAsync(nameof(Register),
                Service.OnRegister(this, message));

        public async Task Send(string message) => await Service.OnReceiveMessage(this, message);
    }
}
