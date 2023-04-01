using System;
using System.Windows;
using AiController.Abstraction.Operation;
using AiController.Client.SignalR;
using AiController.Desktop.Wpf.Models;
using AiController.Desktop.Wpf.Services;
using AiController.Desktop.Wpf.Utilities;
using AiController.Desktop.Wpf.ViewModels;
using AiController.Desktop.Wpf.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AiController.Desktop.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly IHost Host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(config =>
            {
                config
                    .AddJsonFile(GlobalValues.JsonConfigurationFilePath, true, true)
                    .AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<ApplicationHostService>();

                services.AddSingleton<PageService>();
                services.AddSingleton<ChatService>();
                services.AddSingleton<NoteService>();
                services.AddSingleton<ConfigurationService>();
                services.AddSingleton<SmoothScrollingService>();
               
                services.AddSingleton<AppWindow>();
                services.AddSingleton<AppWindowModel>();

                services.AddSingleton<MainPage>();
                services.AddSingleton<MainPageModel>();
                services.AddSingleton<ConfigPage>();
                services.AddSingleton<ConfigPageModel>();

                services.Configure<AppConfig>(
                    o =>
                    {
                        context.Configuration.Bind(o);
                    });
            })
            .Build();

        public static T GetService<T>()
            where T : class
        {
            return (Host.Services.GetService(typeof(T)) as T) ?? throw new Exception("Cannot find service of specified type");
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await Host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await Host.StopAsync();

            Host.Dispose();
        }
    }
}
