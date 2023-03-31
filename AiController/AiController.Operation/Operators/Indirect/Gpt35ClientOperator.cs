using AiController.Abstraction.Operation;
using AiController.Transmission.SignalR;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AiController.Operation.Operators.Indirect
{
    public class Gpt35ClientOperator : IAsyncOperator<DistributeMessageModel?>
    {
        [JsonIgnore] public IAsyncOperator<DistributeMessageModel?> ServerSender { get; set; }

        public Task<DistributeMessageModel?> SendAsync(string ask)
        {
            return ServerSender.SendAsync($"{Identifier} \n{ask}");
        }

        public  string Identifier { get; init; }
        public  string Description { get; set; }
    }
}
