using AiController.Abstraction.Communication;
using AiController.Abstraction.Operation;
using AiController.Communication.GPT35;
using OpenAI.Chat;
using System.Threading.Tasks;

namespace AiController.Operation.Operators.Base;

public abstract class Gpt35BasedOperator<TOperation> : IEventOperator<TOperation>
{
    public IEventOperator<TOperation>.OperationHandler? OnReceiveOperation { get; set; }
    public string Description { get; set; } = string.Empty;
    public required string Identifier { get; init; }
    public abstract void Send(string ask);

    public Gpt35BasedOperator(IAsyncCommunicator<ChatPrompt[]> communicator)
    {
        Communicator = communicator;
    }
    protected readonly IAsyncCommunicator<ChatPrompt[]> Communicator;

    protected virtual Task<string> SendAsync(string ask)
    {
        return Communicator.SendAsync(new ChatPrompt[] {
                Identifier.ToChatPrompt(Role.system),
                Description.ToChatPrompt(Role.system),
                ask.ToChatPrompt(Role.user)
            });
    }
}
