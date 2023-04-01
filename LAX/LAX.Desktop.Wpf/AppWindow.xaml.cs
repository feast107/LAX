using System.Windows;
using System.Windows.Input;
using AiController.Desktop.Wpf.Services;
using AiController.Desktop.Wpf.ViewModels;
using CommunityToolkit.Mvvm.Input;

namespace AiController.Desktop.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        public AppWindow(
            AppWindowModel viewModel,
            NoteService noteService)
        {
            ViewModel = viewModel;
            NoteService = noteService;
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            DataContext = this;
        }

        public NoteService NoteService { get; }

        public AppWindowModel ViewModel { get; }
        public NoteData NoteDataModel => NoteService.Data;

        public void Navigate(object content) => appFrame.Navigate(content);


        [RelayCommand]
        public void CloseNote()
        {
            NoteService.Close();
        }

        [RelayCommand]
        public void ShowApp()
        {
            this.Show();

            if (!this.IsActive)
                this.Activate();
        }

        [RelayCommand]
        public void HideApp()
        {
            this.Hide();
        }

        [RelayCommand]
        public void CloseApp()
        {
            Application.Current.Shutdown();
        }

        private void NoteControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NoteService.Close();
        }
    }
}
