using System;
using System.Windows;
using LAX.Desktop.Wpf.Models;
using LAX.Desktop.Wpf.Services;
using LAX.Desktop.Wpf.Utilities;
using LAX.Desktop.Wpf.ViewModels;
using LAX.Desktop.Wpf.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LAX.Desktop.Wpf
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
