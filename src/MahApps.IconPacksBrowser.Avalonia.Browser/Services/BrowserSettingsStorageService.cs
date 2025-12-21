using System.Runtime.Versioning;
using MahApps.IconPacksBrowser.Avalonia.Services;

namespace MahApps.IconPacksBrowser.Avalonia.Browser.Services;

public sealed class BrowserSettingsStorageService : ISettingsStorageService
{
    private const string StorageKey = "MahApps.IconPacksBrowser.Settings";
    private static string? _inMemoryCache; // last known settings when persistent storage is unavailable

    [SupportedOSPlatform("browser")]
    public string? Read()
    {
        // Try localStorage first
        try
        {
            var json = BrowserStorageInterop.LocalStorageGetItem(StorageKey);
            if (!string.IsNullOrWhiteSpace(json))
            {
                _inMemoryCache = json;
                return json;
            }
        }
        catch
        {
            // ignore and try sessionStorage
        }

        // Fallback to sessionStorage
        try
        {
            var json = BrowserStorageInterop.SessionStorageGetItem(StorageKey);
            if (!string.IsNullOrWhiteSpace(json))
            {
                _inMemoryCache = json;
                return json;
            }
        }
        catch
        {
            // ignore and fallback to memory
        }

        // Final fallback: in-memory for current session only
        return _inMemoryCache;
    }

    [SupportedOSPlatform("browser")]
    public void Write(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return;
        }

        // Always update in-memory cache first so UI stays consistent
        _inMemoryCache = json;

        // Try localStorage
        try
        {
            BrowserStorageInterop.LocalStorageSetItem(StorageKey, json);
            return;
        }
        catch
        {
            // ignored, try sessionStorage next
        }

        // Fallback to sessionStorage
        try
        {
            BrowserStorageInterop.SessionStorageSetItem(StorageKey, json);
            return;
        }
        catch
        {
            // Both persistent stores failed; keep using in-memory cache only
        }
    }
}
