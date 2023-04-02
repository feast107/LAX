using LAX.Abstraction;

namespace LAX.Transmission.Json
{
    public class DistributeMessageModel : IExceptional
    {
        [Description("The client that will need to receive this message will be filled in the json field '[this]'，" +
                     "such as : \"[this]\" :  \"device1\" ,")]
        public string? Device { get; set; }

        [Description("Please fill in the json field '[this]' with the message content that the client needs to receive according to the client's requirements, " +
                     "such as : \"[this]\" : \"I can solve your problem\" ,")]
        public object? Reply { get; set; }

        public Exception? Exception { get; set; }
    }
}
