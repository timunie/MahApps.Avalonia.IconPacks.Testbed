using System;
using Avalonia;

namespace MahApps.IconPacksBrowser.Avalonia.Desktop;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        try
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Application terminated unexpectedly during startup");
            throw;
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .AfterSetup(_ =>
            {
                // Register desktop settings storage service
                MahApps.IconPacksBrowser.Avalonia.Services.SettingsStorage.Register(new MahApps.IconPacksBrowser.Avalonia.Desktop.Services.DesktopSettingsStorageService());
            })
            .LogToTrace();
}