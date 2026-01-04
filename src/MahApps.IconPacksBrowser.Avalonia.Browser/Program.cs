using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;
using MahApps.IconPacksBrowser.Avalonia;
using MahApps.IconPacksBrowser.Avalonia.Browser.Services;
using MahApps.IconPacksBrowser.Avalonia.Services;

[assembly: SupportedOSPlatform("browser")]

internal sealed partial class Program
{
    private static Task Main(string[] args)
    {
        SettingsStorage.Register(new BrowserSettingsStorageService());
        
        return BuildAvaloniaApp()
            .WithInterFont()
            .StartBrowserAppAsync("out");
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}