using LAX.Abstraction.Operation;
using LAX.Transmission.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json.Serialization;

namespace LAX.Client.SignalR;

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

    public async Task<bool> StartAsync()
    {
        await Hub.StartAsync();
        if (Hub.State != HubConnectionState.Connected) return false;
        await Register();
        return true;
    }

    private async Task Register() => await Hub.InvokeAsync(nameof(InvokeMethod.Register), this);

    public void Send(string ask)
    {
        Hub.InvokeAsync(nameof(InvokeMethod.Send), ask);
    }
}