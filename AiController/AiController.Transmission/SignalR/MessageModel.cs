using AiController.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
    }
}
