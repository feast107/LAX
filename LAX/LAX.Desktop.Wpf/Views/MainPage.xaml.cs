using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AiController.Abstraction.Operation;
using AiController.Desktop.Wpf.Services;
using AiController.Desktop.Wpf.ViewModels;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools;
using HandyControl.Tools.Extension;
using ScrollViewer = System.Windows.Controls.ScrollViewer;
using Window = HandyControl.Controls.Window;

namespace AiController.Desktop.Wpf.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage(
            MainPageModel viewModel,
            PageService pageService,
            NoteService noteService,
            ConfigurationService configurationService,
            SmoothScrollingService smoothScrollingService)
        {
            ViewModel = viewModel;
            PageService = pageService;
            NoteService = noteService;
            ConfigurationService = configurationService;

            InitializeComponent();

            DataContext = this;

            smoothScrollingService.Register(MessageScrollViewer);
            
            Loaded += (o, e) =>
            {
                var configure = new ConfigWindow
                {
                    Owner = Application.Current.MainWindow,
                };
                configure.OnConnected += (opera) =>
                {
                    this.eventOperator = opera;
                    this.MainTitle.Text = opera.Identifier;
                    this.eventOperator.OnReceiveOperation += (s) =>
                    {
                        var responseMessage = new ChatMessage()
                        {
                            Role = ChatRoleType.Receiver,
                            Message = s
                        };
                        Dispatcher.Invoke(() =>
                        {
                            ViewModel.Messages.Add(responseMessage);
                        });
                    };
                    configure.Close();
                };
                configure.Closed += (_,_) => {
                    if (this.eventOperator == null)
                    {
                        Application.Current.Shutdown(1);
                    }
                };
                configure.ShowDialog();
            };
        }

        private IEventOperator<string>? eventOperator;

        public MainPageModel ViewModel { get; }
        public PageService PageService { get; }
        public NoteService NoteService { get; }
        public ConfigurationService ConfigurationService { get; }

        [RelayCommand]
        public Task SendAsync()
        {
            var input = ViewModel.InputBoxText.Trim();
            ViewModel.InputBoxText = string.Empty;
            var requestMessage = new ChatMessage()
            {
                Role = ChatRoleType.Sender,
                Message = input,
            };
            eventOperator!.Send(input);
            ViewModel.Messages.Add(requestMessage);
            return Task.CompletedTask;
        }

        [RelayCommand]
        public void GoToConfigPage()
        {
            PageService.Navigate<ConfigPage>();
        }

        [RelayCommand]
        public async Task ResetChat()
        {
            ViewModel.Messages.Clear();
            await NoteService.ShowAsync("Chat has been reset.", 1500);
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            if (SendCommand.IsRunning)
            {
            }
        }
    }
}
