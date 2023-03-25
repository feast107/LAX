namespace AiController.Communication
{
    internal interface ICommunicateListener
    {
        void Send(string message);

        delegate void MessageHandler(string message);

        event MessageHandler OnReceiveMessage;
    }
}
