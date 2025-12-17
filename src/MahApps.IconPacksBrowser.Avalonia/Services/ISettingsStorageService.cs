using System;

namespace MahApps.IconPacksBrowser.Avalonia.Services;

public interface ISettingsStorageService
{
    /// <summary>
    /// Reads the settings JSON string or returns null if unavailable.
    /// </summary>
    string? Read();

    /// <summary>
    /// Writes the provided settings JSON string.
    /// No-op if writing is not supported on the current platform.
    /// </summary>
    void Write(string json);
}
