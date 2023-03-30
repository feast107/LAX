using System.Text.Json;

namespace AiController.Transmission.SignalR
{
    public class DistributeMessageModel
    {
        public string? device { get; set; }
        public string? reply { get; set; }

        public static string Example { get; } = JsonSerializer.Serialize(new DistributeMessageModel()
        {
            device = "deviceId",
            reply = "this is the reply of message"
        });
    }
}
