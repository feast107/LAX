namespace AiController.Communication
{
    internal interface IAsyncCommunicator
    {
        Task<string> SendAsync(string message);
    }
}
