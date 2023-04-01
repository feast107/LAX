using System.Windows;

namespace AiController.Desktop.Wpf.Services
{
    public class PageService
    {
        private readonly AppWindow mainWindow;

        public PageService(AppWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void Navigate<T>()
            where T : FrameworkElement
        {
            mainWindow.Navigate(App.GetService<T>());
        }
    }
}
