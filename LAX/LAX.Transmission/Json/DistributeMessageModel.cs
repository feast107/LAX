using LAX.Abstraction;

namespace LAX.Transmission.Json
{
    public class DistributeMessageModel : IExceptional
    {
        [Description("You will fill in the client name in the json field '[this]'，" +
                     "such as : \"[this]\" :  \"device1\" ,")]
        public string? Client { get; set; }

        [Description("You fill in this field : '[this]' with the content that should be sent to the client, as requested by the client," +
                     "such as : \"[this]\" : \"I can solve your problem\" ,")]
        public object? Reply { get; set; }

        public Exception? Exception { get; set; }
    }
}
