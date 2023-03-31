using AiController.Abstraction.Operation;
using AiController.Infrastructure;
using AiController.Operation.Operators;
using AiController.Server.Interface;
using AiController.Transmission.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace AiController.Server.Service
{
    public class HubMessageDispatcher<THub, TOperator>
        : IHubDispatchService<THub, TOperator>
        where THub : Hub
        where TOperator : IAsyncOperator<DistributeMessageModel?>, IProxied<IAsyncOperator<DistributeMessageModel?>> 
    {
        public HubMessageDispatcher(IExtensibleAsyncOperator<DistributeMessageModel?> asyncOperator)
        {
            this.asyncOperator = asyncOperator;
        }


        private readonly Dictionary<string, Tuple<TOperator, THub>> connectedHubs = new();

        private readonly IExtensibleAsyncOperator<DistributeMessageModel?> asyncOperator;

        private Task<DistributeMessageModel?>? currentRequest;


        public void OnHubDisconnect(string connectionId)
        {
            if (!connectedHubs.TryGetValue(connectionId, out var field)) return;
            asyncOperator.Remove(field.Item1);
            connectedHubs.Remove(connectionId);
        }

        public async Task OnReceiveMessage(THub hub, string message)
        {
            if (!connectedHubs.TryGetValue(hub.Context.ConnectionId, out var field)) return;
            currentRequest = currentRequest == null
                ? field.Item1.SendAsync($"{field.Item1.Identifier} \n{message}")
                : currentRequest.ContinueWith(_ => field.Item1.SendAsync(message).Result);
            await currentRequest.ContinueWith(task =>
            {
                if (task.Result?.device == null) return;
                Console.WriteLine(task.Result);
                foreach (var pair in connectedHubs
                             .Where(pair => pair.Value.Item1.Identifier.Trim() == task.Result.device.Trim()))
                {
                    hub.Clients
                        .Client(pair.Key)
                        .SendAsync(nameof(InvokeMethod.Receive), task.Result.reply);
                }
            });
        }

        public bool OnRegister(THub hub, TOperator identifier)
        {
            identifier.Proxy = asyncOperator;
            if (connectedHubs.TryGetValue(hub.Context.ConnectionId, out var field))
            {
                asyncOperator.Remove(field.Item1);
            }
            connectedHubs[hub.Context.ConnectionId] = new(identifier, hub);
            asyncOperator.Add(identifier);
            return true.With(connectedHubs.Count);
        }
    }
}
