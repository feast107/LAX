using AiController.Abstraction.Communication;
using AiController.Abstraction.Operation;
using Microsoft.AspNetCore.SignalR;

namespace AiController.Server.Service
{
    public class HubMessageDispatcher<THub> : IHubDispatchService<THub> where THub : Hub
    {
        public HubMessageDispatcher(IOperator<Action<Dictionary<string, THub>>> operationConverter)
        {
            this.operationConverter = operationConverter;
        }

        private readonly Dictionary<string, THub> ConnectedHubs = new();

        private readonly IOperator<Action<Dictionary<string, THub>>> operationConverter;

        public void OnHubDisconnect(THub hub)
        {
            foreach(var pair in ConnectedHubs)
            {
                if(pair.Value == hub)
                {
                    ConnectedHubs.Remove(pair.Key);
                }
            }
        }

        public void OnReceiveMessage(THub hub, string message)
        {
           
        }

        public bool OnRegister(THub hub, string name) => ConnectedHubs.TryAdd(name, hub);
    }
}
