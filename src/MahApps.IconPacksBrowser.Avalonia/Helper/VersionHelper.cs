using System.Reflection;

namespace MahApps.IconPacksBrowser.Avalonia.Helper;

public static class VersionHelper
{
    public static string? GetAssemblyVersionSafe(this Assembly asm)
    {
        // Prefer informational version if present
        var info = asm
            .GetCustomAttribute<AssemblyFileVersionAttribute>()?
            .Version; 
        
        if (!string.IsNullOrWhiteSpace(info))
            return info;

        return asm.GetName().Version?.ToString();
    }
}