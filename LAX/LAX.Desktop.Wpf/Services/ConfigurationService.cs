using System.IO;
using System.Text.Json;
using AiController.Desktop.Wpf.Models;
using AiController.Desktop.Wpf.Utilities;
using Microsoft.Extensions.Options;

namespace AiController.Desktop.Wpf.Services
{
    public class ConfigurationService
    {
        public ConfigurationService(IOptions<AppConfig> configuration)
        {
            OptionalConfiguration = configuration;
        }

        private IOptions<AppConfig> OptionalConfiguration { get; }

        public AppConfig Configuration => OptionalConfiguration.Value;

        public void Save()
        {
            using FileStream fs = File.Create(GlobalValues.JsonConfigurationFilePath);
            JsonSerializer.Serialize(fs, OptionalConfiguration.Value, JsonHelper.ConfigurationOptions);
        }
    }
}
