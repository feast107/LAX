using LAX.Abstraction.Operation;
using LAX.Operation.Operators;
using LAX.Transmission.Json;
using Microsoft.AspNetCore.SignalR;
using InvokeMethod = LAX.Transmission.SignalR.InvokeMethod;

namespace LAX.Server.SignalR.Services;

internal class HubMessageDistributeDispatcher<THub, TOperator, TDistributeMessageModel>
    : HubMessageBaseDispatcher<THub, TOperator, TDistributeMessageModel>
    where THub : Hub
    where TOperator : IAsyncOperator<TDistributeMessageModel>, IProxied<IAsyncOperator<TDistributeMessageModel>>
    where TDistributeMessageModel : DistributeMessageModel
{
    public HubMessageDistributeDispatcher(IExtensibleAsyncOperator<TDistributeMessageModel> asyncOperator)
        : base(asyncOperator)
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
            if (task.Result?.Device == null) return;
            Console.WriteLine(task.Result);
            foreach (var pair in GetHubsByIdentifier(task.Result.Device.Trim()))
            {
                hub.Clients
                    .Client(pair.Key)
                    .SendAsync(nameof(InvokeMethod.Receive), task.Result.Reply);
            }
        });
    }
}