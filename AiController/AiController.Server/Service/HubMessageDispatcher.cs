using AiController.Abstraction;
using AiController.Infrastructure;
using AiController.Operation.Operators.Direct;
using AiController.Transmission.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace AiController.Server.Service
{
    public class HubMessageDispatcher<THub> : IHubDispatchService<THub> where THub : Hub
    {
        public HubMessageDispatcher(Gpt35DistributeAsyncOperator asyncOperator)
        {
            this.asyncOperator = asyncOperator;
        }

        private readonly Dictionary<string, Tuple<IDescriptor, THub>> connectedHubs = new();

        private readonly Gpt35DistributeAsyncOperator asyncOperator;

        private Task<DistributeMessageModel?>? currentRequest;

        public void OnHubDisconnect(string connectionId)
        {
            if (!connectedHubs.TryGetValue(connectionId, out var field)) return;
            asyncOperator.Remove(field.Item1);
            connectedHubs.Remove(connectionId);
        }

        public async Task OnReceiveMessage(THub hub, string message)
        {
            var field = connectedHubs[hub.Context.ConnectionId];
            currentRequest = currentRequest == null
                ? asyncOperator.SendAsync($"{field.Item1.Identifier} \n{message}")
                : currentRequest.ContinueWith(_ => asyncOperator.SendAsync(message).Result);
            await currentRequest.ContinueWith(task =>
            {
                if (task.Result?.device == null) return;
                Console.WriteLine(task.Result);
                foreach (var pair in connectedHubs)
                {
                    if (pair.Value.Item1.Identifier.Trim() != task.Result.device.Trim()) return;
                    hub.Clients
                        .Client(pair.Key)
                        .SendAsync(nameof(InvokeMethod.Receive), task.Result.reply);
                }
            });
        }

        public bool OnRegister(THub hub, IDescriptor identifier)
        {
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
