using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using MahApps.IconPacksBrowser.Avalonia.Android.Services;
using MahApps.IconPacksBrowser.Avalonia.Services;

namespace MahApps.IconPacksBrowser.Avalonia.Android;

[Activity(
    Label = "MahApps.IconPacksBrowser.Avalonia.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .AfterSetup(_ =>
            {
                // Register Android settings storage service
                SettingsStorage.Register(new AndroidSettingsStorageService());
            });
    }
}