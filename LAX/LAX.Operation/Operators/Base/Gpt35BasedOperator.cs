﻿using System.Threading.Tasks;
using LAX.Abstraction;
using LAX.Abstraction.Communication;
using LAX.Communication.GPT35;
using OpenAI.Chat;

namespace LAX.Operation.Operators.Base;

public abstract class Gpt35BasedOperator : IDescriptor 
{
    public abstract string Description { get; set; }
    public required string Identifier { get; init; } = "You are a savvy central server: [Central]," +
                                                       "Responsible for scheduling client messages";

    protected Gpt35BasedOperator(IAsyncCommunicator<ChatPrompt[]> communicator)
    {
        Communicator = communicator;
    }
    protected readonly IAsyncCommunicator<ChatPrompt[]> Communicator;

    protected virtual Task<string> SendAsyncInternal(string ask)
    {
        var i = Identifier.ToChatPrompt(Role.system);
        var d = Description.ToChatPrompt(Role.system);
        var a = ask.ToChatPrompt();
        return Communicator.SendAsync(
            new [] {
                i,
                d,
               a 
            });
    }
}
