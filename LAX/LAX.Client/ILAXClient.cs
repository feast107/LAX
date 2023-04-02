using LAX.Abstraction.Operation;

namespace LAX.Client
{
    public interface ILAXClient : IAsyncDisposable, IEventOperator<string>
    {
        Task<ConnectionStatus> StartAsync();
        public ConnectionStatus State { get; }
        public enum ConnectionStatus
        {
            Connected,
            Disconnected,
            Connecting,
        }
    }
}
