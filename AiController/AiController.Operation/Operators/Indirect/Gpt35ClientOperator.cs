using AiController.Abstraction.Operation;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AiController.Operation.Operators.Indirect
{
    public class Gpt35ClientOperator<TMessage>
        : IAsyncOperator<TMessage>, IProxied<IAsyncOperator<TMessage>>
    {
        [JsonIgnore] public IAsyncOperator<TMessage> Proxy { get; set; } = null!;

        public Task<TMessage> SendAsync(string ask)
        {
            return Proxy.SendAsync($"{Identifier} \n{ask}");
        }

        public string? Identifier { get; init; }
        public string? Description { get; set; }
    }
}
