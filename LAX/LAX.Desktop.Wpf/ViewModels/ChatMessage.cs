using System.ComponentModel;
using HandyControl.Data;

namespace LAX.Desktop.Wpf.ViewModels
{
    public class ChatMessage : INotifyPropertyChanged
    {
        public string Message { get; set; } = string.Empty;
        public ChatRoleType Role { get; set; } = ChatRoleType.Sender;
      
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
