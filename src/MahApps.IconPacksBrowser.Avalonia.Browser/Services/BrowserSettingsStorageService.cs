using System;
using System.Runtime.Versioning;
using System.Runtime.InteropServices.JavaScript;
using MahApps.IconPacksBrowser.Avalonia.Services;

namespace MahApps.IconPacksBrowser.Avalonia.Browser.Services;

public sealed class BrowserSettingsStorageService : ISettingsStorageService
{
    private const string StorageKey = "MahApps.IconPacksBrowser.Settings";

    [SupportedOSPlatform("browser")]
    public string? Read()
    {
        try
        {
            return BrowserStorageInterop.LocalStorageGetItem(StorageKey);
        }
        catch
        {
            return null;
        }
    }

    [SupportedOSPlatform("browser")]
    public void Write(string json)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return;
            }

            BrowserStorageInterop.LocalStorageSetItem(StorageKey, json);
        }
        catch
        {
            // ignore write failures on browser
        }
    }
}
