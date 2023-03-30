using AiController.Abstraction;
using AiController.Abstraction.Communication;
using AiController.Communication.GPT35;
using OpenAI.Chat;
using System.Threading.Tasks;
using AiController.Infrastructure;

namespace AiController.Operation.Operators.Base;

public abstract class Gpt35BasedOperator : IDescriptor 
{
    public abstract string Description { get; set; }
    public required string Identifier { get; init; } = "这是中心服务器:[Server]";

    protected Gpt35BasedOperator(IAsyncCommunicator<ChatPrompt[]> communicator)
    {
        Communicator = communicator;
    }
    protected readonly IAsyncCommunicator<ChatPrompt[]> Communicator;

    protected virtual Task<string> SendAsyncInternal(string ask)
    {
        return Communicator.SendAsync(
            new [] {
                Identifier.ToChatPrompt(Role.system),
                Description.ToChatPrompt(Role.system),
                ask.ToChatPrompt()
            });
    }
}

