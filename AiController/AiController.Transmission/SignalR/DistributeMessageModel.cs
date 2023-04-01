using System.Text.Json;
using AiController.Abstraction;

namespace AiController.Transmission.SignalR
{
    public class DistributeMessageModel : IExceptional
    {
        [Description("")]
        public string? device { get; set; }
        public object? reply { get; set; }

        public static string Example { get; } = JsonSerializer.Serialize(new DistributeMessageModel
        {
            device = "deviceId",
            reply = "I have something uncertain"
        });

        public Exception? Exception { get; set; }
    }
}
