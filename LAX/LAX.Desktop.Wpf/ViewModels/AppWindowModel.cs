using System.ComponentModel;
using AiController.Desktop.Wpf.Models;
using AiController.Desktop.Wpf.Services;

namespace AiController.Desktop.Wpf.ViewModels
{
    public class AppWindowModel : INotifyPropertyChanged
    {
        public AppWindowModel(ConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }

        public ConfigurationService ConfigurationService { get; }

        public AppConfig Configuration => ConfigurationService.Configuration;

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
