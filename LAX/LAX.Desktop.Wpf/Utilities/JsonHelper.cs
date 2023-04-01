using System.Text.Json;

namespace AiController.Desktop.Wpf.Utilities
{
    internal class JsonHelper
    {
        public static JsonSerializerOptions ConfigurationOptions { get; } =
            new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
    }
}
