using System.Windows;
using HandyControl.Controls;
using HandyControl.Data;
using LAX.Abstraction.Operation;
using LAX.Client;
using LAX.Client.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace LAX.Desktop.Wpf
{
    /// <summary>
    /// ConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigWindow : System.Windows.Window
    {
        private bool connecting = false;
        public delegate void ConnectHandler(ILAXClient eventOperator);
        public ConnectHandler? OnConnected { get; set; }
        public ConfigWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Location.Text = "http://localhost:5030/server";
            Connect.Click += async (o, e) =>
            {
                if (connecting) return;
                connecting = true;
                Connect.IsChecked = true;
                ILAXClient op =
                    new LAXSignalRClient(Location.Text)
                    {
                        Identifier = Identifier.Text,
                        Description = Description.Text
                    };
                try
                {
                    await op.StartAsync();
                    if (op.State != ILAXClient.ConnectionStatus.Connected) return;
                    OnConnected?.Invoke(op);
                }
                catch
                {
                    Growl.ErrorGlobal(new GrowlInfo()
                    {
                        Message = "连接失败",
                        ShowDateTime = false,
                    });
                }
                finally
                {
                    connecting = false;
                }
            };
        }
    }
}
