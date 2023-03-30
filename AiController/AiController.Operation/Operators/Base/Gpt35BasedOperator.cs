using AiController.Abstraction;
using AiController.Abstraction.Communication;
using AiController.Communication.GPT35;
using OpenAI.Chat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiController.Operation.Operators.Base;

public abstract class Gpt35BasedOperator : IDescriptor 
{
    public string Description { get; set; } = string.Empty;
    public required string Identifier { get; init; }

    public readonly List<IDescriptor> Clients = new ();

    protected Gpt35BasedOperator(IAsyncCommunicator<ChatPrompt[]> communicator)
    {
        Communicator = communicator;
    }
    protected readonly IAsyncCommunicator<ChatPrompt[]> Communicator;

    protected virtual Task<string> SendAsyncInternal(string ask)
    {
        return Communicator.SendAsync(
            new ChatPrompt[] {
                Identifier.ToChatPrompt(Role.system),
                Description.ToChatPrompt(Role.system),
                ask.ToChatPrompt(Role.user)
            });
    }
}
