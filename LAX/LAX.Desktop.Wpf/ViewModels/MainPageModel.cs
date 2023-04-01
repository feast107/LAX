using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AiController.Desktop.Wpf.ViewModels
{
    public class MainPageModel : INotifyPropertyChanged
    {
        public string InputBoxText { get; set; } = string.Empty;

        public ObservableCollection<ChatMessage> Messages { get; } = new ObservableCollection<ChatMessage>();

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
