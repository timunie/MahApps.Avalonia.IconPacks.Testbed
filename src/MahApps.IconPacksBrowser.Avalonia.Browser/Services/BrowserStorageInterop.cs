using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace MahApps.IconPacksBrowser.Avalonia.Browser.Services;

[SupportedOSPlatform("browser")]
internal static partial class BrowserStorageInterop
{
    // Use fully qualified names in the single-argument form to avoid module resolution issues on WASM
    [JSImport("globalThis.localStorage.getItem")]
    internal static partial string? LocalStorageGetItem(string key);

    [JSImport("globalThis.localStorage.setItem")]
    internal static partial void LocalStorageSetItem(string key, string value);

    [JSImport("globalThis.localStorage.removeItem")]
    internal static partial void LocalStorageRemoveItem(string key);

    [JSImport("globalThis.sessionStorage.getItem")]
    internal static partial string? SessionStorageGetItem(string key);

    [JSImport("globalThis.sessionStorage.setItem")]
    internal static partial void SessionStorageSetItem(string key, string value);

    [JSImport("globalThis.sessionStorage.removeItem")]
    internal static partial void SessionStorageRemoveItem(string key);
}
