using AiController.Server.Service;
using Microsoft.AspNetCore.SignalR;
using AiController.Transmission.SignalR;

namespace AiController.Server.Hubs
{
    public class MessageHub : Hub
    {
        public MessageHub(IHubDispatchService<MessageHub> service)
        {
            Service = service;
        }
        public readonly IHubDispatchService<MessageHub> Service;

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Service.OnHubDisconnect(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
        public async Task Register(MessageModel message) =>
            await Clients
            .Caller
            .SendAsync(nameof(Register), 
                Service.OnRegister(this, message));

        public void SendMessage(string message) => Service.OnReceiveMessage(this, message);
    }
}
