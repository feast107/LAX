using LAX.Abstraction.Operation;
using LAX.Transmission.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json.Serialization;

namespace LAX.Client.SignalR;

public class LAXSignalRClient : ILAXClient
{
    [JsonIgnore]
    public IEventOperator<string>.OperationHandler? OnReceiveOperation { get; set; }
    public required string Description { get; set; } 
    public required string Identifier { get; init; }

    public readonly HubConnection Hub;

    public LAXSignalRClient(HubConnection hub)
    {
        Hub = hub;
        Hub.On<string>(nameof(InvokeMethod.Receive), s => { OnReceiveOperation?.Invoke(s); });
    }
    public LAXSignalRClient(string url) : this(new HubConnectionBuilder().WithUrl(url).Build()) { }

    public async Task<ILAXClient.ConnectionStatus> StartAsync()
    {
        if (State is not ILAXClient.ConnectionStatus.Disconnected) return State;
        await Hub.StartAsync();
        if (State == ILAXClient.ConnectionStatus.Connected)
            await Register();
        return State;
    }

    public ILAXClient.ConnectionStatus State => Hub.State switch
    {
        HubConnectionState.Connected => ILAXClient.ConnectionStatus.Connected,
        HubConnectionState.Connecting => ILAXClient.ConnectionStatus.Connecting,
        HubConnectionState.Reconnecting => ILAXClient.ConnectionStatus.Connecting,
        HubConnectionState.Disconnected => ILAXClient.ConnectionStatus.Disconnected,
        _ => throw new NotSupportedException()
    };

    private async Task Register() => await Hub.InvokeAsync(nameof(InvokeMethod.Register), this);

    public void Send(string ask) => Hub.InvokeAsync(nameof(InvokeMethod.Send), ask);

    public ValueTask DisposeAsync() => Hub.DisposeAsync();
}