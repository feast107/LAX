using System.Diagnostics;
using System.IO;

namespace LAX.Desktop.Wpf.Utilities
{
    internal class GlobalValues
    {
        public static string AppName => nameof(LAX);

        public static string JsonConfigurationFilePath { get; } =
            Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName) ?? "./", "AppConfig.json");
    }
}
