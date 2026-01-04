using MahApps.IconPacksBrowser.Avalonia.Properties;

namespace MahApps.IconPacksBrowser.Avalonia.Services;

public static class SettingsStorage
{
    private static ISettingsStorageService? _service;

    public static void Register(ISettingsStorageService service)
    {
        _service = service;
    }

    public static ISettingsStorageService Get()
    {
        return _service ??= new DefaultSettingsStorageService();
    }
}
