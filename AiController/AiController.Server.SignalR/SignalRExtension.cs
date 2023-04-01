using AiController.Abstraction.Communication;
using AiController.Communication.GPT35;
using AiController.Operation.Operators;
using AiController.Operation.Operators.Direct;
using AiController.Operation.Operators.Indirect;
using AiController.Server.SignalR.Hubs;
using AiController.Server.SignalR.Interface;
using AiController.Server.SignalR.Service;
using AiController.Transmission.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Chat;

namespace AiController.Server.SignalR
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