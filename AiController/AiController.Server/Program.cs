using AiController.Abstraction.Communication;
using AiController.Abstraction.Operation;
using AiController.Communication.GPT35;
using AiController.Operation.Operators.Direct;
using AiController.Operation.Operators.Indirect;
using AiController.Server.Hubs;
using AiController.Server.Service;
using AiController.Transmission.SignalR;
using OpenAI.Chat;

namespace AiController.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddSingleton(typeof(IAsyncCommunicator<ChatPrompt[]>), typeof(Gpt35AsyncCommunicator));
            builder.Services.AddSingleton<IAsyncCommunicator<ChatPrompt[]>>(new Gpt35AsyncCommunicator()
            {
                
            });
            builder.Services.AddSingleton(typeof(Gpt35DistributeAsyncOperator));
            builder.Services.AddSingleton(typeof(IHubDispatchService<,,>), typeof(HubMessageDispatcher<,,>));
            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapHub<MessageHub<Gpt35ClientOperator<DistributeMessageModel?>, DistributeMessageModel?>>("/server");
            app.Run();
        }
    }
}