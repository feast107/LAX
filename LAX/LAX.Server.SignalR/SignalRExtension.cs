using LAX.Abstraction.Communication;
using LAX.Communication.GPT35;
using LAX.Operation.Operators;
using LAX.Operation.Operators.Direct;
using LAX.Operation.Operators.Indirect;
using LAX.Server.SignalR.Hubs;
using LAX.Server.SignalR.Interfaces;
using LAX.Server.SignalR.Services;
using LAX.Transmission.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Chat;

namespace LAX.Server.SignalR
{
    public static class SignalRExtension
    {
        /// <summary>
        /// Add LAX signalR
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="communicator"></param>
        /// <returns></returns>
        public static IServiceCollection AddLAXSignalR(this IServiceCollection collection, Func<IServiceProvider,Gpt35AsyncCommunicator> communicator)
        {
            collection
                .AddSingleton<IAsyncCommunicator<ChatPrompt[]>>(communicator)
                .AddSingleton(typeof(IExtensibleAsyncOperator<>), typeof(Gpt35DistributeAsyncOperator<>))
                .AddSingleton(typeof(IHubDispatchService<,,>), typeof(HubMessageMultiDistributeDispatcher<,,>))
                .AddSignalR();
            return collection;
        }

        /// <summary>
        /// Add LAX signalR
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="communicator"></param>
        /// <returns></returns>
        public static IServiceCollection AddLAXSignalR(this IServiceCollection collection, Gpt35AsyncCommunicator communicator)
        {
            return collection.AddLAXSignalR(_ => communicator);
        }

        /// <summary>
        /// Map LAX signalR hub
        /// </summary>
        /// <param name="app"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static HubEndpointConventionBuilder MapLAXHub(this IEndpointRouteBuilder app,string pattern)
        {
            return app
                .MapHub<MessageHub<Gpt35ClientOperator<DistributeMessageListModel?>, DistributeMessageListModel?>>(pattern);
        }
    }
}