using AiController.Abstraction.Operation;
using AiController.Transmission.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json.Serialization;

namespace AiController.Client.SignalR;

public class SignalRClientOperator : IEventOperator<string>
{
    [JsonIgnore]
    public IEventOperator<string>.OperationHandler? OnReceiveOperation { get; set; }
    public required string Description { get; set; }
    public required string Identifier { get; init; }

    public readonly HubConnection Hub;

    public SignalRClientOperator(HubConnection hub)
    {
        Hub = hub;
        Hub.On<string>(nameof(InvokeMethod.Receive), s => { OnReceiveOperation?.Invoke(s); });
    }

    public async Task StartAsync() => await Hub.StartAsync();

    public async Task Register() => await Hub.InvokeAsync(nameof(InvokeMethod.Register), this);

    public void Send(string ask)
    {
        Hub.InvokeAsync(nameof(InvokeMethod.Send), ask);
    }
}