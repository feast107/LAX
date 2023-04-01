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
            return Proxy.SendAsync($"This is client：{Identifier} \n" +
                                   $"My request is :{ask}");
        }

        public string? Identifier { get; init; }
        public string? Description { get; set; }

        public override string ToString()
        {
            return Identifier ?? "Anonymous";
        }
    }
}
