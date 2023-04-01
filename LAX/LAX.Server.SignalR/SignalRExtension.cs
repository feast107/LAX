using LAX.Abstraction.Communication;
using LAX.Communication.GPT35;
using LAX.Operation.Operators;
using LAX.Operation.Operators.Direct;
using LAX.Operation.Operators.Indirect;
using LAX.Server.SignalR.Hubs;
using LAX.Server.SignalR.Interface;
using LAX.Server.SignalR.Service;
using LAX.Transmission.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Chat;

namespace LAX.Server.SignalR
{
    public static class SignalRExtension
    {
        /// <summary>
        /// Add AiController signalR
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="communicator"></param>
        /// <returns></returns>
        public static IServiceCollection AddAiControllerSignalR(this IServiceCollection collection, Gpt35AsyncCommunicator communicator )
        {
            collection
                .AddSingleton<IAsyncCommunicator<ChatPrompt[]>>(communicator)
                .AddSingleton(typeof(IExtensibleAsyncOperator<>), typeof(Gpt35DistributeAsyncOperator<>))
                .AddSingleton(typeof(IHubDispatchService<,,>), typeof(HubMessageDistributeDispatcher<,,>))
                .AddSignalR();
            return collection;
        }

        /// <summary>
        /// Map AiController signalR hub
        /// </summary>
        /// <param name="app"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static HubEndpointConventionBuilder MapAiControllerHub(this IEndpointRouteBuilder app,string pattern)
        {
            return app
                .MapHub<MessageHub<Gpt35ClientOperator<DistributeMessageModel?>, DistributeMessageModel?>>(pattern);
        }
    }
}