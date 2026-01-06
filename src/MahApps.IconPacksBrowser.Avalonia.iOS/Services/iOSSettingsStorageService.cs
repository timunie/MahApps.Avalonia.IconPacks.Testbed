using System;
using System.IO;
using System.Threading.Tasks;
using MahApps.IconPacksBrowser.Avalonia.Services;

namespace MahApps.IconPacksBrowser.Avalonia.iOS.Services;

public sealed class iOSSettingsStorageService : ISettingsStorageService
{
    private static string SettingsDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MahApps.IconPacksBrowser");
    private static string SettingsFile => Path.Combine(SettingsDirectory, "Settings.json");

    public async Task<string?> ReadAsync()
    {
        try
        {
            if (!File.Exists(SettingsFile)) return null;
            return await File.ReadAllTextAsync(SettingsFile);
        }
        catch
        {
            return null;
        }
    }

    public async Task WriteAsync(string json)
    {
        try
        {
            if (!Directory.Exists(SettingsDirectory))
            {
                Directory.CreateDirectory(SettingsDirectory);
            }
            await File.WriteAllTextAsync(SettingsFile, json);
        }
        catch
        {
            // ignore
        }
    }
}
