﻿using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LAX.Desktop.Wpf.Utilities;
using LAX.Desktop.Wpf.Views;
using Microsoft.Extensions.Hosting;

namespace LAX.Desktop.Wpf.Services
{
    internal class ApplicationHostService : IHostedService
    {
        public ApplicationHostService(
            ConfigurationService config)
        {
            ConfigurationService = config;
        }

        public ConfigurationService ConfigurationService { get; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (!File.Exists(GlobalValues.JsonConfigurationFilePath))
                ConfigurationService.Save();

            if (!App.Current.Windows.OfType<AppWindow>().Any())
            {
                AppWindow window = App.GetService<AppWindow>();
                window.Show();

                window.Navigate(App.GetService<MainPage>());
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
