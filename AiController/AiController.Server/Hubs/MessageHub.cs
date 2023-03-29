using AiController.Abstraction.Communication;
using AiController.Communication.GPT35;
using AiController.Server.Service;
using Microsoft.AspNetCore.SignalR;

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
            Service.OnHubConnect(this);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Service.OnHubDisconnect(this);
            return base.OnDisconnectedAsync(exception);
        }
        public async Task Register(string name) =>
            await Clients
            .Caller
            .SendAsync(nameof(Register), 
                Service.OnRegister(this, name));

        public void SendMessage(string message) => Service.OnReceiveMessage(this, message);
    }
}
