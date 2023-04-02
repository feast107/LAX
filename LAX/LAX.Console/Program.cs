using LAX.Client;
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

        ILAXClient ilaxClient = new LAXSignalRClient("http://localhost:5030/server")
        {
            Identifier = identifier,
            Description = description
        };
        ilaxClient.OnReceiveOperation += s => { System.Console.WriteLine($"\nServer: {s}"); };
        var res = await ilaxClient.StartAsync();
        System.Console.WriteLine(res);
        System.Console.Write("You:");
        var message = System.Console.ReadLine();
        while (message is not null or "q")
        {
            ilaxClient.Send(message);
            System.Console.Write("You:");
            message = System.Console.ReadLine();
        }
        System.Console.WriteLine("quit");
        System.Console.ReadLine();
    }
}