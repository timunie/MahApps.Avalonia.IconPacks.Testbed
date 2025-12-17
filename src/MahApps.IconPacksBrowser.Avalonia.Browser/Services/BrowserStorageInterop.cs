using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace MahApps.IconPacksBrowser.Avalonia.Browser.Services;

[SupportedOSPlatform("browser")]
internal static partial class BrowserStorageInterop
{
    [JSImport("localStorage.getItem", "globalThis")]
    internal static partial string? LocalStorageGetItem(string key);

    [JSImport("localStorage.setItem", "globalThis")]
    internal static partial void LocalStorageSetItem(string key, string value);
}
