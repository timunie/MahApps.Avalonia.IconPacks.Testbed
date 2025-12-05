using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Avalonia.Data.Converters;

namespace MahApps.IconPacksBrowser.Avalonia.Converters;

public class GetAssemblyVersionConverter : IValueConverter
{
    public static GetAssemblyVersionConverter Instance { get; } = new GetAssemblyVersionConverter();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return null;

        // Browser/WASM does not support FileVersionInfo or Assembly.Location.
        // Prefer informational version; fall back to assembly version.
        var asm = value.GetType().Assembly;
        var info = asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (!string.IsNullOrWhiteSpace(info))
            return info;

        return asm.GetName().Version?.ToString();
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}