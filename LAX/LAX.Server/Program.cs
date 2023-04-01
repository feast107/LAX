using AiController.Communication.GPT35;
using AiController.Server.SignalR;

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

            builder.Services.AddAiControllerSignalR(new Gpt35AsyncCommunicator()
            {
                ApiKey = "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",
                ModelName = "gpt-3.5-turbo",
                Temperature = 0
            });

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
            app.MapAiControllerHub("/server");
            app.Run();
        }
    }
}