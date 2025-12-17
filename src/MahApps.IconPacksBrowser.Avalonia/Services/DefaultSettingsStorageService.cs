using System;
using System.IO;

namespace MahApps.IconPacksBrowser.Avalonia.Services;

/// <summary>
/// Default file system based storage. On Browser it behaves as no-op.
/// </summary>
public sealed class DefaultSettingsStorageService : ISettingsStorageService
{
    private static string SettingsDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MahApps.IconPacksBrowser");
    private static string SettingsFile => Path.Combine(SettingsDirectory, "Settings.json");

    public string? Read()
    {
        try
        {
            if (OperatingSystem.IsBrowser()) return null;
            if (!File.Exists(SettingsFile)) return null;
            return File.ReadAllText(SettingsFile);
        }
        catch
        {
            return null;
        }
    }

    public void Write(string json)
    {
        try
        {
            if (OperatingSystem.IsBrowser()) return;
            if (!Directory.Exists(SettingsDirectory))
            {
                Directory.CreateDirectory(SettingsDirectory);
            }
            File.WriteAllText(SettingsFile, json);
        }
        catch
        {
            // ignore
        }
    }
}
