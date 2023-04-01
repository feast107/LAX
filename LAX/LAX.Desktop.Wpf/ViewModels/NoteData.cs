using System.ComponentModel;

namespace LAX.Desktop.Wpf.ViewModels
{
    public class NoteData : INotifyPropertyChanged
    {
        public string Text { get; set; } = string.Empty;

        public bool Show { get; set; } = false;


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
