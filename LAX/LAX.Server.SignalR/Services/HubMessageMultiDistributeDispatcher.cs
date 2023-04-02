using LAX.Abstraction.Operation;
using LAX.Operation.Operators;
using LAX.Transmission.Json;
using LAX.Transmission.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace LAX.Server.SignalR.Services
{
    internal class HubMessageMultiDistributeDispatcher<THub, TOperator, TDistributeMessageModel>
        : HubMessageBaseDispatcher<THub, TOperator, TDistributeMessageModel>
        where THub : Hub
        where TOperator : IAsyncOperator<TDistributeMessageModel>, IProxied<IAsyncOperator<TDistributeMessageModel>>
        where TDistributeMessageModel : DistributeMessageListModel
    {
        public HubMessageMultiDistributeDispatcher(IExtensibleAsyncOperator<TDistributeMessageModel> asyncOperator) : base(asyncOperator)
        {
        }

        public override async Task OnReceiveMessage(THub hub, string message)
        {
            if (!TryGetHubByConnectionId(hub.Context.ConnectionId, out var field)) return;
            await Run(() =>
                field.Item1.SendAsync(message).Result
            ).ContinueWith(task =>
            {
                if (task.Result.Exception != null) return;
                if (task.Result?.ReplyMessages == null) return;
                task.Result.ReplyMessages.ForEach(x =>
                {
                    if (x.Client == null) return;
                    var pair = GetHubByIdentifier(x.Client.Trim());
                    if(pair.HasValue)
                    {
                        hub.Clients
                            .Client(pair.Value.Key)
                            .SendAsync(nameof(InvokeMethod.Receive), x.Reply);
                    }
                });
            });
        }
    }
}
