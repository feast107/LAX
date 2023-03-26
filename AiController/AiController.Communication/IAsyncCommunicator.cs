namespace AiController.Communication
{
    public interface IAsyncCommunicator
    {
        Task<string> SendAsync(string message,CancellationToken token = default);
    }
}
