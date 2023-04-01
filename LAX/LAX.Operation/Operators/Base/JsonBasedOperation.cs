using System.Text.Json;

namespace LAX.Operation.Operators.Base
{
    public static class JsonBasedOperation
    {
        public static JsonSerializerOptions Options { get; } = new ()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public static string Serialize<TModel>(this TModel model) => JsonSerializer.Serialize<TModel>(model, Options);
        public static TModel? Deserialize<TModel>(this string reply) => JsonSerializer.Deserialize<TModel>(reply, Options);
    }
}
