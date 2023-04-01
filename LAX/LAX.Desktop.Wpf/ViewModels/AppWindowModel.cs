using System.ComponentModel;
using LAX.Desktop.Wpf.Models;
using LAX.Desktop.Wpf.Services;

namespace LAX.Desktop.Wpf.ViewModels
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
