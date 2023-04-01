using System;
using System.ComponentModel;
using System.Windows;
using HandyControl.Data;
using PropertyChanged;

namespace AiController.Desktop.Wpf.ViewModels
{
    public class ChatMessage : INotifyPropertyChanged
    {
        public string Message { get; set; } = string.Empty;
        public ChatRoleType Role { get; set; } = ChatRoleType.Sender;
      
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
