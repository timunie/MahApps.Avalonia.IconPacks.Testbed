using MahApps.IconPacksBrowser.Avalonia.Properties;

namespace MahApps.IconPacksBrowser.Avalonia.Services;

public static class SettingsStorage
{
    private static ISettingsStorageService? _service;

    public static void Register(ISettingsStorageService service)
    {
        _service = service;
        // We can load the settings as soon as the service is registered.
        Settings.LoadSettings();
    }

    public static ISettingsStorageService Get()
    {
        return _service ??= new DefaultSettingsStorageService();
    }
}
