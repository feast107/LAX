using AiController.Abstraction;
using AiController.Abstraction.Communication;
using AiController.Abstraction.Operation;
using AiController.Operation.Operators.Base;
using AiController.Transmission.SignalR;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AiController.Infrastructure;

namespace AiController.Operation.Operators.Direct
{
    public abstract class Gpt35DistributeBasedOperator :
        Gpt35BasedOperator,
        IExtensibleSemanteme
    {
        protected Gpt35DistributeBasedOperator(IAsyncCommunicator<ChatPrompt[]> communicator) : base(communicator) { }

        public override string Description
        {
            get => $"""
            Now we have {Clients.Count} device/s named [{ Clients.Aggregate(new StringBuilder(), (sb, c) => sb.Append(',' + c.Identifier)).ToString()[1..] }] 。
            A self-description for each device follows：
            [{ Clients.Aggregate(new StringBuilder(), (sb, c) => sb.AppendLine(
                $"{{ name：{c.Identifier},\n description：{c.Description} }},\n"
            )) }]
            Next, the client initiates the request，You must reply strictly in JSON format，for example：{DistributeMessageModel.Example}，
            You must explicitly label the devices referred to below and fill in the JSON fields :'{nameof(DistributeMessageModel.device)}' and this is indispensable,
            Then fill in the content of the reply client according to the client's requirements in the JSON fields :'{nameof(DistributeMessageModel.reply)}' whether you have any doubts about it or not，
            In any case, it is forbidden to reply to content other than JSON format
            """;
            set { }
        }

        protected override Task<string> SendAsyncInternal(string ask)
        {
            return base.SendAsyncInternal($"The next message comes from the client：\n" + ask);
        }

        protected readonly List<IDescriptor> Clients = new();
        public void Add(IDescriptor descriptor) => Clients.With(
            $"new Descriptor {descriptor}",
            $"Descriptor count: {Clients.Count + 1}").Add(descriptor);
        public void Remove(IDescriptor descriptor) => Clients.Remove(descriptor).With($"Descriptor count: {Clients.Count}");
    }


    public class Gpt35DistributeEventOperator : 
        Gpt35DistributeBasedOperator, 
        IEventOperator<DistributeMessageModel?> 
    {
        public Gpt35DistributeEventOperator(IAsyncCommunicator<ChatPrompt[]> communicator) :base(communicator)
        {}

        public IEventOperator<DistributeMessageModel?>.OperationHandler? OnReceiveOperation { get; set; }

        public void Send(string ask)
        {
            SendAsyncInternal(ask).ContinueWith(r =>
            {
                OnReceiveOperation?.Invoke(r.Result.With(r.Result).Deserialize<DistributeMessageModel>());
            });
        }
    }

    public class Gpt35DistributeAsyncOperator : 
        Gpt35DistributeBasedOperator, 
        IAsyncOperator<DistributeMessageModel?>
    {
        public Gpt35DistributeAsyncOperator(IAsyncCommunicator<ChatPrompt[]> communicator) : base(communicator)
        { }

        public Task<DistributeMessageModel?> SendAsync(string ask)
        {
            return base.SendAsyncInternal(ask)
                .ContinueWith(r =>
                    r.Result
                        .With($"ChatGPT Reply : {r.Result}")
                        .Ease()
                        .Deserialize<DistributeMessageModel>());
        }
    }
}
