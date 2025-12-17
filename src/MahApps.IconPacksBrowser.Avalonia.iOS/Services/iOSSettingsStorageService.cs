using System;
using System.IO;
using MahApps.IconPacksBrowser.Avalonia.Services;

namespace MahApps.IconPacksBrowser.Avalonia.iOS.Services;

public sealed class iOSSettingsStorageService : ISettingsStorageService
{
    private static string SettingsDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MahApps.IconPacksBrowser");
    private static string SettingsFile => Path.Combine(SettingsDirectory, "Settings.json");

    public string? Read()
    {
        try
        {
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
