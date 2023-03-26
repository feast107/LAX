namespace AiController.Communication
{
    public interface IStreamCommunicator
    {
        delegate void MessageHandler(string message);
        Task Send(string message, MessageHandler handler, CancellationToken token = default);
    }
}
