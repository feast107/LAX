using AiController.Client.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace AiController.Test;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var connection = new HubConnectionBuilder().WithUrl("http://localhost:5030/server").Build();
        Console.Write("Your Identifier :");
        var identifier = Console.ReadLine() ?? "Client";

        Console.Write("Your Description :");
        var description = Console.ReadLine() ?? "This is Windows operating system Client";

        var client = new SignalRClientOperator(connection)
        {
            Identifier = identifier,
            Description = description
        };
        client.OnReceiveOperation += (s) => { Console.WriteLine($"\nServer: {s}"); };
        var task = client.StartAsync();
        task.Wait();
        Console.WriteLine(task.IsCompletedSuccessfully ? "Connect success" : "Connect failed");
        client.Register().Wait();
        Console.Write("You:");
        var message = Console.ReadLine();
        while (message is not null or "q")
        {
            client.Send(message);
            Console.Write("You:");
            message = Console.ReadLine();
        }
        Console.WriteLine("quit");
        Console.ReadLine();
    }
}
/*var r = new SignalRClientOperator(connection)
{
    Identifier = "Client1",
    Description = "This is Windows operating system 'Client1'",
};*/