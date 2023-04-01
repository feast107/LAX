using System.Collections.ObjectModel;
using System.ComponentModel;
using AiController.Desktop.Wpf.Utilities;

namespace AiController.Desktop.Wpf.ViewModels
{
    public class ConfigPageModel : INotifyPropertyChanged
    {
        public ObservableCollection<ValueWrapper<string>> SystemMessages { get; }
            = new ObservableCollection<ValueWrapper<string>>();

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
