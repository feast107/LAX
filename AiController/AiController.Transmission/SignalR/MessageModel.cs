using System.Text.Json;
using AiController.Abstraction;

namespace AiController.Transmission.SignalR
{
    [Serializable]
    public class MessageModel : IDescriptor
    {
        public required string Description { get; set; }
        public required string Identifier { get; init; }

        public static MessageModel Transform(IDescriptor another)
        {
            return new MessageModel() { Identifier = another.Identifier, Description = another.Description };
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
