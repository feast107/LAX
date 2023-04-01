using AiController.Abstraction.Operation;
using AiController.Client.SignalR;
using HandyControl.Controls;
using HandyControl.Data;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AiController.Desktop.Wpf
{
    /// <summary>
    /// ConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigWindow : System.Windows.Window
    {
        private bool connecting = false;
        public delegate void ConnectHandler(IEventOperator<string> eventOperator);
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
                var conn = new HubConnectionBuilder().WithUrl(Location.Text)
                    .Build();
                var op =
                    new SignalRClientOperator(conn)
                    {
                        Identifier = Identifier.Text,
                        Description = Description.Text
                    };
                try
                {
                    await op.StartAsync();
                    if (conn.State != HubConnectionState.Connected) return;
                    await op.Register();
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
