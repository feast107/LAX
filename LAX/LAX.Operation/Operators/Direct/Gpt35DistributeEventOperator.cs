using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAX.Abstraction;
using LAX.Abstraction.Communication;
using LAX.Abstraction.Operation;
using LAX.Infrastructure;
using LAX.Operation.Operators.Base;
using LAX.Transmission;
using LAX.Transmission.Json;
using OpenAI.Chat;

namespace LAX.Operation.Operators.Direct
{
    public abstract class Gpt35DistributeBasedOperator :
        Gpt35BasedOperator,
        IExtensible
    {
        protected Gpt35DistributeBasedOperator(IAsyncCommunicator<ChatPrompt[]> communicator) : base(communicator) { }

        public override string Description
        {
            get => Clients.Count == 0
                ? string.Empty
                : $"""
            Now we have {Clients.Count} device/s named [{Clients.Aggregate(new StringBuilder(), (sb, c) => sb.Append(',' + c.Identifier)).ToString()[1..]}] 。
            A self-description for each device follows：
            [{Clients.Aggregate(new StringBuilder(), (sb, c) => sb.AppendLine(
                $"{{ name：{c.Identifier},\n description：{c.Description} }},\n"
            ))}]
            Next, the client initiates the request，You must reply strictly in JSON format，for example：{ExampleHelper<DistributeMessageListModel>.ForExample}，
            In any case, it is forbidden to reply to content other than JSON format,
            If you have any uncertainties, you must also strictly use the JSON format to express them.
            """;
            set { }
        }

        protected readonly List<IDescriptor> Clients = new();

        public void Add(IDescriptor descriptor) =>
            Clients
                .With($"Descriptor [{descriptor}] joined",
                    $"Descriptors : [{Clients.Count + 1}] in total")
                .Add(descriptor);

        public void Remove(IDescriptor descriptor) =>
            Clients
                .Remove(descriptor)
                .With($"Descriptor [{descriptor}] leaved" +
                      $"Descriptors : [{Clients.Count + 1}] in total");
    }



    public class Gpt35DistributeAsyncOperator<TMessage> :
        Gpt35DistributeBasedOperator,
        IExtensibleAsyncOperator<TMessage?>
        where TMessage : IExceptional , new()
    {
        public Gpt35DistributeAsyncOperator(IAsyncCommunicator<ChatPrompt[]> communicator) : base(communicator)
        { }

        public Task<TMessage?> SendAsync(string ask)
        {
            return 
                base.SendAsyncInternal(ask)
                .ContinueWith(r =>
                {
                    var res = r.Result.With($"ChatGPT Reply : {r.Result}");
                    try
                    {
                        var ret = res.Deserialize<TMessage>();
                        return ret;
                    }
                    catch (Exception e)
                    {
                        return new TMessage { Exception = e }.WithError($"ChatGPT Exception : {e}");
                    }
                });
        }
    }

    public class Gpt35DistributeEventOperator :
        Gpt35DistributeBasedOperator,
        IEventOperator<DistributeMessageModel?>
    {
        public Gpt35DistributeEventOperator(IAsyncCommunicator<ChatPrompt[]> communicator) : base(communicator)
        { }

        public IEventOperator<DistributeMessageModel?>.OperationHandler? OnReceiveOperation { get; set; }

        public void Send(string ask)
        {
            SendAsyncInternal(ask).ContinueWith(r =>
            {
                OnReceiveOperation?.Invoke(r.Result.With(r.Result).Deserialize<DistributeMessageModel>());
            });
        }
    }

}
