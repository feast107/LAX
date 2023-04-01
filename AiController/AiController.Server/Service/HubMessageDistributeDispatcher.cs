using AiController.Abstraction.Operation;
using AiController.Operation.Operators;
using AiController.Transmission.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace AiController.Server.Service;

public class HubMessageDistributeDispatcher<THub, TOperator, TDistributeMessageModel>
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
            if (task.Result?.device == null) return;
            Console.WriteLine(task.Result);
            foreach (var pair in GetHubByIdentifier(task.Result.device.Trim()))
            {
                hub.Clients
                    .Client(pair.Key)
                    .SendAsync(nameof(InvokeMethod.Receive), task.Result.reply);
            }
        });
    }
}