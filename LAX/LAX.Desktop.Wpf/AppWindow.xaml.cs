using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using LAX.Desktop.Wpf.Services;
using LAX.Desktop.Wpf.ViewModels;

namespace LAX.Desktop.Wpf
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

        public void Navigate(object content) => AppFrame.Navigate(content);


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
