using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;
using MahApps.IconPacksBrowser.Avalonia;

[assembly: SupportedOSPlatform("browser")]

internal sealed partial class Program
{
    private static Task Main(string[] args) => BuildAvaloniaApp()
        .WithInterFont()
        .AfterSetup(_ =>
        {
            // Register browser settings storage service (no-op persistence)
            MahApps.IconPacksBrowser.Avalonia.Services.SettingsStorage.Register(new MahApps.IconPacksBrowser.Avalonia.Browser.Services.BrowserSettingsStorageService());
        })
        .StartBrowserAppAsync("out");

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}