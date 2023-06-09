﻿using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Input;
using LAX.Desktop.Wpf.Services;
using LAX.Desktop.Wpf.Utilities;
using LAX.Desktop.Wpf.ViewModels;

namespace LAX.Desktop.Wpf.Views
{
    /// <summary>
    /// Interaction logic for ConfigPage.xaml
    /// </summary>
    public partial class ConfigPage : Page
    {
        public ConfigPage(
            ConfigPageModel viewModel,
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
            LoadSystemMessagesCore();

            smoothScrollingService.Register(configurationScrollViewer);
        }

        public ConfigPageModel ViewModel { get; }
        public PageService PageService { get; }
        public NoteService NoteService { get; }
        public ConfigurationService ConfigurationService { get; }


        [RelayCommand]
        public void GoToMainPage()
        {
            PageService.Navigate<MainPage>();
        }

        [RelayCommand]
        public void About()
        {
        }

        private void LoadSystemMessagesCore()
        {
            ViewModel.SystemMessages.Clear();
            foreach (var msg in ConfigurationService.Configuration.SystemMessages)
                ViewModel.SystemMessages.Add(new ValueWrapper<string>(msg));
        }

        private void ApplySystemMessagesCore()
        {
            ConfigurationService.Configuration.SystemMessages = ViewModel.SystemMessages
                .Select(wraper => wraper.Value)
                .ToArray();
        }

        [RelayCommand]
        public Task LoadSystemMessages()
        {
            LoadSystemMessagesCore();
            return NoteService.ShowAsync("System messages loaded", 1500);
        }

        [RelayCommand]
        public Task ApplySystemMessages()
        {
            ApplySystemMessagesCore();
            return NoteService.ShowAsync("System messages applied", 1500);
        }

        [RelayCommand]
        public void AddSystemMessage()
        {
            ViewModel.SystemMessages.Add(new ValueWrapper<string>("New system message"));
        }

        [RelayCommand]
        public void RemoveSystemMessage()
        {
            if(ViewModel.SystemMessages.Count > 0)
            {
                ViewModel.SystemMessages.RemoveAt(ViewModel.SystemMessages.Count - 1);
            }
        }

        [RelayCommand]
        public Task SaveConfiguration()
        {
            ConfigurationService.Save();
            return NoteService.ShowAsync("Configuration saved", 2000);
        }
    }
}
