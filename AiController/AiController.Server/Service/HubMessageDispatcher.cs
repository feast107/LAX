using AiController.Abstraction;
using AiController.Operation.Operators.Direct;
using AiController.Server.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace AiController.Server.Service
{
    public class HubMessageDispatcher<THub> : IHubDispatchService<THub> where THub : Hub
    {
        public HubMessageDispatcher(Gpt35DistributeAsyncOperator asyncOperator)
        {
            this.asyncOperator = asyncOperator;
        }

        private readonly Dictionary<string, Tuple<IDescriptor, THub>> ConnectedHubs = new();

        private readonly Gpt35DistributeAsyncOperator asyncOperator;

        private Task<Tuple<string, string>?>? CurrentRequest;

        public void OnHubDisconnect(string connectionId)
        {
            ConnectedHubs.Remove(connectionId);
        }

        public void OnReceiveMessage(THub hub, string message)
        {
            if (CurrentRequest == null)
            {
                CurrentRequest = asyncOperator.SendAsync(message);
            }
            else
            {
                CurrentRequest = CurrentRequest.ContinueWith(t => asyncOperator.SendAsync(message).Result);
            }
            CurrentRequest.ContinueWith(task =>
                {
                    if (task.Result == null) return;
                    if (ConnectedHubs.TryGetValue(task.Result.Item1, out var tuple))
                    {
                        tuple.Item2.Clients.Caller.SendAsync(task.Result.Item2);
                    }
                });
        }

        public bool OnRegister(THub hub, IDescriptor identifier) =>
            ConnectedHubs.TryAdd(hub.Context.ConnectionId, new(identifier, hub));
    }
}
