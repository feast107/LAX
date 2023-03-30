using AiController.Client.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace AiController.Test;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var connection = new HubConnectionBuilder().WithUrl("http://localhost:5030/server").Build();
        var client = new SignalRClientOperator(connection)
        {
            Identifier = "Client1",
            Description = "This is Windows operating system Client1"
        };
        var task = client.StartAsync();
        task.Wait();
        if (task.IsCompletedSuccessfully)
        {
            Console.WriteLine("SUCCESS");
        }
        else
        {
            Console.WriteLine("FAILED");
        }
        client.Register().Wait();
        Console.ReadLine();
    }
}
/*var r = new SignalRClientOperator(connection)
{
    Identifier = "Client1",
    Description = "This is Windows operating system 'Client1'",
};*/