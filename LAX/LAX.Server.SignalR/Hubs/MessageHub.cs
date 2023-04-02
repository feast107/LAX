using LAX.Abstraction.Operation;
using LAX.Server.SignalR.Interfaces;
using LAX.Transmission.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace LAX.Server.SignalR.Hubs
{
    internal class MessageHub<TOperator, TMessage> : Hub
        where TOperator : class, IAsyncOperator<TMessage>, IProxied<IAsyncOperator<TMessage>>, new()
    {
        public MessageHub(IHubDispatchService<MessageHub<TOperator, TMessage>, TOperator, TMessage> service)
        {
            Service = service;
        }
        public readonly IHubDispatchService<MessageHub<TOperator, TMessage>, TOperator, TMessage> Service;

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
            .SendAsync(nameof(InvokeMethod.Register),
                Service.OnRegister(this, message));

        public async Task Send(string message) => await Service.OnReceiveMessage(this, message);
    }
}
