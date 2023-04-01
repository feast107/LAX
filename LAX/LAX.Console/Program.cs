using LAX.Client.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace LAX.Console;

public class Program
{
    public static async Task Main(string[] args)
    {
        System.Console.WriteLine("Hello, Gpt!");

        var connection = new HubConnectionBuilder().WithUrl("http://localhost:5030/server").Build();
        AppDomain.CurrentDomain.ProcessExit += async (o, e) => { await connection.DisposeAsync(); };

        System.Console.Write("Your Identifier :");
        var identifier = System.Console.ReadLine() ?? "Client";

        System.Console.Write("Your Description :");
        var description = System.Console.ReadLine() ?? "This is Windows operating system Client";

        var client = new SignalRClientOperator(connection)
        {
            Identifier = identifier,
            Description = description
        };
        client.OnReceiveOperation += s => { System.Console.WriteLine($"\nServer: {s}"); };
        var task = client.StartAsync();
        await task;
        System.Console.WriteLine(task.IsCompletedSuccessfully ? "Connect success" : "Connect failed");
        await client.Register();
        System.Console.Write("You:");
        var message = System.Console.ReadLine();
        while (message is not null or "q")
        {
            client.Send(message);
            System.Console.Write("You:");
            message = System.Console.ReadLine();
        }
        System.Console.WriteLine("quit");
        System.Console.ReadLine();
    }
}