using System.Text.Json;
using AiController.Abstraction;

namespace AiController.Transmission.SignalR
{
    public class DistributeMessageModel 
    {
        [Description("")]
        public string? device { get; set; }
        public string? reply { get; set; }

        public static string Example { get; } = JsonSerializer.Serialize(new DistributeMessageModel()
        {
            device = "deviceId",
            reply = "this is the reply of message"
        });
    }
}
