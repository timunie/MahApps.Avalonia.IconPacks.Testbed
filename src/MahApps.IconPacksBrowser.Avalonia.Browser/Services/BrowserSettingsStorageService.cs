using System;
using System.Net.Http.Json;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using MahApps.IconPacksBrowser.Avalonia.Services;

namespace MahApps.IconPacksBrowser.Avalonia.Browser.Services;

public partial class BrowserSettingsStorageService : ISettingsStorageService
{
    [JSImport("setItem", "storage")]
    private static partial void SetItem(string key, string value);

    [JSImport("getItem", "storage")]
    private static partial string? GetItem(string key);

    private static string Identifier { get; } = "MahApps_IconPacksBrowser_Settings";
    
    public async Task<string?> ReadAsync()
    {    
        try
        {
            await InitializeAsync();
            
            Console.WriteLine("Attempting to read settings from storage");
            var json = GetItem(Identifier);
            Console.WriteLine($"Settings read from storage for key '{Identifier}': '{(json ?? "null")}'");
            return json;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task WriteAsync(string json)
    {
        await InitializeAsync();
        Console.WriteLine($"Attempting to write settings to storage for key '{Identifier}'");
        SetItem(Identifier, json);
        Console.WriteLine($"Wrote settings to storage: {Identifier}");
    }

    private async Task InitializeAsync()
    {
        const string storageJsLocation = "../storage.js";
        await JSHost.ImportAsync("storage",storageJsLocation);
    }
}